using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu, settings, editPlayer, playSelectionController;

    public void Play()
    {
        Canvas canvas = settings.GetComponent<Canvas>();
        canvas.enabled = false;
    }

    public void onPlay()
    {
        playSelectionController.GetComponent<PlayerSelectionController>().ReturnCurrentPlayer().SetActive(true);
        SceneManager.LoadScene("WinterRun (Map)");
    }

    public void onSettings()
    {
        settings.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void onEditPlayer()
    {
        editPlayer.SetActive(true);
        mainMenu.SetActive(false);
        playSelectionController.GetComponent<PlayerSelectionController>().ShowPlayer();
    }
    public void onBack()
    {
        mainMenu.SetActive(true);
        settings.SetActive(false);
        editPlayer.SetActive(false);
    }
}
