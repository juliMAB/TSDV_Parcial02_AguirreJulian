using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_TitleController : MonoBehaviour
{
    [SerializeField] List<UI_TitlePart> titleParts = null;
    int currentIndex = 0;

    public Action TitleComplete;

    private void Start()
    {
        foreach (var t in titleParts)
        {
            t.OnAnimationComplete += NextTitle;
        }
    }

    private void NextTitle()
    {
        currentIndex++;
        if (currentIndex < titleParts.Count)
        {
            titleParts[currentIndex].gameObject.GetComponent<Animator>().enabled = true;
        }
        else
        {
            foreach (var t in titleParts)
            {
                t.StartGradient();
            }
            TitleComplete();
        }
    }

    public void FadeTitle()
    {
        foreach (var title in titleParts)
        {
            title.FadeOut();
        }
    }
}
