using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu, settings, choosePlayer, chooseGameMode, playSelectionController;

    void Start()
    {
        if (!PlayerPrefs.HasKey("Controls"))
        {
            PlayerPrefs.SetFloat("Controls", 0.0f);
        }
        if (!PlayerPrefs.HasKey("PlayerName"))
        {
            PlayerPrefs.SetString("PlayerName", "Guest" + Random.Range(1000, 9999).ToString());
        }
    }

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

    public void OpenAchievements()
    {
        SceneManager.LoadScene("Achievements");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
