using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int rows = 20;
    public int cols = 100;
    public float tileSize = (float)1.28;
    public int floorSize = 4;
    public int chaos = 3;
    public GameObject[,] grid = null;

    private int floorA, floorB, floorC, floorD;
    System.Random rng = new System.Random();

    void Start()
    {
        grid = new GameObject[cols, rows];
        GenerateFloorDimensions(); //generiramo katove na nasumičnim mjestima
        GenerateGrid(); // stvaramo grid
        GenerateHoles(floorA);
        GenerateHoles(floorB);
        GenerateHoles(floorC);
        //GenerateObstacles();
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

    

    private void GenerateObstacles()
    {
        for (int col = 0; col < cols; col++)
        {
            for (int row = 0; row < rows; row++)
            {
                if(RandomNumber(100) < chaos)
                {
                    Debug.Log("col: "+col+" row: "+row);
                    CreateObstacle(col, row);
                }
            }
        }       
    }
    private void CreateObstacle(int col, int row)
    {
        GameObject go = grid[col, row];
        if (go.GetComponent<Node>().tileType == Node.TileType.dirt ||
            go.GetComponent<Node>().tileType == Node.TileType.snow)
        {
            RemoveTile(col,row);
            CreateTile(col, row, Node.TileType.empty);
        }
        else if (go.GetComponent<Node>().tileType == Node.TileType.empty)
        {
          //  CreateTile(col, row, Node.TileType.dirt);
            
        }
    }




    /// <summary>
    /// Generiraju se nasumično rupe u katu.
    /// </summary>
    /// <param name="floor"></param>
    private void GenerateHoles(int floor)
    {
        for (int row = 0; row < rows; row++)
        {
            if (row == floor)
            {
                for (int col = 0; col < cols; col++)
                {
                    if (RandomNumber(100) < chaos)
                    {
                        GameObject go = grid[col, row];
                        for (int i = RandomNumber(0, 2); i < 5; i++)
                        {
                            if(col == cols)
                            {
                                break;
                            }
                            RemoveTile(col, row);
                            col++;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Funkcija gleda gdje je najgornji tile te njega pretvara u snježni.
    /// </summary>
    private void CheckForTop()
    {
        for (int col = 0; col < cols; col++)
        {
            for (int row = 0; row < rows; row++)
            {
                GameObject go = grid[col, row];
                if(go.GetComponent<Node>().tileType == Node.TileType.dirt)
                {
                    RemoveTile(col, row);
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
    private void RemoveTile(int col, int row)
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
        floorA = RandomNumber(0, 4);       
        floorB = RandomNumber(floorA + floorSize, 10);
        floorC = RandomNumber(floorB + floorSize, 15);
        floorD = RandomNumber(floorC + floorSize, rows-1);
    }

    /// <summary>
    /// Generira nasumičan broj od min do max.
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns>int</returns>
    public int RandomNumber(int min, int max)
    {
        int number = rng.Next(min, max);
        return number;       
    }

    /// <summary>
    /// Generira nasumičan broj od 0 do max.
    /// </summary>
    /// <param name="max"></param>
    /// <returns></returns>
    public int RandomNumber(int max)
    {
        int number = rng.Next(0, max);
        return number;
    }
    
}

