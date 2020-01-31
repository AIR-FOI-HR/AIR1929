using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;

public class RunMonitor : MonoBehaviourPunCallbacks
{
    public GameObject[] crazyRunnerPrefabs;
    public GameObject startRaceButton;
    public GameObject waitingText;
    public GameObject countdownText;
    public bool raceStarted = false;
    int runnerIndex;

    private void Start()
    {
        countdownText.SetActive(false);
        runnerIndex = PlayerPrefs.GetInt("PlayerIndex");
        GameObject runner = null;
        startRaceButton.SetActive(false);
        if (PhotonNetwork.IsConnected)
        {
            if (NetworkedPlayer.localPlayerInstance == null)
            {
                runner = PhotonNetwork.Instantiate(crazyRunnerPrefabs[runnerIndex].name, new Vector2(0, 2), Quaternion.identity, 0);
            }
            if (PhotonNetwork.IsMasterClient)
            {
                startRaceButton.SetActive(true);
            }
            else
            {
                waitingText.SetActive(true);
            }
        }
        else
        {
            runner = Instantiate(crazyRunnerPrefabs[runnerIndex]);
            runner.transform.position = new Vector2(0, 4);

            StartGame();
        }
        runner.GetComponent<PlayerControls>().enabled = true;
        runner.GetComponent<CharacterController2D>().enabled = true;
        runner.transform.Find("Main Camera").gameObject.SetActive(true);
    }

    public void StartGame()
    {
        countdownText.SetActive(true);
        startRaceButton.SetActive(false);
        waitingText.SetActive(false);
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        countdownText.GetComponent<Text>().text = "";
        yield return new WaitForSeconds(1);
        countdownText.GetComponent<Text>().text = "3";
        yield return new WaitForSeconds(1);
        countdownText.GetComponent<Text>().text = "2";
        yield return new WaitForSeconds(1);
        countdownText.GetComponent<Text>().text = "1";
        yield return new WaitForSeconds(1);
        countdownText.SetActive(false);
        raceStarted = true;
        //runTime.Start();
    }
}
