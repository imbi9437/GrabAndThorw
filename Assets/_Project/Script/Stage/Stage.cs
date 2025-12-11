using _Project.Script.Behaviour.StateMachine.Mono;
using UnityEngine;

namespace _Project.Script.Stage
{
    public abstract class Stage : MonoState
    {
        protected StageController Controller;

        public override void InitState(MonoStateMachine machine)
        {
            if (machine is not StageController con)
            {
                Debug.LogError("Invalid State Machine");
                return;
            }

            Controller = con;
        }
    }
}
