#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace _Project.Script.Utility
{
    public static class ColliderRemover
    {
        [MenuItem("Tools/Remove Collider")]
        public static void RemoveCollider()
        {
            var objs = Selection.gameObjects;

            if (objs.Length <= 0) return;

            for (int i = 0; i < objs.Length; i++)
            {
                var cols = objs[i].GetComponentsInChildren<Collider>();

                for (int j = 0; j < cols.Length; j++)
                {
                    Object.DestroyImmediate(cols[j]);
                }
            }
            
            AssetDatabase.SaveAssets();
        }
    }
}
#endif