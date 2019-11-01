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
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = 0;
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
      {
        currentSpeed += acceleration;
        if (currentSpeed >= maxSpeed)
        {
            currentSpeed = maxSpeed;
        }

        if (Input.GetButtonDown("Jump") == true)
        {
            jump = true;
            animator.SetBool("Jump", true);
        }
        rigidbody2d.velocity = new Vector2(currentSpeed, rigidbody2d.velocity.y);
    }

    private void FixedUpdate()
      {
        characterController.Move(rigidbody2d.velocity.x * Time.fixedDeltaTime, false, jump);
        //After a successful jump we need to set jump attribute to the default value = false;
        jump = false;
    }
}
