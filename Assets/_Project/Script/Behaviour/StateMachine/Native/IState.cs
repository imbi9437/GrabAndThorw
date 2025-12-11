using UnityEngine;

namespace _Project.Script.Behaviour.StateMachine.Native
{
    public interface IState<T>
    {
        public void Enter(T owner);
        public void Update(T owner);
        public void Exit(T owner);
    }
}
