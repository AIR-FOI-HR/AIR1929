using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject item;
    private Transform player;
    float brzina = 50.0f;
    public GameObject metak;
    public bool fired=false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (fired)
        {
            Vector2 position = metak.transform.position;
            position = new Vector2(position.x + brzina * Time.deltaTime, position.y);
            metak.transform.position = position;
        }
    }

    public void SpawnItem()
    {
        if (item.CompareTag("Projectile"))
        {
            FireRocket();
        }
        else
        {
            Vector3 vektor = new Vector3(1, 1, 1);
            Vector2 playerPos = new Vector2(player.position.x - 3, player.position.y);
            item.transform.localScale = vektor;
            Instantiate(item, playerPos, Quaternion.identity);
        }


    }

    public void FireRocket()
    {
        Vector3 vektor = new Vector3(1, 1, 1);
        item.transform.localScale = vektor;
        Vector2 playerPos = new Vector2(player.position.x + 5, player.position.y);
        GameObject noviMetak = Instantiate(item, playerPos, Quaternion.identity);
        noviMetak.GetComponent<Spawn>().metak=noviMetak;
        noviMetak.GetComponent<Spawn>().fired = true;
    }


}
