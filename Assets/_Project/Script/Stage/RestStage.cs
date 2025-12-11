using _Project.Script.Behaviour.StateMachine.Mono;
using UnityEngine;

namespace _Project.Script.Stage
{
    public class RestStage : Stage
    {
        public override int index => (int)StageType.Rest;

        [SerializeField] private float restTime;
    
        private float _curTime;

        protected override void OnEnable()
        {
            _curTime = 0f;
        }

        protected override void Update()
        {
            _curTime += Time.deltaTime;
        
            if (_curTime < restTime) return;
        
            Controller.ChangeState(StageType.Defence);
        }


        protected override void OnDisable()
        {
            
        }
    }
}
