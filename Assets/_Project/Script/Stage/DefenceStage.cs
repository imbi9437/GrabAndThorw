using System.Collections.Generic;
using _Project.Script.Behaviour.StateMachine.Mono;
using _Project.Script.Character;
using _Project.Script.Manager;
using _Project.Script.ScriptableObjects.Character;
using UnityEngine;

namespace _Project.Script.Stage
{
    public class DefenceStage : Stage
    {
        public override int index => (int)StageType.Defence;
        
        [SerializeField] private float spawnDelay;
        
        private float _curDelay;
        private Queue<MonsterData> spawnQueue = new Queue<MonsterData>();
        
        protected override void OnEnable()
        {
            _curDelay = 0f;
            
            DataManager.Instance.GetRandomMonsterSpawn(spawnQueue);
        }

        protected override void Update()
        {
            int queueCount = spawnQueue.Count;
            int monsterCount = DefenceManager.Instance.MonsterCount;
            
            if (queueCount <= 0 && monsterCount <= 0) Controller.ChangeState(StageType.Rest);
            if (queueCount <= 0) return;
            
            _curDelay += Time.deltaTime;
            if (_curDelay < spawnDelay) return;
            _curDelay = 0f;
            
            var data = spawnQueue.Dequeue();
            ObjectPoolManager.Instance.Get(data);
        }

        protected override void OnDisable()
        {
            DataManager.Instance?.SetStageLevel(1);
            DataManager.Instance.spawnCount += 5;
        }
    }
}
