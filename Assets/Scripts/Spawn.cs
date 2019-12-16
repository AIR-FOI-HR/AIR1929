using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject item;
    private Transform player;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void SpawnItem()
    {
        Vector3 vektor = new Vector3(1, 1, 1);
        Vector2 playerPos = new Vector2(player.position.x-3, player.position.y);
        item.transform.localScale = vektor;
        Instantiate(item,playerPos,Quaternion.identity);
        

    }
}
