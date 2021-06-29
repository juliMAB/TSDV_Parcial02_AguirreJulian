using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UI_Levels : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title = null;
    [SerializeField] List<GameObject> levels = null;
    [SerializeField] float fadeSpeed = 1f;

    private void Start()
    {
        if (AudioManager.Get() != null)
        {
            AudioManager.Get().StopAllSFX();
            AudioManager.Get().Play("Loading");
        }
    }

    public void LoadLevel(int levelNumber)
    {
        LoaderManager.Get().LoadSceneAsyncWithLoadingBar("Game Scene");
        StartCoroutine(UI_Generics.CoroutineFadeText(title, false, fadeSpeed));
        LoaderManager.Get().FadeCanvasObjects(levels, false);
        LoaderManager.Get().SetCurrentLevel(levelNumber);
        BackgroundController.Get().StartBackground();
    }
}
