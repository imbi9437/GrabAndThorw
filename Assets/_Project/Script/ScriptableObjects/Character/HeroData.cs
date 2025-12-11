using System;
using System.IO;
using UnityEngine;

namespace _Project.Script.ScriptableObjects.Character
{
    [CreateAssetMenu(menuName = "ScriptableObjects/HeroData")]
    public class HeroData : CharacterData
    {
        public float attackRange;
        public float attackDamage;
        public float attackSpeed;

        public int grade;
        public HeroData mergedHeroData;
        
        public AnimationClip attackClip;

        #if UNITY_EDITOR
        
        private void Reset()
        {
            attackRange = 8f;
            attackSpeed = 1f;

            var path = UnityEditor.AssetDatabase.GetAssetPath(this);
            var fileName = Path.GetFileNameWithoutExtension(path);
            
            var split = fileName.Split('_');
            grade = int.Parse(split[1]);

            attackDamage = grade switch
            {
                1 => 30f,
                2 => 50f,
                3 => 70f,
                4 => 90f,
                5 => 150f,
                _ => 0f
            };
        }
        
        #endif
    }
}
