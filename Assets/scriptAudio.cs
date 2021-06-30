using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scriptAudio : MonoBehaviour
{
    [SerializeField] Button button;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(AudioManager.Get().StopAllSFX);
    }

}
