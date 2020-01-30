﻿
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

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
    GameObject runMonitor;
    GameObject progressBar;

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
            raceEnded = true;
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

}
