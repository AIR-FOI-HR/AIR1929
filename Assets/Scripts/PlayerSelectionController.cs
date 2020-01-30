using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectionController : MonoBehaviour
{
    public GameObject[] listOfPlayers;
    public int currentId = 0;
    int selectedId = 0;
    public Image shownPlayer;
    public GameObject checkmark;

    void Start()
    {
        SetCurrentPlayer();
        if (PlayerPrefs.HasKey("PlayerIndex"))
        {
            currentId = PlayerPrefs.GetInt("PlayerIndex");
            PlayerPrefs.SetInt("PlayerIndex", currentId);
        }
        else
        {
            PlayerPrefs.SetInt("PlayerIndex", currentId);
        }
        ShowCheckmark();
    }

    /// <summary>
    /// Prijelaz na sljedećeg lika u izborniku Choose player
    /// </summary>
    public void ShowNextPlayer()
    {
        if (listOfPlayers.Length != 0)
        {
            if (currentId == listOfPlayers.Length - 1)
            {
                currentId = 0;
            }
            else
            {
                currentId++;
            }

            ShowPlayer();
            ShowCheckmark();
        }
    }

    /// <summary>
    /// Prijelaz na prethodnog lika u izborniku Choose player
    /// </summary>
    public void ShowPreviousPlayer()
    {
        if (listOfPlayers.Length != 0)
        {
            if (currentId == 0)
            {
                currentId = listOfPlayers.Length - 1;
            }
            else
            {
                currentId--;
            }

            ShowPlayer();
            ShowCheckmark();
        }
    }

    /// <summary>
    /// Prikaz slike lika u izborniku
    /// </summary>
    public void ShowPlayer()
    {
        shownPlayer.sprite = listOfPlayers[currentId].GetComponent<SpriteRenderer>().sprite;
        shownPlayer.SetNativeSize();
    }

    /// <summary>
    /// Postavi trenutnog lika kao lika kojim će se igrač utrkivati
    /// </summary>
    public void SetCurrentPlayer()
    {
        if (listOfPlayers.Length != 0)
        {
            selectedId = currentId;
            ShowCheckmark();
            PlayerPrefs.SetInt("PlayerIndex", currentId);
        }
    }

    /// <summary>
    /// Prikaz kvačice kako bi se označilo koji lik je trenutno odabran
    /// </summary>
    void ShowCheckmark()
    {
        if (currentId == selectedId)
        {
            checkmark.SetActive(true);
        }
        else
        {
            checkmark.SetActive(false);
        }
    }
}
