using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using _Project.Script.Behaviour.StateMachine.Native;
using _Project.Script.Character.HeroStates;
using _Project.Script.Extensions;
using _Project.Script.Interface;
using _Project.Script.Manager;
using _Project.Script.ScriptableObjects.Character;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace _Project.Script.Character
{
    public enum HeroState
    {
        Idle,
        Walk,
        Combat,
        Grab,
        Fall,
        Merge,
    }
    
    [RequireComponent(typeof(XRGrabInteractable), typeof(NavMeshObstacle))]
    public class HeroObject : CharacterObject, IHeroStateAction
    {
        private static readonly int Grabbed = Animator.StringToHash("Grabbed");
        private static readonly int Fall = Animator.StringToHash("Fall");
        private static readonly int Attack = Animator.StringToHash("Attack");

        [SerializeField] private Rigidbody mainRigidbody;
        [SerializeField] private XRGrabInteractable grabInteractable;
        [SerializeField] private NavMeshObstacle navMeshObstacle;
        
        private StateMachine<HeroObject> _fsm;
        private Dictionary<HeroState, State<HeroObject>> _stateDic;
        
        private HeroData _heroData;

        private Vector3 _point;

        private CancellationTokenSource _cts;
        private Collider[] _enemyCols;
        private IHitAble _hitTarget;
        
        public int uid => _heroData.uid;
        
        #region Unity Message Funcion

        protected override void Awake()
        {
            base.Awake();
            
            mainRigidbody ??= GetComponent<Rigidbody>();
            grabInteractable ??= GetComponent<XRGrabInteractable>();
            _enemyCols = new Collider[10];
        }
        
        private void Update()
        {
            _fsm?.Update();
        }
        
        private void OnCollisionEnter(Collision other)
        {
            if (grabInteractable.isSelected) return;
            if (other.collider.gameObject.layer != LayerMask.NameToLayer("Ground")) return;
            
            ChangeState(HeroState.Idle);
        }

        #endregion

        #region ObjectPooling Funcion

        public override void OnCreated(CharacterData data)
        {
            if (data is not HeroData heroData) return;
            
            Initialize(heroData);
            ToggleAgent(false);
        }

        public override void OnSpawned()
        {
            grabInteractable.selectEntered.AddListener(GrabEnter);
            grabInteractable.selectExited.AddListener(GrabExit);
        }
        public override void OnDespawned()
        {
            grabInteractable.selectEntered.RemoveListener(GrabEnter);
            grabInteractable.selectExited.RemoveListener(GrabExit);
        }
        
        #endregion

        private void Initialize(HeroData data)
        {
            _heroData = data;
            InitStateMachine();
        }

        private void InitStateMachine()
        {
            _fsm ??= new StateMachine<HeroObject>(this);
            _stateDic ??= new Dictionary<HeroState, State<HeroObject>>();

            _stateDic.TryAdd(HeroState.Fall, new FallState());
            _stateDic.TryAdd(HeroState.Grab, new GrabState());
            _stateDic.TryAdd(HeroState.Idle, new IdleState());
            _stateDic.TryAdd(HeroState.Walk, new WalkState());
            _stateDic.TryAdd(HeroState.Combat, new CombatState(1/_heroData.attackSpeed));
            _stateDic.TryAdd(HeroState.Merge, new MergeState());
        }

        private void GrabEnter(SelectEnterEventArgs args) => ChangeState(HeroState.Grab);
        private void GrabExit(SelectExitEventArgs args) => ChangeState(HeroState.Fall);

        public void ToggleAgent(bool isOn)
        {
            mainRigidbody.isKinematic = isOn;
            mainRigidbody.useGravity = isOn == false;
            
            mainAgent.enabled = isOn;
            
            if (isOn) mainAgent.Warp(transform.position);
        }

        public void ToggleObstacle(bool isOn)
        {
            mainAgent.enabled = isOn == false;
            navMeshObstacle.enabled = isOn;
        }

        public void ChangeState(HeroState state) => _fsm.ChangeState(_stateDic[state]);

        #region StateAction Funcion

        public void GrabAnimate(bool isGrab) => mainAnimator.SetBool(Grabbed, isGrab);
        public void FallAnimate(bool isFall) => mainAnimator.SetBool(Fall, isFall);
        public void MoveAnimate(bool isMove) => mainAnimator.SetBool(Move, isMove);

        public void AttackAnimate() => mainAnimator.SetTrigger(Attack);
        
        public void MoveToPoint() => mainAgent.SetDestination(_point);
        public bool IsSameAnimate(string stateName, float time) => mainAnimator.IsCurrentAnimationRequire(stateName, time);
        
        public bool FindMonster()
        {
            _hitTarget = null;
            int enemyLayer = 1 << LayerMask.NameToLayer("Enemy");
            int count = Physics.OverlapSphereNonAlloc(transform.position, _heroData.attackRange, _enemyCols, enemyLayer);
            
            if (count <= 0) return false;

            var nearest = _enemyCols.GetNearest(transform);
            nearest.TryGetComponent(out _hitTarget);

            return _hitTarget != null;
        }
        public void AttackMonster()
        {
            _hitTarget.Hit(_heroData.attackDamage);
        }

        public void TryCheckMerge()
        {
            DefenceManager.Instance.CheckMerge(this);
            ChangeState(HeroState.Walk);
        }

        #endregion


        public void SetPoint(Vector3 pos) => _point = pos;
        public bool IsCompleteMove() => mainAgent.IsCompleteMove();
        public bool IsCompleteCalcPath() => mainAgent.IsCompleteComputePath();

#if UNITY_EDITOR

        protected override void Reset()
        {
            base.Reset();
            mainRigidbody =  GetComponent<Rigidbody>();
            grabInteractable = GetComponent<XRGrabInteractable>();
            navMeshObstacle = GetComponent<NavMeshObstacle>();
        }

        #endif
        
    }
}
