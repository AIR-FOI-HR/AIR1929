
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PlayerControls : MonoBehaviour
{
    public CharacterController2D characterController;
    Rigidbody2D rigidbody2d;
    float currentSpeed;
    public float maxSpeed;
    public float acceleration;
    float currentAcceleration;
    bool jump = false;
    bool crouch = false;
    public Animator animator;
    bool raceEnded = false;
    private float jumpForce = 1800f;

   

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = 0;
        currentAcceleration = 0;
        rigidbody2d = GetComponent<Rigidbody2D>();
        StartCoroutine(Countdown());
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

            SwipeCheck();

            if (Input.GetButtonDown("Jump") == true)
            {
                jump = true;
                animator.SetBool("Jump", true);
            }

            if (Input.GetButtonDown("Crouch") == true)
            {
                crouch = true;
                //animator.setbool("crouch", true);
            }
            else if (Input.GetButtonUp("Crouch") == true)
            {
                crouch = false;
                //animator.SetBool("Crouch", false);
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
        JumpIfAllowed();
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
    private Vector2 startTouchPosition, endTouchPosition;
    private bool jumpAllowed = false;
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
                jumpAllowed = true;
            }
        }

    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("3");
        yield return new WaitForSeconds(1);
        Debug.Log("2");
        yield return new WaitForSeconds(1);
        Debug.Log("1");
        yield return new WaitForSeconds(1);
        Debug.Log("Go!");


        currentAcceleration = acceleration;
    }
    private void JumpIfAllowed()
    {
        if (jumpAllowed)
        {
            rigidbody2d.AddForce(Vector2.up * jumpForce);
            jumpAllowed = false;
        }
    }
}
