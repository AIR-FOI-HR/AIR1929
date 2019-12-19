
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
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






    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        currentSpeed = 0;
        currentAcceleration = 0;
        rigidbody2d = GetComponent<Rigidbody2D>();
        StartCoroutine(Countdown());
    }

    public void onJumpClick()
    {
        animator.SetBool("Jump", true);
        characterController.Move(rigidbody2d.velocity.x * Time.fixedDeltaTime, false, true);
    }
    public void onSlideClick()
    {
        StartCoroutine(SlideAnimation());
    }

    // Update is called once per frame
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

    public void OnLanding()
    {
        animator.SetBool("Jump", false);
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("Crouch", isCrouching);
    }

    private void FixedUpdate()
    {
        characterController.Move(rigidbody2d.velocity.x * Time.fixedDeltaTime, crouch, jump);
        //After a successful jump we need to set jump attribute to the default value = false;
        jump = false;

    }

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



    void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.gameObject.tag == "FlagController")
        {
            raceEnded = true;
        }
    }

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

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(1);
        yield return new WaitForSeconds(1);
        yield return new WaitForSeconds(1);
        yield return new WaitForSeconds(1);
        currentAcceleration = acceleration;
    }
    IEnumerator SlideAnimation()
    {
        crouch = true;
        yield return new WaitForSeconds(0.500f);
        crouch = false;
    }

}
