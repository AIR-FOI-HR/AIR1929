﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    float currentSpeed;
    public float maxSpeed;
    public float acceleration;

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
        rigidbody2d.velocity = new Vector2(currentSpeed, rigidbody2d.velocity.y);
    }
}
