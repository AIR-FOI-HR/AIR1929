using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class FlagController : MonoBehaviour
{
    public GameObject EndRacePanel;
    private Stopwatch runTime = new Stopwatch();
    public Text RunTimeInformationText;
    bool FirstCollision = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Countdown());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {


        //Checking if a run is ended
        if (FirstCollision == false)
        {
            if (collider.gameObject.tag == "Player")
            {
                //1. Open panel with run information
                //2. IDLE Player motivation (IDLE)
                Debug.Log("Sudar objekta sa zastavom.");

                string runTimeString = runTime.Elapsed.Seconds + " seconds.";
                Debug.Log(runTimeString);

                RunTimeInformationText.text = "Run Information\nRun time: " + runTimeString + "";

                EndRacePanel.SetActive(true);

            }
            FirstCollision = true;
        }
    }
    void ReturnMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(4);
        runTime.Start();
    }
}
