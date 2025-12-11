using System;
using _Project.Script.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Script
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Collider pointCollider;

        private void Start()
        {
            button.onClick.AddListener(Spawn);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(Spawn);
        }

        private void Spawn()
        {
            if (DataManager.Instance.spawnCount <= 0) return;
            if (Physics.Raycast(pointCollider.transform.position, pointCollider.transform.up, 5f, ~(1 <<LayerMask.NameToLayer("Environment")))) return;
            
            DataManager.Instance.spawnCount--;
            var data = DataManager.Instance.GetRandomHeroData(); 
            var obj = ObjectPoolManager.Instance.Get(data);
            obj.transform.position = spawnPoint.position;
        }
    }
}
