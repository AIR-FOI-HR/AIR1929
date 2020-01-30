using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu, settings, choosePlayer, chooseGameMode, playSelectionController;

    public void OpenSettings()
    {
        settings.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void OpenChoosePlayer()
    {
        choosePlayer.SetActive(true);
        mainMenu.SetActive(false);
        playSelectionController.GetComponent<PlayerSelectionController>().ShowPlayer();
    }

    public void OpenChooseGameMode()
    {
        chooseGameMode.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        mainMenu.SetActive(true);
        settings.SetActive(false);
        choosePlayer.SetActive(false);
        chooseGameMode.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
