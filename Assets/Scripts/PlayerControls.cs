using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public CharacterController2D characterController;
    Rigidbody2D rigidbody2d;
    float currentSpeed;
    public float maxSpeed;
    public float acceleration;
    bool jump = false;
    bool crouch = false;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = 0;
        acceleration = 0;
        rigidbody2d = GetComponent<Rigidbody2D>();
        StartCoroutine(Countdown());
    }

    // Update is called once per frame
    void Update()
    {
        currentSpeed += acceleration;
        if (currentSpeed >= maxSpeed)
        {
            currentSpeed = maxSpeed;
        }
        animator.SetFloat("Speed", currentSpeed);

        if (Input.GetButtonDown("Jump") == true)
        {
            jump = true;
            animator.SetBool("Jump", true);
        }

        if (Input.GetButtonDown("Crouch") == true)
        {
            crouch = true;
            animator.SetBool("Crouch", true);
        }
        else if (Input.GetButtonUp("Crouch") == true)
        {
            crouch = false;
            animator.SetBool("Crouch", false);
        }

        rigidbody2d.velocity = new Vector2(currentSpeed, rigidbody2d.velocity.y);
    }

    public void OnLanding()
    {
        animator.SetBool("Jump", false);
    }

    private void FixedUpdate()
    {
        characterController.Move(rigidbody2d.velocity.x * Time.fixedDeltaTime, crouch, jump);
        //After a successful jump we need to set jump attribute to the default value = false;
        jump = false;
        crouch = false;
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
        acceleration = 0.1f;
    }
}
