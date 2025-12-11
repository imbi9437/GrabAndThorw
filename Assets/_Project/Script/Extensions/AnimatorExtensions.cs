using UnityEngine;

namespace _Project.Script.Extensions
{
    public static class AnimatorExtensions
    {
        public static bool IsCurrentAnimationRequire(this Animator animator, string name, float time = 1f)
        {
            var state = animator.GetCurrentAnimatorStateInfo(0);
            return state.IsName(name) && state.normalizedTime >= time;
        }
    }
}
