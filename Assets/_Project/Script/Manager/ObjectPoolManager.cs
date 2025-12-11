using System.Collections.Generic;
using _Project.Script.Generic;
using _Project.Script.Interface;
using _Project.Script.ScriptableObjects.Character;
using Script.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

namespace _Project.Script.Manager
{
    public class ObjectPoolManager : MonoSingleton<ObjectPoolManager>
    {
        private Dictionary<CharacterData, Pool> _poolDic;
        private Transform _poolParent;
        
        protected override void Awake()
        {
            base.Awake();
            
            _poolDic = new Dictionary<CharacterData, Pool>();
            _poolParent = new GameObject("ObjectPool").transform;
            _poolParent.gameObject.SetActive(false);
            _poolParent.position = Vector3.zero;
            _poolParent.rotation = Quaternion.identity;
        }

        public GameObject Get(CharacterData data)
        {
            if (_poolDic.ContainsKey(data) == false)
            {
                RegisterPool(data);
            }
            var obj = _poolDic[data].Get();

            return obj;
        }

        public void Release(CharacterData data, GameObject obj) => _poolDic[data].Release(obj);
        
        private void RegisterPool(CharacterData data)
        {
            _poolDic.TryAdd(data, new Pool(data, _poolParent));
        }

        private void UnRegisterPool(CharacterData data)
        {
            _poolDic[data].Clear();
            _poolDic.Remove(data);
        }
    }
}
