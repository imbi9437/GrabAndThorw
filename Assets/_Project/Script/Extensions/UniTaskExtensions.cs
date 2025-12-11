using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Script.Extensions
{
    public static class UniTaskExtensions
    {
        public static async UniTaskVoid WaitAnimateEnd(this Animator animator, string name, Action callback, CancellationToken token)
        {
            await UniTask.Yield();
            await UniTask.WaitUntil(() => animator.IsEndAnimate(name), cancellationToken: token);
            
            callback?.Invoke();
        }
        
        private static bool IsEndAnimate(this Animator animator, string name, float time = 1f)
        {
            var curState = animator.GetCurrentAnimatorStateInfo(0);
            return curState.normalizedTime >= time && curState.IsName(name);
        }
    }
}
