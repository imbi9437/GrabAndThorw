using System;
using System.Collections.Generic;
using _Project.Script.Interface;
using Script.Generic;

namespace _Project.Script.Manager
{
    public class EventHub : MonoSingleton<EventHub>
    {
        private Dictionary<Type, Delegate> _eventDic;

        protected override void Awake()
        {
            base.Awake();
            
            _eventDic = new Dictionary<Type, Delegate>();
        }


        public void RegisterEvent<T>(Action<T> callback) where T : struct, IEvent
        {
            if (_eventDic.TryGetValue(typeof(T), out var handler))
                _eventDic[typeof(T)] = (Action<T>)handler + callback;
            else
                _eventDic.Add(typeof(T), callback);
        }
        
        public void UnRegisterEvent<T>(Action<T> callback) where T : struct, IEvent
        {
            if (_eventDic.TryGetValue(typeof(T), out var handler) == false) return;
            
            var cur = (Action<T>)handler - callback;

            if (cur == null) _eventDic.Remove(typeof(T));
            else _eventDic[typeof(T)] = cur;
        }

        public void RaiseEvent<T>(T eventData) where T : struct, IEvent
        {
            if (_eventDic.TryGetValue(typeof(T), out var handler))
                ((Action<T>)handler)?.Invoke(eventData);
        }
    }
}
