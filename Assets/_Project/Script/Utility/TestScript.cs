using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Script.Character;
using _Project.Script.Manager;
using _Project.Script.ScriptableObjects.Character;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class TestScript : MonoBehaviour
{
    public Button button;

    private void Start()
    {
        button.onClick.AddListener(Sample);
    }

    private void Sample()
    {
        
    }
}
