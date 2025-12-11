using _Project.Script.Behaviour.StateMachine.Native;
using _Project.Script.Manager;
using Unity.VisualScripting;
using UnityEngine;

namespace _Project.Script.Character.HeroStates
{
    public class GrabState : State<HeroObject>
    {
        public override void Enter(HeroObject owner)
        {
            owner.GrabAnimate(true);
            owner.ToggleAgent(false);
        }

        public override void Exit(HeroObject owner)
        {
            owner.GrabAnimate(false);
        }
    }
}