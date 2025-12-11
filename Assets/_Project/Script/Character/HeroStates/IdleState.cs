using System.Threading;
using _Project.Script.Behaviour.StateMachine.Native;
using _Project.Script.Extensions;
using _Project.Script.Manager;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace _Project.Script.Character.HeroStates
{
    public class IdleState : State<HeroObject>
    {
        public override void Enter(HeroObject owner)
        {
            owner.FallAnimate(false);
            owner.GrabAnimate(false);
            owner.MoveAnimate(false);
            
            owner.ToggleAgent(true);
            
            owner.transform.position.GetNearestPoint(NavMeshExtensions.MonsterLayer,out var point);
            owner.SetPoint(point);
        }

        public override void Update(HeroObject owner)
        {
            if (owner.IsSameAnimate("Idle", 0f) == false) return;
            owner.TryCheckMerge();
        }

        public override void Exit(HeroObject owner)
        {
            
        }
    }
}
