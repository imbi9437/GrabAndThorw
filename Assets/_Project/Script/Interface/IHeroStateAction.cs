using System.Threading;

namespace _Project.Script.Interface
{
    public interface IHeroStateAction
    {
        public void GrabAnimate(bool isGrab);
        public void FallAnimate(bool isFall);
        public void MoveAnimate(bool isMove);
        public void AttackAnimate();

        public void MoveToPoint();
        public bool FindMonster();
        public void AttackMonster();
    }
}
