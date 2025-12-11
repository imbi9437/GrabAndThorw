using System;
using System.Threading;
using _Project.Script.Extensions;
using _Project.Script.Interface;
using _Project.Script.Manager;
using _Project.Script.ScriptableObjects.Character;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Script.Character
{
    public class MonsterObject : CharacterObject, IHitAble
    {
        private static readonly int IsDie = Animator.StringToHash("IsDie");
        
        private float _maxHp;
        private float _def;
        private float _speed;
        private int _reward;
        private int _wayPointIndex;
        
        private float _curHp;
        private CancellationTokenSource _moveCts;
        private CancellationTokenSource _waitCts;
        public float Hp => _curHp;
        
        private void OnDestroy()
        {
            _moveCts?.Cancel();
            _moveCts?.Dispose();
            _moveCts = null;
            
            _waitCts?.Cancel();
            _waitCts?.Dispose();
            _waitCts = null;
        }

        private void Initialize(MonsterData data)
        {
            _maxHp = data.hp;
            _def = data.def;
            _speed = data.speed;
            _reward = data.rewardValue;

            _curHp = _maxHp;
            mainAgent.speed = _speed;
        }

        public override void OnCreated(CharacterData data)
        {
            if (data is not MonsterData monsterData) return;
            Initialize(monsterData);
        }

        public override void OnSpawned()
        {
            _wayPointIndex = 0;
            _curHp = _maxHp;

            mainAnimator.SetBool(IsDie, false);
            mainAnimator.SetBool(Move, false);
            mainAgent.isStopped = false;
            mainCollider.enabled = true;

            var startPos = DefenceManager.Instance.StartPosition;
            DefenceManager.Instance.GetNextPoint(_wayPointIndex, out var next);
            DefenceManager.Instance.IncreaseMonsterCount();
            var dir = next - startPos;
            dir.y = 0f;
            dir.Normalize();
            
            transform.forward = dir;
            mainAgent.Warp(startPos);

            if (_moveCts != null)
            {
                _moveCts.Cancel();
                _moveCts.Dispose();
                _moveCts = null;
            }

            if (_waitCts != null)
            {
                _waitCts.Cancel();
                _waitCts.Dispose();
                _waitCts = null;           
            }
            
            _moveCts = new CancellationTokenSource();
            _waitCts = new CancellationTokenSource();
            MoveToEnd(_moveCts.Token).Forget();
        }
        public override void OnDespawned()
        {
            _moveCts?.Cancel();
            _moveCts?.Dispose();
            _moveCts = null;
            
            _waitCts?.Cancel();
            _waitCts?.Dispose();
            _waitCts = null;
            DefenceManager.Instance.DecreaseMonsterCount();
        }

        public void Hit(float damage)
        {
            if (_curHp <= 0) return;
            
            _curHp -= damage;
            var effect = DataManager.Instance.hitParticle;
            Instantiate(effect, transform.position, Quaternion.identity);
            
            if (_curHp <= 0) Die();
        }
        private void Die()
        {
            mainCollider.enabled = false;
            mainAgent.isStopped = true;
            mainAnimator.SetBool(IsDie, true);
            
            DataManager.Instance.SetGold(_reward);

            mainAnimator.WaitAnimateEnd("Die",Release, _waitCts.Token).Forget();
        }

        private async UniTaskVoid MoveToEnd(CancellationToken cts)
        {
            bool hasNext = true;  
            
            while (hasNext)
            {
                hasNext = DefenceManager.Instance.GetNextPoint(_wayPointIndex, out var nextPos);
                mainAgent.SetDestination(nextPos);
                mainAnimator.SetBool(Move, true);
                
                await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken: cts);
                await UniTask.WaitUntil(mainAgent.IsCompleteComputePath, cancellationToken: cts);
                await UniTask.WaitUntil(mainAgent.IsCompleteMove, cancellationToken: cts);
                _wayPointIndex++;
            }
            
            DataManager.Instance.SetLifeCount(-1);
            mainAnimator.SetBool(Move, false);
            Release();
        }
    }
}
