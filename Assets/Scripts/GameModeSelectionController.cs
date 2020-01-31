using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Photon.Realtime;
using Photon.Pun;

public class GameModeSelectionController : MonoBehaviourPunCallbacks
{
    public GameObject playerSelectionController;
    public string[] maps;
    public Sprite[] thumbnails;
    public Text mapName;
    public Image mapImage;
    int currentId = 0;

    public string playerName;
    byte maxPlayersPerRoom = 2;
    bool isConnecting;
    public Text feedbackText;
    string gameVersion = "1";
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start()
    {
        //dohvati datoteku s informacijama igraca
        if (PlayerPrefs.HasKey("PlayerName"))
        {
            playerName = PlayerPrefs.GetString("PlayerName");
        }

        ShowMap();
    }

    /// <summary>
    /// Pokreni mapu za više igrača koja se proceduralno generira
    /// </summary>
    public void PlayMultiplayerGame()
    {
        //spajanje na server
        feedbackText.text = "";
        isConnecting = true;

        PhotonNetwork.NickName = playerName;
        if (PhotonNetwork.IsConnected)
        {
            feedbackText.text += "\nJoining room...";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            feedbackText.text += "\nConnecting...";
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    /// <summary>
    /// Pokreće mapu za jednog igrača koja je predefinirana
    /// </summary>
    public void PlaySingleplayerGame()
    {
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

    /// Network Callbacks

    public override void OnConnectedToMaster()
    {
        if (isConnecting)
        {
            feedbackText.text += "\nOnConnectedToMaster";
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        feedbackText.text += "\nFailed to join random room.";
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = this.maxPlayersPerRoom });
    }

    public override void OnDisconnected(DisconnectCause cause)
    {

        isConnecting = false;
    }

    public override void OnJoinedRoom()
    {
        feedbackText.text += "\nJoined room. Current count: " + PhotonNetwork.CurrentRoom.PlayerCount + " player(s).";
        PhotonNetwork.LoadLevel(maps[currentId]);
    }
}
