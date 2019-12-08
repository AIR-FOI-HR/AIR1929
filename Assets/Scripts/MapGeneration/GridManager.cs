using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int rows = 20;
    public int cols = 100;
    public float tileSize = (float)1.28;  
    public GameObject[,] grid;

    private int floorA, floorB, floorC, floorD;

    void Start()
    {
        grid = new GameObject[cols, rows];
        GenerateFloorDimensions();
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        GameObject referenceTile = (GameObject)Resources.Load("TileDirt");
        Node node = referenceTile.GetComponent<Node>();
        

        for (int col = 0; col < cols; col++)
        {
            for (int row = 0; row < rows; row++)
            {
                //tile koji zelimo koristiti
                referenceTile = node.CreateNode(Node.TileType.dirt);

                if(row >= 0 && row < floorA)
                {
                    Destroy(referenceTile);
                    referenceTile = node.CreateNode(Node.TileType.empty);
                } else if (row > floorA && row < floorB)
                {
                    Destroy(referenceTile);
                    referenceTile = node.CreateNode(Node.TileType.empty);
                }
                else if (row > floorB && row < floorC)
                {
                    Destroy(referenceTile);
                    referenceTile = node.CreateNode(Node.TileType.empty);
                }
                else if (row > floorC && row < floorD)
                {
                    Destroy(referenceTile);
                    referenceTile = node.CreateNode(Node.TileType.empty);
                }

                GameObject tile = (GameObject)Instantiate(referenceTile, transform);
                tile.name ="("+ col + ", " + row + ")";
                float posX = col * tileSize;
                float posY = row * -tileSize;
                tile.transform.position = new Vector2(posX, posY);
                grid[col, row] = tile;
                Destroy(referenceTile);
            }
        }




    }
    /// <summary>
    /// Vraća željeni tile sa tim kordinatama.
    /// </summary>
    /// <param name="posX"></param>
    /// <param name="posY"></param>
    /// <returns>GameObject</returns>
    public GameObject GetNode(int posX, int posY)
    {
        GameObject retVal = null;
        retVal = grid[posX, posY];
        return retVal;
    }

    private void GenerateFloorDimensions()
    {
        floorA = GenerateNumber(0, 4);       
        floorB = GenerateNumber(floorA + 3, 10);
        floorC = GenerateNumber(floorB + 3, 15);
        floorD = GenerateNumber(floorC + 3, rows-1);
    }
    /// <summary>
    /// Generira nasumičan broj.
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns>int</returns>
    public int GenerateNumber(int min, int max)
    {
        System.Random rng = new System.Random();
        int number = rng.Next(min, max);
        return number;       
    }
    
}

