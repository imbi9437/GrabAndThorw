using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Script.Behaviour.StateMachine.Mono
{
    public abstract class MonoStateMachine : MonoBehaviour
    {
        private Dictionary<int, MonoState> _stateDic;

        private MonoState _prevState;
        private MonoState _currentState;
        
        protected virtual void Awake()
        {
            InitState();
        }

        protected void InitState()
        {
            _stateDic = new Dictionary<int, MonoState>();
            
            var states = GetComponentsInChildren<MonoState>(true);
            foreach (var state in states)
            {
                _stateDic.Add(state.index, state);
                state.InitState(this);
            }
        }
        
        public void ChangeState(int index)
        {
            if (_stateDic == null || _stateDic.Count <= 0) return;
            
            _prevState = _currentState;
            _currentState = _stateDic[index];
            
            _prevState?.gameObject.SetActive(false);
            _currentState?.gameObject.SetActive(true);
        }
    }
}
