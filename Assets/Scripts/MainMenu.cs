using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu, settings;

    public void Play()
    {
        Canvas canvas = settings.GetComponent<Canvas>();
        canvas.enabled = false;
    }

    public void onPlay()
    {
        SceneManager.LoadScene("WinterRun (Map)");
    }

    public void onSettings()
    {
        settings.SetActive(true);
        mainMenu.SetActive(false);
    }
    public void onBack()
    {
        mainMenu.SetActive(true);
        settings.SetActive(false);
    }
}
