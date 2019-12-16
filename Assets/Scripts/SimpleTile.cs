using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTile : MonoBehaviour, ITileMaster {
	public GameObject CreateTile(string tileName) {
		return (GameObject)Instantiate(Resources.Load(tileName));
	}
}
