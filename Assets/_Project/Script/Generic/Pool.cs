using _Project.Script.Interface;
using _Project.Script.ScriptableObjects.Character;
using UnityEngine;
using UnityEngine.Pool;

namespace _Project.Script.Generic
{
    public class Pool
    {
        private CharacterData data;
        private Transform parent;
        private ObjectPool<GameObject> pool;

        public Pool(CharacterData data, Transform poolParent)
        {
            this.data = data;
            parent = poolParent;
            pool = new ObjectPool<GameObject>(Create, OnGet, OnRelease, OnDestroy);
        }

        private GameObject Create()
        {
            var obj = Object.Instantiate(data.prefab);
            obj.TryGetComponent(out IPoolingAble pooling);
            
            pooling.Pool = this;
            pooling.OnCreated(data);
            
            return obj;
        }

        private void OnGet(GameObject obj)
        {
            obj.TryGetComponent(out IPoolingAble pooling);
            obj.transform.SetParent(null);
            pooling.OnSpawned();
        }

        private void OnRelease(GameObject obj)
        {
            obj.TryGetComponent(out IPoolingAble pooling);
            obj.transform.SetParent(parent);
            pooling.OnDespawned();
        }

        private void OnDestroy(GameObject obj)
        {
            Object.Destroy(obj);
        }
        

        public GameObject Get() => pool.Get();
        public void Release(GameObject obj) => pool.Release(obj);
        public void Clear() => pool.Clear();
    }
}
