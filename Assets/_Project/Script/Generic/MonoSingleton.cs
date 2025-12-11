using System;
using UnityEngine;

namespace Script.Generic
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        private static object _lock = new object();
        private static bool _isApplicationQuitting = false;
        
        [SerializeField] protected bool isDontDestroyOnLoad = false;

        public static T Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_isApplicationQuitting) return null;
                    if (_instance == null) _instance = FindAnyObjectByType<T>();
                    if (_instance) return _instance;

                    GameObject obj = new GameObject(typeof(T).Name);
                    _instance = obj.AddComponent<T>();
                    
                    return _instance;
                }
            }
        }

        protected virtual void Awake()
        {
            if (_instance == null) _instance = this as T;
            else DestroyImmediate(_instance);
            
            if (isDontDestroyOnLoad) DontDestroyOnLoad(gameObject);
        }

        private void OnApplicationQuit()
        {
            _isApplicationQuitting = true;
        }
    }
}
