using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_EndScene : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText = null;
    [SerializeField] TextMeshProUGUI energyLeftText = null;
    [SerializeField] TextMeshProUGUI enemiesDestroyedText = null;
    [SerializeField] TextMeshProUGUI shotsMadeText = null;
    [SerializeField] TextMeshProUGUI accuracyText= null;

    private void Awake()
    {
        UI_GameStats currentGameStats = LoaderManager.Get().GetCurrentGameStats();
        scoreText.text = "Score: " + currentGameStats.score;
        energyLeftText.text = "Energy left: " + currentGameStats.energyLeft;
        enemiesDestroyedText.text = "Enemies Destroyed: " + currentGameStats.enemiesDestroyed;
        shotsMadeText.text = "Shots Made: " + currentGameStats.shotsMade;
        accuracyText.text = "Accuracy: " + currentGameStats.accuracy + "%";
    }

    public void BackToMenu()
    {
        LoaderManager.Get().LoadSceneAsyncWithBlackScreen("Main Menu");
    }

}
