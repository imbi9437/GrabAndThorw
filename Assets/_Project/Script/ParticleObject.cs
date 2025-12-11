using System;
using Unity.VisualScripting;
using UnityEngine;

namespace _Project.Script
{
    public class ParticleObject : MonoBehaviour
    {
        public ParticleSystem particleSystem;

        private void Start()
        {
            particleSystem.Play();
        }

        private void Update()
        {
            if (particleSystem.isStopped) Destroy(gameObject);
        }
    }
}
