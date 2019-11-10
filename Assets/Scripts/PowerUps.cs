using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    private Inventory inventory;


    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PickUp();
        }

    }

    private void PickUp()
    {
        
        if (gameObject.name== "powerup_collectable")
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.puno[i] == false)
                {
                    inventory.puno[i] = true;
                    Debug.Log("Dodano u inventory");
                }
            }
        }
        Debug.Log("Pokupljeno");
        Destroy(gameObject);
    }
}
