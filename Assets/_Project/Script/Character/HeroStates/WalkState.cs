using System.Threading;
using _Project.Script.Behaviour.StateMachine.Native;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Script.Character.HeroStates
{
    public class WalkState : State<HeroObject>
    {
        private CancellationTokenSource _cts;
        
        public override void Enter(HeroObject owner)
        {
            owner.MoveAnimate(true);
            
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = new CancellationTokenSource();
            
            MoveToPointAsync(owner, _cts.Token).Forget();
        }

        public override void Exit(HeroObject owner)
        {
            owner.MoveAnimate(false);
            
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }

        private async UniTaskVoid MoveToPointAsync(HeroObject owner,CancellationToken token)
        {
            owner.MoveToPoint();
            
            await UniTask.Yield();
            await UniTask.WaitUntil(owner.IsCompleteCalcPath, cancellationToken: token);
            await UniTask.WaitUntil(owner.IsCompleteMove, cancellationToken: token);
            owner.ChangeState(HeroState.Combat);
        }
    }
}
