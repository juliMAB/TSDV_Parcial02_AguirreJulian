using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_MainMenu : MonoBehaviour
{

    //[SerializeField] float fadeSpeed = 1f;
    [SerializeField] UI_TitleController titleController = null;
    [SerializeField] List<GameObject> mainMenuObjects = null;
    [SerializeField] List<GameObject> creditsObjects = null;
    
    void Start()
    {
        titleController.TitleComplete += StartMainMenu;
    }    

    public void PlayGame()
    {
        StopAllCoroutines();
        LoaderManager.Get().FadeCanvasObjects(mainMenuObjects, false);
        LoaderManager.Get().FadeCanvasObjects(creditsObjects, false);
        titleController.FadeTitle();
        LoaderManager.Get().LoadSceneAsyncWithBlackScreen("Game");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ShowCredits()
    {
        StopAllCoroutines();
        LoaderManager.Get().FadeCanvasObjects(mainMenuObjects, false);
        LoaderManager.Get().FadeCanvasObjects(creditsObjects, true);
    }

    public void StartMainMenu()
    {
        StopAllCoroutines();
        LoaderManager.Get().FadeCanvasObjects(mainMenuObjects, true);
        LoaderManager.Get().FadeCanvasObjects(creditsObjects, false);

        if (AudioManager.Get() != null)
        {
            AudioManager.Get().StopAllSFX();
            AudioManager.Get().Play("MainMenu");
        }
    }

}
