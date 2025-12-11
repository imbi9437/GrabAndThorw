using _Project.Script.Behaviour.StateMachine.Native;
using UnityEngine;

namespace _Project.Script.Character.HeroStates
{
    public class FallState : State<HeroObject>
    {
        public override void Enter(HeroObject owner)
        {
            owner.transform.rotation = Quaternion.identity;
            
            owner.FallAnimate(true);
            owner.ToggleAgent(false);
        }

        public override void Exit(HeroObject owner)
        {
            owner.FallAnimate(false);
        }
    }
}
