using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PickUp();
        }

    }

    private void PickUp()
    {
        Debug.Log("Pokupljeno");
        Destroy(gameObject);
    }
}
