using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject settings;
    public void Play()
    {
        //   CanvasGroup canvas = settings.GetComponent<CanvasGroup>();
        Canvas canvas = settings.GetComponent<Canvas>();
        canvas.enabled = false;
    }
    
    public void onSettings()
    {
        settings.SetActive(true);
    }
    public void onBack()
    {
        settings.SetActive(false);
    }
}
