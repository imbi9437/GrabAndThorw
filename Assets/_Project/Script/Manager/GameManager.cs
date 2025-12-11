using System;
using _Project.Script.Generic;
using Script.Generic;
using UnityEngine;

namespace _Project.Script.Manager
{
    public class GameManager : MonoSingleton<GameManager>
    {
        private void Start()
        {
            EventHub.Instance.RegisterEvent<LifeChangeEvent>(DefeatEnd);
        }

        private void OnDestroy()
        {
            EventHub.Instance?.UnRegisterEvent<LifeChangeEvent>(DefeatEnd);
        }

        private void DefeatEnd(LifeChangeEvent eventValue)
        {
            if (eventValue.newLife > 0) return;
            
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }
    }
}
