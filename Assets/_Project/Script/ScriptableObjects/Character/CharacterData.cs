using UnityEngine;

namespace _Project.Script.ScriptableObjects.Character
{
    public abstract class CharacterData : ScriptableObject
    {
        public int uid;
        public string charName;
        public string charDesc;

        public GameObject prefab;
    }
}
