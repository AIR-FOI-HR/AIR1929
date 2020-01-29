
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

using Photon.Realtime;
using Photon.Pun;
using Assets.Scripts.ERA;

public class PlayerControls : MonoBehaviourPunCallbacks
{
    public CharacterController2D characterController;
    Rigidbody2D rigidbody2d;
    public Animator animator;
    private Vector2 startTouchPosition, endTouchPosition;

    public float maxSpeed, acceleration, currentAcceleration, currentSpeed;

    bool jump = false;
    bool crouch = false;
    bool raceEnded = false;
    bool firstColision = false;

    public GameObject startRaceButton;
    public GameObject endRacePanel;
    public GameObject startPanel;
    public GameObject countDownText;




    /// <summary>
    /// Prva funkcija koja se pokrece
    /// </summary>
    void Start()
    {
        startRaceButton.SetActive(false);
        DontDestroyOnLoad(gameObject);
        currentSpeed = 0;
        currentAcceleration = 0;
        rigidbody2d = GetComponent<Rigidbody2D>();

        ///Ukoliko je photon network povezan i ukoliko je master klijent spojen. (Master klijent je prvi klijent koji je usao u sobu).
        if (PhotonNetwork.IsConnected)
        {
            ///Multiplayer
            if (PhotonNetwork.IsMasterClient)
            {
                startRaceButton.SetActive(true);
            }
        }
        else
        {
            ///Singleplayer
            StartGame();
        }
    }
    public void StartGame()
    {
        startRaceButton.SetActive(false);
        StartCoroutine(Countdown());
    }

    /// <summary>
    /// Funkcija koja se pokreće na android tipku
    /// </summary>
    public void onJumpClick()
    {
        jump = true;
        animator.SetBool("Jump", true);
    }
    /// <summary>
    /// Funkcija koja se pokreće na android tipku
    /// </summary>
    public void onSlideClick()
    {
        StartCoroutine(SlideAnimation());
    }

    /// <summary>
    /// Funkcija koja se poziva svaki frame
    /// </summary>
    void Update()
    {
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
            //SwipeCheck();

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

        if (collider.gameObject.tag == "FlagController")
        {
            if (firstColision == false)
            {

                string characterName = collider.name;

                //1. Open panel with run information
                //2. IDLE Player motivation (IDLE)
                Debug.Log("Sudar objekta sa zastavom.");


                //float runTimeFloat = (float)runTime.Elapsed.TotalMilliseconds / 1000;
                //string runTimeString = runTimeFloat.ToString("0.00");
                //Debug.Log(runTimeString);

                ////Zapisi podatke u Log
                //List<Score> listOfScores = ReadLocalData();
                //WriteResultToLocalFile(listOfScores, runTimeFloat, characterName);

                ////Leaderboard.text = Application.persistentDataPath;
                //RunTimeInformationText.GetComponent<Text>().text = "Run time: " + runTimeString + "";

                endRacePanel.SetActive(true);


                firstColision = true;
                raceEnded = true;
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
    /// Odbrojavanje prilikom pocetka utrke
    /// </summary>
    /// <returns></returns>
    IEnumerator Countdown()
    {
        countDownText.GetComponent<Text>().text = "";
        yield return new WaitForSeconds(1);
        countDownText.GetComponent<Text>().text = "3";
        yield return new WaitForSeconds(1);
        countDownText.GetComponent<Text>().text = "2";
        yield return new WaitForSeconds(1);
        countDownText.GetComponent<Text>().text = "1";
        yield return new WaitForSeconds(1);
        startPanel.SetActive(false);
        //runTime.Start();
        currentAcceleration = acceleration;
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

}
