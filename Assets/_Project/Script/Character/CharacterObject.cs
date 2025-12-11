using System;
using _Project.Script.Generic;
using _Project.Script.Interface;
using _Project.Script.ScriptableObjects.Character;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Script.Character
{
    [RequireComponent(typeof(Collider), typeof(Animator), typeof(NavMeshAgent))]
    public abstract class CharacterObject : MonoBehaviour, IPoolingAble
    {
        protected static readonly int Move = Animator.StringToHash("Move");
        public Pool Pool { get; set; }
        
        [SerializeField] protected Collider mainCollider;
        [SerializeField] protected Animator mainAnimator;
        [SerializeField] protected NavMeshAgent mainAgent;

        protected virtual void Awake()
        {
            mainCollider ??= GetComponent<Collider>();
            mainAnimator ??= GetComponent<Animator>();
            mainAgent ??= GetComponent<NavMeshAgent>();
        }

        public virtual void OnCreated(CharacterData data)
        {
            
        }

        public virtual void OnSpawned()
        {
            
        }

        public virtual void OnDespawned()
        {
            
        }

        public void Release() => Pool.Release(gameObject);

        
        #if UNITY_EDITOR

        protected virtual void Reset()
        {
            mainCollider = GetComponent<Collider>();
            mainAnimator = GetComponent<Animator>();
            mainAgent = GetComponent<NavMeshAgent>();
        }

        #endif
        
    }
}
