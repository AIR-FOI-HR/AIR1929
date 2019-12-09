using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int rows = 20;
    public int cols = 100;
    public float tileSize = (float)1.28;
    public GameObject[,] grid = null;

    private int floorA, floorB, floorC, floorD;

    void Start()
    {
        grid = new GameObject[cols, rows];
        GenerateFloorDimensions(); //generiramo katove na nasumičnim mjestima
        GenerateGrid(); // stvaramo grid
        CheckForTop(); // vrh pretvaramo u snijeg
    }

    private void GenerateGrid()
    {   
        for (int col = 0; col < cols; col++)
        {
            for (int row = 0; row < rows; row++)
            {
                if(row >= 0 && row < floorA)
                {
                    CreateTile(col, row, Node.TileType.empty);
                } else if (row > floorA && row < floorB)
                {
                    CreateTile(col, row, Node.TileType.empty);
                }
                else if (row > floorB && row < floorC)
                {
                    CreateTile(col, row, Node.TileType.empty);
                }
                else if (row > floorC && row < floorD)
                {
                    CreateTile(col, row, Node.TileType.empty);
                } else
                {
                    CreateTile(col, row, Node.TileType.dirt);
                }
            }
        }
    }
    private void CheckForTop()
    {
        for (int col = 0; col < cols; col++)
        {
            for (int row = 0; row < rows; row++)
            {
                GameObject go = grid[col, row];
                if(go.GetComponent<Node>().tileType == Node.TileType.dirt)
                {
                    DestroyTile(col, row);
                    CreateTile(col, row, Node.TileType.snow);
                    break;
                    
                } 
            }
        }
    }
    /// <summary>
    /// Stvara tile određenog tipa na tim kordinatama.
    /// </summary>
    /// <param name="col"></param>
    /// <param name="row"></param>
    /// <param name="type"></param>
    /// <returns>GameObject</returns>
    private GameObject CreateTile(int col, int row, Node.TileType type)
    {
        GameObject referenceTile = (GameObject)Resources.Load("TileDirt");
        Node node = referenceTile.GetComponent<Node>();

        referenceTile = node.CreateNode(type);
        referenceTile.GetComponent<Node>().tileType = type;

        GameObject tile = (GameObject)Instantiate(referenceTile, transform);
        tile.name = "(" + col + ", " + row + ")";
        float posX = col * tileSize;
        float posY = row * -tileSize;
        tile.transform.position = new Vector2(posX, posY);
        grid[col, row] = tile;
        Destroy(referenceTile);        

        return tile;
    }
    /// <summary>
    /// Uništava tile na tim kordinatama.
    /// </summary>
    /// <param name="col"></param>
    /// <param name="row"></param>
    private void DestroyTile(int col, int row)
    {       
        Destroy(grid[col, row]);
        CreateTile(col, row, Node.TileType.empty);
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
    /// <summary>
    /// Generira na kojim će se mjestima nalaziti katovi.
    /// </summary>
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

