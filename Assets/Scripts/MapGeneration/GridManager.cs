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
            Debug.Log(i);

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
        for (int col = cols * room; col < (cols + cols * room); col++)
        {
            GameObject go = grid[col, floor];
            if (go.GetComponent<Node>().tileType == Node.TileType.dirt)
            {
                if (floor != rows - 1)
                {
                    if (RandomNumber(100) < 50) 
                    {                        
                        CreateObstacles(col, floor, Direction.up);
                    }
                    else
                    {
                        CreateObstacles(col, floor, Direction.down);
                    }
                }
                else //zadnji kat
                {
                    if (RandomNumber(100) < 50)
                    {                          
                        CreateObstacles(col, floor, Direction.up);                            
                    }
                }
            }
        }
    }

    /// <summary>
    /// Generiraju se prepreke na tom katu. True znači da će se generirati
    /// prepreke gornje strane kata, a false da će se generirati sa donje.
    /// </summary>
    /// <param name="col"></param>
    /// <param name="floor"></param>
    /// <param name="up"></param>
    private void CreateObstacles(int col, int floor, Direction direction)
    {
        DebugLogTile(col, floor);
        int currentCol, currentRow;
        if (col > 0)
        {
            if (direction == Direction.up)
            {
                if (RandomNumber(100) <= chaos)
                {
                    currentCol = col;
                    currentRow = floor;
                    if (!CheckForObstacles(currentCol, currentRow, Direction.up))
                    {
                        
                        currentRow -= 1;
                        CreateTile(currentCol, currentRow, Node.TileType.dirt);
                        for (int i = 0; i < RandomNumber(2, 4); i++)
                        {
                            if (RandomNumber(100) < 50) //gore
                            {
                                if (!CheckForObstacles(currentCol, currentRow, Direction.up))
                                {
                                    currentRow -= 1;
                                    CreateTile(currentCol, currentRow, Node.TileType.dirt);
                                }
                            }
                            else // desno
                            {
                                if (!CheckForObstacles(currentCol, currentRow, Direction.right))
                                {
                                    currentCol += 1;
                                    CreateTile(currentCol, currentRow, Node.TileType.dirt);
                                    if (currentRow != floor)
                                    {
                                        if (RandomNumber(100) < 50)
                                        {
                                            currentRow += 1;
                                            CreateTile(currentCol, currentRow, Node.TileType.dirt);
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
            }
            else if (direction == Direction.down) // dole
            {

            } 
        }
    }

    /// <summary>
    /// Vraća true ako postoje prepreke za postavljanje novog tilea.
    /// </summary>
    /// <param name="col"></param>
    /// <param name="row"></param>
    /// <param name="up"></param>
    /// <returns></returns>
    private bool CheckForObstacles(int col, int row, Direction direction)
    {
        int currentCol = col;
        int currentRow = row;
        bool retVal = true;
        if (direction == Direction.up)
        {
            if (!CheckForTile(currentCol - 1, currentRow - 1) &&
                !CheckForTile(currentCol + 0, currentRow - 1) &&
                !CheckForTile(currentCol + 1, currentRow - 1) &&

                !CheckForTile(currentCol - 1, currentRow - 2) &&
                !CheckForTile(currentCol + 0, currentRow - 2) &&
                !CheckForTile(currentCol + 1, currentRow - 2) &&

                !CheckForTile(currentCol - 1, currentRow - 3) &&
                !CheckForTile(currentCol + 0, currentRow - 3))
            {                
                retVal = false;
            }
        }
        else if (direction == Direction.right)
        {
            if(!CheckForTile(currentCol + 1, currentRow - 1))
            {
                retVal = false;
            }
        } else if (direction == Direction.down)
        {
            //check for down
        }
        return retVal;
    }

    /// <summary>
    /// Provjerava imali na tim kordinatama tile i vraća true ako ima.
    /// Ako su kordinate van grida vraća true.
    /// </summary>
    /// <param name="col"></param>
    /// <param name="row"></param>
    /// <returns></returns>
    private bool CheckForTile(int col, int row)
    {       
        GameObject go = GetNode(col, row);
        bool retVal = false;
        if(go == null)
        {
            retVal = true;
        }
        else if (go.GetComponent<Node>().tileType == Node.TileType.dirt ||
            go.GetComponent<Node>().tileType == Node.TileType.snow)
        {
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

    /// <summary>
    /// Ispisuje u debug logu taj tile i poruku.
    /// </summary>
    /// <param name="col"></param>
    /// <param name="row"></param>
    /// <param name="message"></param>
    private void DebugLogTile(int col, int row, string message, bool warning = false)
    {
        if (!warning)
        {
            Debug.Log("(" + col + ", " + row + ") -> " + message); 
        } else
        {
            Debug.LogWarning("(" + col + ", " + row + ") -> " + message);
        }
    }

    /// <summary>
    /// Generira se soba sa tim rednim brojem.
    /// </summary>
    /// <param name="room"></param>
    private void GenerateRoom(int room)
    {
        GenerateFloorDimensions();
        for (int col = cols * room; col < (cols + cols * room); col++)
        {
            
            for (int row = 0; row < rows; row++)
            {
                if (row >= 0 && row < floorA)
                {
                    CreateTile(col, row, Node.TileType.empty);
                }
                else if (row > floorA && row < floorB)
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
                }
                else
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
                            if (col == cols + cols * room)
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
                if (go.GetComponent<Node>().tileType == Node.TileType.dirt)
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
        if (col >= cols * numberOfRooms || row >= rows)
        {
            Debug.LogWarning("CreateTile je problem");
            return null;
        }
        DebugLogTile(col, row, "radim tile", true);
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
        if (posX >= cols || posY >= rows || posX<0 || posY<0)
        {
            retVal = null;
        } else
        {
            retVal = grid[posX, posY];
        }
        
        return retVal;
    }

    /// <summary>
    /// Generira na kojim će se mjestima nalaziti katovi.
    /// </summary>
    private void GenerateFloorDimensions() //POPRAVITI 
    {
        floorA = RandomNumber(4,4);
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

    private enum Direction
    {
        up,
        down,
        right,
    }
}

