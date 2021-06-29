using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Game : MonoBehaviour
{
    [Header("General")]
    [SerializeField] float fadeSpeed = 1f;

    [Header("Gameplay UI")]
    [SerializeField] Image bombImage = null;
    [SerializeField] TextMeshProUGUI scoreText = null;
    [SerializeField] Button pauseButton = null;
    [SerializeField] Image energyBar = null;
    [SerializeField] Image energyBarOverlay = null;
    [SerializeField] List<Image> generalImages = null;
    [SerializeField] List<TextMeshProUGUI> generalTexts = null;

    [Header("Pause Menu")]
    [SerializeField] TextMeshProUGUI PauseText = null;
    [SerializeField] List<Button> pauseMenuButtons = null;
    [SerializeField] List<Image> pauseMenuImages = null;


    private void Start()
    {
        StartUI();

        if (AudioManager.Get() != null)
        {
            AudioManager.Get().StopAllSFX();
            AudioManager.Get().Play("Game");
        }
    }


    void StartUI()
    {
        StartCoroutine(UI_Generics.CoroutineFadeImage(bombImage, true, fadeSpeed));
        StartCoroutine(UI_Generics.CoroutineFadeImage(energyBar, true, fadeSpeed));
        StartCoroutine(UI_Generics.CoroutineFadeImage(energyBarOverlay, true, fadeSpeed));
        StartCoroutine(UI_Generics.CoroutineFadeText(scoreText, true, fadeSpeed));
        foreach (var image in generalImages)
        {
            StartCoroutine(UI_Generics.CoroutineFadeImage(image, true, fadeSpeed));
        }
        foreach (var text in generalTexts)
        {
            StartCoroutine(UI_Generics.CoroutineFadeText(text, true, fadeSpeed));
        }
        pauseButton.interactable = true;
        StartCoroutine(UI_Generics.CoroutineFadeImage(pauseButton.GetComponent<Image>(), true, fadeSpeed));
        StartCoroutine(UI_Generics.CoroutineFadeText(pauseButton.GetComponentInChildren<TextMeshProUGUI>(), true, fadeSpeed));
    }

    public void StopUI()
    {
        pauseButton.interactable = false;
        foreach (Button button in pauseMenuButtons)
        {
            button.interactable = false;
        }
    }

    public void PauseStateChanged(bool paused)
    {
        StopAllCoroutines();
        StartCoroutine(UI_Generics.CoroutineFadeText(PauseText, paused, fadeSpeed, paused));
        foreach (Button button in pauseMenuButtons)
        {
            button.interactable = paused;
            StartCoroutine(UI_Generics.CoroutineFadeImage(button.GetComponent<Image>(), paused, fadeSpeed, paused));
            StartCoroutine(UI_Generics.CoroutineFadeText(button.GetComponentInChildren<TextMeshProUGUI>(), paused, fadeSpeed, paused));
        }
        foreach (Image image in pauseMenuImages)
        {
            StartCoroutine(UI_Generics.CoroutineFadeImage(image, paused, fadeSpeed, paused));
        }
        pauseButton.interactable = !paused;
        StartCoroutine(UI_Generics.CoroutineFadeImage(pauseButton.GetComponent<Image>(), !paused, fadeSpeed, paused));
        StartCoroutine(UI_Generics.CoroutineFadeText(pauseButton.GetComponentInChildren<TextMeshProUGUI>(), !paused, fadeSpeed, paused));
    }

    void ScoreUpdate(int score)
    {
        scoreText.text = "Score: " + score;
    }

}
