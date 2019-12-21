using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    private Inventory inventory;
    public GameObject itemButton;
    public GameObject shield;
    private Transform player;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Inventory>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
        
        if (gameObject.name== "powerup_collectable" || gameObject.name == "powerup_collectable_rocket")
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.isFull[i] == false)
                {
                    inventory.isFull[i] = true;
                    Debug.Log("Dodano u inventory");
                    Instantiate(itemButton, inventory.slots[i].transform,false);
                    Destroy(gameObject);
                    break;
                }
            }
        }
        if (gameObject.name == "powerup")
        {
            Debug.Log("Pokupljeno");
            SpawnShield();
            Destroy(gameObject);
        }
        
    }

    private void SpawnShield()
    {
        GameObject newShield = Instantiate(shield, player.position, Quaternion.identity);
        newShield.transform.SetParent(player);
        Destroy(newShield,5);
    }
}
