using System;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] UI_Game ui_Game = null;

    static public GameplayManager instance;

    bool gamePaused = false;
    UI_GameStats currentGameStats = new UI_GameStats();

    public Action<int> OnScoreChanged;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void PausePressed()
    {
        gamePaused = !gamePaused;
        Time.timeScale = gamePaused ? 0 : 1;
        ui_Game.PauseStateChanged(gamePaused);
    }

    void PlayerDestroyed()
    {
        if (currentGameStats.shotsMade > 0)
        {
            
            if (currentGameStats.accuracy > 100)
            {
                currentGameStats.accuracy = 100;
            }
        }
        LoaderManager.Get().SetCurrentGameStats(currentGameStats);
        LoaderManager.Get().LoadSceneAsyncWithBlackScreen("End");
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;
        //ui_Game.StopUI();
        LoaderManager.Get().LoadSceneAsyncWithBlackScreen("Menu");
    }

    void OnPlayerShot()
    {
        currentGameStats.shotsMade++;
    }

    public void AddScore(int _score)
    {
        currentGameStats.score += _score;
        currentGameStats.enemiesDestroyed++;
        OnScoreChanged(currentGameStats.score);
    }
}