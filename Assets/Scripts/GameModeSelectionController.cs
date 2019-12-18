using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameModeSelectionController : MonoBehaviour
{
    public GameObject playerSelectionController;
    public string[] maps;
    public Sprite[] thumbnails;
    public Text mapName;
    public Image mapImage;
    int currentId = 0;

    void Start()
    {
        ShowMap();
    }

    /// <summary>
    /// Pokreni mapu za više igrača koja se proceduralno generira
    /// </summary>
    public void PlayMultiplayerGame()
    {
        playerSelectionController.GetComponent<PlayerSelectionController>().ReturnCurrentPlayer().SetActive(true);
        SceneManager.LoadScene(maps[currentId]);
    }

    /// <summary>
    /// Pokreće mapu za jednog igrača koja je predefinirana
    /// </summary>
    public void PlaySingleplayerGame()
    {
        playerSelectionController.GetComponent<PlayerSelectionController>().ReturnCurrentPlayer().SetActive(true);
        SceneManager.LoadScene(maps[currentId]);
    }

    /// <summary>
    /// Prijelaz na sljedeću mapu
    /// </summary>
    public void ShowNextMap()
    {
        if (maps.Length != 0)
        {
            if (currentId == maps.Length - 1)
            {
                currentId = 0;
            }
            else
            {
                currentId++;
            }

            ShowMap();
        }
    }

    /// <summary>
    /// Prijelaz na prethodnu mapu
    /// </summary>
    public void ShowPreviousMap()
    {
        if (maps.Length != 0)
        {
            if (currentId == 0)
            {
                currentId = maps.Length - 1;
            }
            else
            {
                currentId--;
            }

            ShowMap();
        }
    }

    /// <summary>
    /// Prikaži naziv i sliku mape
    /// </summary>
    void ShowMap()
    {
        mapName.text = maps[currentId];
        mapImage.sprite = thumbnails[currentId];
    }
}
