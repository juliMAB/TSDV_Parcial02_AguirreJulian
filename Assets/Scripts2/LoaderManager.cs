using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class LoaderManager : MonoBehaviourSingleton<LoaderManager>
{

    [SerializeField] UI_LoadingScreen loadingScreen = null;
    [Header("Loading screen with bar")]
    [SerializeField] float minTimeToLoadBar = 2;
    float loadingProgress = 0f;
    float timeLoading = 0f;
    [Header("Loading screen with black screen")]
    [SerializeField] float minTimeToLoadBlackScreen = 1;
    [SerializeField] float fadeSpeed = 1f;

    int currentLevel = 1;

    UI_GameStats gameStats = new UI_GameStats();

    public void LoadSceneAsyncWithLoadingBar(string sceneName)
    {
        StartCoroutine(AsynchronousLoadWithFake(sceneName));
    }

    public void LoadSceneAsyncWithBlackScreen(string sceneName)
    {
        StartCoroutine(AsynchronousLoadWithBlackScreen(sceneName));
    }

    IEnumerator AsynchronousLoadWithFake(string scene)
    {
        loadingProgress = 0;
        timeLoading = 0;

        yield return null;

        AsyncOperation ao = SceneManager.LoadSceneAsync(scene);
        loadingScreen.StartBarLoadingScreen();
        ao.allowSceneActivation = false;

        while (!ao.isDone)
        {
            timeLoading += Time.deltaTime;
            loadingProgress = ao.progress + 0.1f;
            loadingProgress = loadingProgress * timeLoading / minTimeToLoadBar;

            loadingScreen.UpdateBarLoadingScreen(loadingProgress);

            // Se completo la carga
            if (loadingProgress >= 1)
                ao.allowSceneActivation = true;

            yield return null;
        }
        loadingScreen.EndBarLoadingScreen();
    }

    IEnumerator AsynchronousLoadWithBlackScreen(string scene)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(scene);
        loadingScreen.StartBlackScreenLoadingScreen();
        ao.allowSceneActivation = false;

        yield return new WaitForSecondsRealtime(minTimeToLoadBlackScreen);

        ao.allowSceneActivation = true;
        loadingScreen.EndBlackScreenLoadingScreen();
    }

    public UI_GameStats GetCurrentGameStats()
    {
        return gameStats;
    }

    public void SetCurrentGameStats(UI_GameStats newGamestats)
    {
        gameStats = newGamestats;
    }

    public void FadeCanvasObjects(List<GameObject> list, bool fadeIn)
    {
        foreach (var item in list)
        {
            if (item.GetComponent<Button>())
            {
                item.GetComponent<Button>().interactable = fadeIn;
                StartCoroutine(UI_Generics.CoroutineFadeText(item.GetComponentInChildren<TextMeshProUGUI>(), fadeIn, fadeSpeed));
                StartCoroutine(UI_Generics.CoroutineFadeImage(item.GetComponent<Image>(), fadeIn, fadeSpeed));
            }
            else if (item.GetComponent<Image>())
            {
                StartCoroutine(UI_Generics.CoroutineFadeImage(item.GetComponent<Image>(), fadeIn, fadeSpeed));
            }
            else if (item.GetComponent<TextMeshProUGUI>())
            {
                StartCoroutine(UI_Generics.CoroutineFadeText(item.GetComponent<TextMeshProUGUI>(), fadeIn, fadeSpeed));
            }
        }
    }

    public void SetCurrentLevel(int levelNumber)
    {
        currentLevel = levelNumber;
    }

    public int GetCurrentLevel()
    {
        return currentLevel - 1;
    }
}