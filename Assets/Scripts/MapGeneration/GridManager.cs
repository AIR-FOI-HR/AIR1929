using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int rows = 20;
    public int cols = 100;
    public float tileSize = (float)1.28;
    public int numberOfRooms = 10;
    public int floorSize = 4;
    public int chaos = 3;
    public GameObject[,] grid = null;

    private int floorA, floorB, floorC, floorD;
    System.Random rng = new System.Random();

    void Start()
    {
        Debug.Log("Start: " + System.DateTime.Now);
        grid = new GameObject[cols * numberOfRooms, rows];
        GenerateMap(numberOfRooms);



        //GenerateObstacles();
        CheckForTop(); // vrh pretvaramo u snijeg
        Debug.Log("End: " + System.DateTime.Now);
    }
    
    /// <summary>
    /// Generira se cijela mapa za igranje sa tim brojem soba.
    /// </summary>
    /// <param name="rooms"></param>
    private void GenerateMap(int rooms)
    {
        for (int i = 0; i < rooms; i++)
        {
            GenerateFloorDimensions();
            GenerateRoom(i); 
            GenerateHoles(floorA, i);
            GenerateHoles(floorB, i);
            GenerateHoles(floorC, i);
            GenerateObstacles(floorA, i);
            GenerateObstacles(floorB, i);
            GenerateObstacles(floorC, i);
            GenerateObstacles(floorD, i);
        }
    }

    private void GenerateObstacles(int floor, int room)
    {
        int currentCol, currentRow;
        for (int col = cols * room; col < (cols + cols * room); col++)
        {
            GameObject go = grid[col, floor];
            if (go.GetComponent<Node>().tileType == Node.TileType.dirt)
            {
                if (RandomNumber(1)==1)
                {
                    if (col > 0)
                    {
                        if (grid[col - 1, floor].GetComponent<Node>().tileType == Node.TileType.dirt)
                        {
                            if (RandomNumber(100) < chaos)
                            {
                                if (floor > 0)
                                {
                                    currentCol = col;
                                    currentRow = floor - 1;

                                    CreateTile(currentCol, currentRow, Node.TileType.dirt); //gore

                                    for (int i = 0; i < RandomNumber(2, 4); i++)
                                    {
                                        if (RandomNumber(100) < 50)
                                        {
                                            if (currentRow > 2)
                                            {
                                                if (!CheckForTile(currentCol, currentRow - 3) &&
                                                    !CheckForTile(currentCol - 1, currentRow - 3))
                                                {
                                                    currentRow -= 1;
                                                    CreateTile(currentCol, currentRow, Node.TileType.dirt);
                                                }
                                                else
                                                {
                                                    DebugLogTile(currentCol, currentRow, "stajem");
                                                }
                                            }

                                        }
                                        else //ide desno
                                        {
                                            if (currentCol < cols * numberOfRooms)
                                            {
                                                currentCol += 1;
                                                CreateTile(currentCol, currentRow, Node.TileType.dirt);
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    } 
                } else
                {
                    //tu ide prema dole
                }
            }
        }
    }

    /// <summary>
    /// Provjerava imali na tim kordinatama tile i vraća true ako ima.
    /// </summary>
    /// <param name="col"></param>
    /// <param name="row"></param>
    /// <returns></returns>
    private bool CheckForTile(int col, int row)
    {
        GameObject go = GetNode(col, row);
        bool retVal = false;
        if(go.GetComponent<Node>().tileType == Node.TileType.dirt ||
            go.GetComponent<Node>().tileType == Node.TileType.snow)
        {
           // DebugLogTile(col, row, "true");
            retVal = true;
        }
        return retVal;
    }
    
    /// <summary>
    /// Ispisuje u debug logu taj tile.
    /// </summary>
    /// <param name="col"></param>
    /// <param name="row"></param>
    private void DebugLogTile(int col, int row)
    {
        Debug.Log("(" + col + ", " + row + ")");
    }
    private void DebugLogTile(int col, int row, string message)
    {
        Debug.Log("(" + col + ", " + row + ") -> " + message);
    }

    /// <summary>
    /// Generira se soba sa tim rednim brojem.
    /// </summary>
    /// <param name="room"></param>
    private void GenerateRoom(int room)
    {     
        for (int col = cols * room; col < (cols + cols*room); col++)
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


    /// <summary>
    /// Generiraju se nasumično rupe u katu.
    /// </summary>
    /// <param name="floor"></param>
    private void GenerateHoles(int floor, int room)
    {
        for (int row = 0; row < rows; row++)
        {
            if (row == floor)
            {
                for (int col = cols * room; col < (cols + cols * room); col++)
                {
                    if (RandomNumber(100) < chaos)
                    {
                        GameObject go = grid[col, row];
                        for (int i = RandomNumber(0, 2); i < 5; i++)
                        {
                            if(col == cols + cols * room)
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
        for (int col = 0; col < cols * numberOfRooms; col++)
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
        floorD = rows - 1;
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

