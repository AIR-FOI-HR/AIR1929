using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndRacePanel : MonoBehaviour
{
    public void onClickReturnToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void onClickTryAgain()
    {
        SceneManager.LoadScene("WinterRun (Map)");
    }
}
