using UnityEngine;

namespace _Project.Script.Interface
{
    public interface IHitAble
    {
        public float Hp { get; }
        public void Hit(float damage);
    }
}
