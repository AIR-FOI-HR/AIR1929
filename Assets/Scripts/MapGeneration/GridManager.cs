using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int rows = 20;
    public int cols = 100;
    public float tileSize = 1;

    void Start()
    {        
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        GameObject referenceTile = (GameObject)Resources.Load("TileDirt");
        Node node = referenceTile.AddComponent<Node>();
        for (int col = 0; col < cols; col++)
        {            
            for (int row = 0; row < rows; row++)
            {
               
                referenceTile = node.CreateNode(Node.TileType.snow);
                GameObject tile = (GameObject)Instantiate(referenceTile, transform);
                tile.name ="("+ col + ", " + row + ")";

                float posX = col * tileSize;
                float posY = row * -tileSize;

                tile.transform.position = new Vector2(posX, posY);
            }
        }
    }

    
    void Update()
    {
        
    }
}

