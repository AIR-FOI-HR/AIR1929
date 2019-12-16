using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public interface ITileMaster{
	GameObject CreateTile(string tileName);
}
