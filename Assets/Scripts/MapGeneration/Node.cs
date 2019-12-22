using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    void Start()
    {
 
    }


    /// <summary>
    /// Stvara novi node sa odabranim tipom.
    /// </summary>
    /// <param name="type"></param>
    /// <returns>GameObject</returns>
    public GameObject CreateNode(TileType type)
    {
        GameObject referenceTile = null;
        switch (type)
        {
            case TileType.dirt:
                referenceTile = (GameObject)Instantiate(Resources.Load("TileDirt"));
                break;
            case TileType.snow:
                referenceTile = (GameObject)Instantiate(Resources.Load("TileSnow"));
                break;
            case TileType.empty:
                referenceTile = (GameObject)Instantiate(Resources.Load("TileEmpty"));
                break;

        }    
        return referenceTile;
    }
    

    public TileType tileType;
    public enum TileType
    {
        dirt,
        snow,
        empty
    }

}
