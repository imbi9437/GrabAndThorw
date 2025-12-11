using System;
using _Project.Script.Behaviour.StateMachine.Mono;

namespace _Project.Script.Stage
{
    public enum StageType
    {
        Rest,
        Defence,
    }
    public class StageController : MonoStateMachine
    {
        private void Start()
        {
            ChangeState(StageType.Rest);
        }

        public void ChangeState(StageType type) => ChangeState((int)type);
    }
}
