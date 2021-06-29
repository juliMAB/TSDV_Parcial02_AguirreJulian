using System;
using UnityEngine;

public class UI_TitlePart : MonoBehaviour
{

    Animator anim;

    public Action OnAnimationComplete;
    
    void AnimationComplete()
    {
        OnAnimationComplete();
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void StartGradient()
    {
        //anim.SetTrigger("Gradient");
    }

    public void FadeOut()
    {
        anim.SetTrigger("FadeOut");
    }

}
