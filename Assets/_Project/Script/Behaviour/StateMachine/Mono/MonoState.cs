using System;
using UnityEngine;

namespace _Project.Script.Behaviour.StateMachine.Mono
{
    public abstract class MonoState : MonoBehaviour
    {
        public abstract int index { get; }

        protected abstract void OnEnable();
        protected abstract void Update();
        protected abstract void OnDisable();

        public abstract void InitState(MonoStateMachine machine);
    }
}
