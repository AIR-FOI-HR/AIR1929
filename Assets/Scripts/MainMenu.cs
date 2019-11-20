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
        Debug.Log("kliknut settings");
        settings.SetActive(true);
        mainMenu.SetActive(false);
    }
    public void onBack()
    {
        Debug.Log("kliknut back");
        mainMenu.SetActive(true);
        settings.SetActive(false);
    }
}
