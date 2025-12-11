using System;
using System.IO;
using UnityEngine;

namespace _Project.Script.ScriptableObjects.Character
{
    [CreateAssetMenu(menuName = "ScriptableObjects/MonsterData")]
    public class MonsterData : CharacterData
    {
        public float hp;
        public float def;
        public float speed;

        public Vector2 stage;
        public int rewardValue;

        public AnimationClip dieClip;
        
        #if UNITY_EDITOR

        private void Reset()
        {
            var path = UnityEditor.AssetDatabase.GetAssetPath(this);
            var fileName = Path.GetFileNameWithoutExtension(path);
            
            var split = fileName.Split('_');

            var grade = int.Parse(split[1]);
            
            hp = 100f + (grade-1) * 10f;
            speed = 3 + grade;
            
            switch (int.Parse(split[1]))
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
            }
        }

#endif
    }
}
