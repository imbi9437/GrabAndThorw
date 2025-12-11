using _Project.Script.Behaviour.StateMachine.Native;
using UnityEngine;

namespace _Project.Script.Character.HeroStates
{
    public class CombatState : State<HeroObject>
    {
        private readonly float _cooldown;
        private float _curCooldown;

        public CombatState(float cooldown) => _cooldown = cooldown;
        
        public override void Enter(HeroObject owner)
        {
            _curCooldown = 0;
            owner.ToggleObstacle(true);
        }

        public override void Update(HeroObject owner)
        {
            if (_curCooldown < _cooldown)
            {
                _curCooldown += Time.deltaTime;
                return;
            }
            
            bool isFind = owner.FindMonster();
            if (isFind == false) return;
            
            owner.AttackMonster();
            owner.AttackAnimate();
        }

        public override void Exit(HeroObject owner)
        {
            owner.ToggleObstacle(false);
        }
    }
}
