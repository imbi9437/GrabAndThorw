using _Project.Script.Generic;
using _Project.Script.ScriptableObjects.Character;

namespace _Project.Script.Interface
{
    public interface IPoolingAble
    {
        public Pool Pool { get; set; }
        
        public void OnCreated(CharacterData data);
        public void OnSpawned();
        public void OnDespawned();
        public void Release();
    }
}
