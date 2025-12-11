using _Project.Script.Behaviour.StateMachine.Native;
using _Project.Script.Manager;
using UnityEngine;

namespace _Project.Script.Character.HeroStates
{
    public class MergeState : State<HeroObject>
    {
        public override void Enter(HeroObject owner)
        {
            owner.MoveAnimate(false);
            owner.GrabAnimate(false);
            owner.FallAnimate(false);

            var effect = DataManager.Instance.mergeParticle;
            Object.Instantiate(effect, owner.transform.position, Quaternion.identity);
            
            owner.Release();
        }
    }
}
