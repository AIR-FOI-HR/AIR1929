﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    private Inventory inventory;
    public GameObject itemButton;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Inventory>();
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
                    Instantiate(itemButton, inventory.slots[i].transform,false);
                    Destroy(gameObject);
                }
            }
        }
        if (gameObject.name == "powerup")
        {
            Debug.Log("Pokupljeno");
            Destroy(gameObject);
        }
        
    }
}
