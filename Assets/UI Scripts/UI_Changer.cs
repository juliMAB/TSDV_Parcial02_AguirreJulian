using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Changer : MonoBehaviour
{
    public void GoToMenu()
    {
        LoaderManager.Get().GoTo("Menu");
    }
}
