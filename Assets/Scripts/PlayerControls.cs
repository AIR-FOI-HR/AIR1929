using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Scripts.ERA;
using System.Linq;
using Debug = UnityEngine.Debug;
using System;
using System.IO;
using Assets.Scripts;

public class PlayerControls : MonoBehaviour
{
    public CharacterController2D characterController;
    Rigidbody2D rigidbody2d;
    public Animator animator;
    private Vector2 startTouchPosition, endTouchPosition;

    public float maxSpeed, acceleration, currentAcceleration, currentSpeed;

    bool jump = false;
    bool crouch = false;
    bool raceEnded = false;
    bool raceStarted = false;

    private Stopwatch runTime = new Stopwatch();
    public DateTime RunDate;
    public string CurrentMap;
    private string localPathForReadingWritingData;
    bool FirstCollision = false;

    GameObject runMonitor;
    GameObject progressBar;
    GameObject runInformationText;
    GameObject leaderboard;
    GameObject mainMenuButton;

    /// <summary>
    /// Prva funkcija koja se pokrece
    /// </summary>
    void Start()
    {
        currentSpeed = 0;
        currentAcceleration = 0;
        rigidbody2d = GetComponent<Rigidbody2D>();
        runMonitor = GameObject.Find("RunMonitor");
        progressBar = GameObject.Find("ProgressBar");
        runInformationText = GameObject.Find("RunInformationText");
        runInformationText.SetActive(false);
        leaderboard = GameObject.Find("LeaderboardText");
        leaderboard.SetActive(false);
        mainMenuButton = GameObject.Find("MainMenuButton");
        mainMenuButton.SetActive(false);

        /////Postavljanje datuma i vremena utrke
        RunDate = DateTime.Now;
        /////Dohvaćanje mape trke
        CurrentMap = SceneManager.GetActiveScene().name;
        ////Postavljanje rute za spremanje lokalnih podataka
        localPathForReadingWritingData = Application.persistentDataPath + "/score.txt";
    }

    /// <summary>
    /// Funkcija koja se pokreće na android tipku
    /// </summary>
    public void onJumpClick()
    {
        if (PlayerPrefs.HasKey("Controls") && PlayerPrefs.GetFloat("Controls") == 0.0f)
        {
            jump = true;
            animator.SetBool("Jump", true);
        }
    }

    /// <summary>
    /// Funkcija koja se pokreće na android tipku
    /// </summary>
    public void onSlideClick()
    {
        if (PlayerPrefs.HasKey("Controls") && PlayerPrefs.GetFloat("Controls") == 0.0f)
        {
            StartCoroutine(SlideAnimation());
        }
    }

    /// <summary>
    /// Funkcija koja se poziva svaki frame
    /// </summary>
    void Update()
    {
        if (runMonitor.GetComponent<RunMonitor>().raceStarted)
        {
            currentAcceleration = acceleration;
            if (!raceStarted)
            {
                raceStarted = true;
                runTime.Start();
            }
        }

        if (raceEnded)
        {
            SlowDown();
            rigidbody2d.velocity = new Vector2(currentSpeed, rigidbody2d.velocity.y);
        }
        else
        {
            currentSpeed += currentAcceleration;
            if (currentSpeed >= maxSpeed)
            {
                currentSpeed = maxSpeed;
            }
            animator.SetFloat("Speed", currentSpeed);

            //For Android
            if (PlayerPrefs.HasKey("Controls") && PlayerPrefs.GetFloat("Controls") == 1.0f)
            {
                SwipeCheck();
            }

            //For Computer
            if (Input.GetButtonDown("Jump") == true)
            {
                jump = true;
                animator.SetBool("Jump", true);
            }

            if (Input.GetButtonDown("Crouch") == true)
            {
                StartCoroutine(SlideAnimation());
            }

            rigidbody2d.velocity = new Vector2(currentSpeed, rigidbody2d.velocity.y);

            if (progressBar.activeSelf && progressBar.GetComponent<Slider>().value <= 1)
                progressBar.GetComponent<Slider>().value = transform.position.x / GameObject.FindGameObjectWithTag("FlagController").transform.position.x;
        }
    }

    /// <summary>
    /// Funkcija koja se poziva prilikom spuštanja na zemlju
    /// </summary>
    public void OnLanding()
    {
        animator.SetBool("Jump", false);
    }

    /// <summary>
    /// Funkcija koja se poziva pritiskom tipke na tipkovnici (slide-anje)
    /// </summary>
    /// <param name="isCrouching"></param>
    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("Crouch", isCrouching);
    }

    /// <summary>
    /// Funkcija koja se pokrece cesce od Update frame-a
    /// </summary>
    private void FixedUpdate()
    {
        characterController.Move(rigidbody2d.velocity.x * Time.fixedDeltaTime, crouch, jump);
        //After a successful jump we need to set jump attribute to the default value = false;
        jump = false;

    }

    /// <summary>
    /// Usporavanje nakon zavrsetka utrke
    /// </summary>
    void SlowDown()
    {
        currentAcceleration = -0.1f;
        currentSpeed += currentAcceleration;
        if (currentSpeed <= 0)
        {
            currentSpeed = 0;
            animator.SetFloat("Speed", currentSpeed);
        }
    }


    /// <summary>
    /// Sudar sa zastavicom
    /// </summary>
    /// <param name="collider"></param>
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(collider, GetComponent<Collider2D>());
        }
        if (collider.gameObject.tag == "FlagController")
        {
            if (FirstCollision == false)
            {
                string characterName = gameObject.name;

                float runTimeFloat = (float)runTime.Elapsed.TotalMilliseconds / 1000;
                string runTimeString = runTimeFloat.ToString("0.00");
                Debug.Log(runTimeString);

                //Zapisi podatke u Log
                List<Score> listOfScores = ReadLocalData();
                WriteResultToLocalFile(listOfScores, runTimeFloat, characterName);

                //leaderboard.GetComponent<Text>().text = Application.persistentDataPath;
                runInformationText.GetComponent<Text>().text = "Run time: " + runTimeString + "";

                FirstCollision = true;
                raceEnded = true;
                EndRace();
            }
        }
    }

    /// <summary>
    /// Funckija koja prati swipe-anje po touch screen-u
    /// </summary>
    private void SwipeCheck()
    {

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPosition = Input.GetTouch(0).position;
            if (endTouchPosition.y > startTouchPosition.y && rigidbody2d.velocity.y == 0)
            {
                jump = true;

            }
            else if (endTouchPosition.y < startTouchPosition.y && rigidbody2d.velocity.y == 0)
            {
                StartCoroutine(SlideAnimation());
            }
        }

    }

    /// <summary>
    /// Odredivanje trajanja slide-a
    /// </summary>
    /// <returns></returns>
    IEnumerator SlideAnimation()
    {
        crouch = true;
        yield return new WaitForSeconds(0.500f);
        crouch = false;
    }

    void EndRace()
    {
        runInformationText.SetActive(true);
        leaderboard.SetActive(true);
        mainMenuButton.SetActive(true);
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("Menu");
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
            leaderboard.GetComponent<Text>().text = "Leaderboard\n\n";
            foreach (var race in listOfScores)
            {
                leaderboard.GetComponent<Text>().text = leaderboard.GetComponent<Text>().text + counter + ". " + race.PlayerSettings.User + ": " + race.RaceTime.ToString("0.00") + "\n";
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
            leaderboard.GetComponent<Text>().text = "GRESKA." + ex.Message;
        }
    }
}
