using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using Assets.Scripts.ERA;
using System.Linq;
using System;
using System.IO;
using Assets.Scripts;

/// <summary>
/// Klasa koja obavlja operacije povezane sa kolizijom objekta sa zastavicom
/// </summary>
public class FlagController : MonoBehaviour
{
    public GameObject StartGamePanel;
    public GameObject EndRacePanel;
    private Stopwatch runTime = new Stopwatch();
    public GameObject CountdownText;
    public GameObject RunTimeInformationText;
    public GameObject Leaderboard;
    public GameObject ProgressBar;
    bool FirstCollision = false;
    public DateTime RunDate;
    public string CurrentMap;
    public Slider volumeSlider;
    private string localPathForReadingWritingData;

    // Start is called before the first frame update
    void Start()
    {
        ///Postavljanje datuma i vremena utrke
        this.RunDate = DateTime.Now;
        ///Dohvaćanje mape trke
        this.CurrentMap = SceneManager.GetActiveScene().name;
        //Postavljanje rute za spremanje lokalnih podataka
        this.localPathForReadingWritingData = Application.persistentDataPath + "/score.txt";

        StartGamePanel = GameObject.Find("StartRacePanel");
        EndRacePanel = GameObject.Find("EndRacePanel");
        Leaderboard = GameObject.Find("LeaderboardText");
        RunTimeInformationText = GameObject.Find("RunInformationText");
        CountdownText = GameObject.Find("CountdownText");
        ProgressBar = GameObject.Find("ProgressBar");
        EndRacePanel.SetActive(false);

        StartCoroutine(Countdown());
    }

    // Update is called once per frame
    void Update()
    {
        if (ProgressBar.activeSelf && ProgressBar.GetComponent<Slider>().value <= 1)
            ProgressBar.GetComponent<Slider>().value = GameObject.FindGameObjectWithTag("Player").transform.position.x / transform.position.x;
    }
    /// <summary>
    /// Funkcija koja provjerava da li je završena trka te zove funkciju za dohvacanje i upisivanje podataka lokalno
    /// </summary>
    /// <param name="collider"></param>
    public void OnTriggerEnter2D(Collider2D collider)
    {
        //Checking if a run is ended
        if (FirstCollision == false)
        {
            if (collider.gameObject.tag == "Player")
            {
                string characterName = collider.name;

                //1. Open panel with run information
                //2. IDLE Player motivation (IDLE)
                Debug.Log("Sudar objekta sa zastavom.");


                float runTimeFloat = (float)runTime.Elapsed.TotalMilliseconds / 1000;
                string runTimeString = runTimeFloat.ToString("0.00");
                Debug.Log(runTimeString);

                //Zapisi podatke u Log
                List<Score> listOfScores = ReadLocalData();
                WriteResultToLocalFile(listOfScores, runTimeFloat, characterName);

                //Leaderboard.text = Application.persistentDataPath;
                RunTimeInformationText.GetComponent<Text>().text = "Run time: " + runTimeString + "";

                EndRacePanel.SetActive(true);

            }
            FirstCollision = true;
        }
    }
    public void ReturnMainMenu()
    {
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        SceneManager.LoadScene("Menu");
    }

    public void TryAgain()
    {
        //this code could work with additional 8 hours of work

        /*GameObject player = GameObject.FindGameObjectWithTag("Player");
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        int sceneindex = SceneManager.GetActiveScene().buildIndex;
        GameObject newplayer = Instantiate(player, new Vector2(0, 1), Quaternion.identity);
        DontDestroyOnLoad(newplayer);
        SceneManager.LoadScene(sceneindex);*/
    }

    /// <summary>
    /// Citanje podataka iz lokalne datoteke
    /// </summary>
    /// <returns></returns>
    List<Score> ReadLocalData()
    {
        List<Score> listOfScores = new List<Score>();
        Score[] arrayOfScores = listOfScores.ToArray();
        ///Provjeri da li postoji path tj. file

        if (File.Exists(this.localPathForReadingWritingData))
        {
            string dataJsonString = File.ReadAllText(this.localPathForReadingWritingData);
            if (!string.IsNullOrEmpty(dataJsonString))
            {
                try
                {
                    arrayOfScores = JsonHelper.FromJson<Score>(dataJsonString);
                    listOfScores = arrayOfScores.ToList();
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.Message);
                }
            }
        }


        return listOfScores;
    }

    /// <summary>
    /// Pisanje u lokalnu datoteku
    /// </summary>
    /// <param name="listOfScores"></param>
    /// <param name="runTime"></param>
    /// <param name="characterName"></param>
    void WriteResultToLocalFile(List<Score> listOfScores, float runTime, string characterName)
    {
        try
        {
            Map currentMap = new Map()
            {
                Name = this.CurrentMap
            };

            PlayerSettings settings = new PlayerSettings()
            {

                ///Hardkodirano
                Controls = ControlsEnum.Button,
                Character = characterName,
                ///Procitaj iz novog fajla volume jer je volume object u sceni koja se destroy-a
                Volume = 0
            };
            Score score = new Score()
            {
                RaceTime = runTime,
                RunDate = this.RunDate,
                Map = currentMap,
                PlayerSettings = settings
            };

            listOfScores.Add(score);
            listOfScores = listOfScores.OrderBy(t => t.RaceTime).ToList();
            int counter = 1;
            Leaderboard.GetComponent<Text>().text = "Leaderboard\n\n";
            foreach (var race in listOfScores)
            {
                Leaderboard.GetComponent<Text>().text = Leaderboard.GetComponent<Text>().text + counter + ". " + race.PlayerSettings.User + ": " + race.RaceTime.ToString("0.00") + "\n";
                counter++;
                if (counter == 6)
                {
                    break;
                }
            }

            Score[] arrayOfScores = listOfScores.ToArray();


            ///Serializacija rezultata    
            string mapJson = JsonHelper.ToJson(arrayOfScores, true);


            //mapJson = JsonUtility.ToJson(maps);




            //Zapis u file
            if (!File.Exists(this.localPathForReadingWritingData))
            {
                File.Create(this.localPathForReadingWritingData).Close();
                File.WriteAllText(this.localPathForReadingWritingData, mapJson);
            }
            else
            {
                File.WriteAllText(this.localPathForReadingWritingData, mapJson);
            }
        }
        catch (Exception ex)
        {
            Leaderboard.GetComponent<Text>().text = "GRESKA." + ex.Message;
        }



    }
    IEnumerator Countdown()
    {
        CountdownText.GetComponent<Text>().text = "";
        yield return new WaitForSeconds(1);
        CountdownText.GetComponent<Text>().text = "3";
        yield return new WaitForSeconds(1);
        CountdownText.GetComponent<Text>().text = "2";
        yield return new WaitForSeconds(1);
        CountdownText.GetComponent<Text>().text = "1";
        yield return new WaitForSeconds(1);
        StartGamePanel.SetActive(false);
        runTime.Start();
    }
}
