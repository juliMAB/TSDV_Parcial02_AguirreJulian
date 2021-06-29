using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UI_LoadingScreen : MonoBehaviour
{

    [SerializeField] float fadeSpeed = .5f;
    [SerializeField] TextMeshProUGUI loadingText = null;
    [SerializeField] Image loadingBar = null;
    [SerializeField] Image loadingBarOverlay = null;
    [SerializeField] Image blackScreen = null;

    public void StartBarLoadingScreen()
    {
        StartCoroutine(UI_Generics.CoroutineFadeImage(loadingBar, true, fadeSpeed));
        StartCoroutine(UI_Generics.CoroutineFadeImage(loadingBarOverlay, true, fadeSpeed));
        StartCoroutine(UI_Generics.CoroutineFadeText(loadingText, true, fadeSpeed));
    }

    public void UpdateBarLoadingScreen(float loadingProgress)
    {
        int loadingVal = (int)(loadingProgress * 100);
        loadingText.text = loadingVal + "%";
        loadingBar.fillAmount = loadingProgress;
    }

    public void EndBarLoadingScreen()
    {
        StartCoroutine(UI_Generics.CoroutineFadeImage(loadingBar, false, fadeSpeed));
        StartCoroutine(UI_Generics.CoroutineFadeImage(loadingBarOverlay, false, fadeSpeed));
        StartCoroutine(UI_Generics.CoroutineFadeText(loadingText, false, fadeSpeed));
    }

    public void StartBlackScreenLoadingScreen()
    {
        StartCoroutine(UI_Generics.CoroutineFadeImage(blackScreen, true, fadeSpeed));
    }

    public void EndBlackScreenLoadingScreen()
    {
        StartCoroutine(UI_Generics.CoroutineFadeImage(blackScreen, false, fadeSpeed));
    }
}
