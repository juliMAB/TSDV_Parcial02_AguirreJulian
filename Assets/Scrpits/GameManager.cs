using MonoBehaviourSingletonScript;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    int ScoreInMach;
    public Action<int> OnGameOver;
    private void Start()
    {
        OnGameOver += GetScore;
    }

    void GetScore(int a)
    {
        ScoreInMach = a;
    }

}
