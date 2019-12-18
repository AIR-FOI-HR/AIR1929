using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapsCollision : MonoBehaviour
{
    public GameObject trigger;
    void OnTriggerEnter2D(Collider2D other)
    {

       if (other.CompareTag("Player"))
        {
            TriggerAction(other);
        }

    }

    private void TriggerAction(Collider2D other)
    {
        Transform[] children = GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<Transform>();
        bool flag = true;
        foreach (var item in children)
        {
            if (item.CompareTag("Shield"))
            {
                Destroy(item.gameObject);
                flag = false;
                break;
            }
        }
        if (flag)
        {
            other.gameObject.GetComponent<PlayerControls>().currentSpeed = 0;
        }
        Destroy(trigger);
    }
}
