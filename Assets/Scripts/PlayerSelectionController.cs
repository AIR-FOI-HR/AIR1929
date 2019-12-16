using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectionController : MonoBehaviour
{
    GameObject currentPlayer;
    public GameObject[] listOfPlayers;
    public int currentId;
    public Image shownPlayer;

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
        }
    }

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
        }
    }

    public void ShowPlayer()
    {
        shownPlayer.sprite = listOfPlayers[currentId].GetComponent<SpriteRenderer>().sprite;
        shownPlayer.SetNativeSize();
    }

    public void SetCurrentPlayer()
    {
        if (listOfPlayers.Length != 0)
        {
            currentPlayer = Instantiate(listOfPlayers[currentId], new Vector2(5, 3), Quaternion.identity);
            currentPlayer.SetActive(false);
        }
    }

    public GameObject ReturnCurrentPlayer()
    {
        return currentPlayer;
    }
}
