using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainModSlow : MonoBehaviour, IInteractable<float>{
	public bool Triggered { get => triggered; set => triggered = false; }
	private bool triggered;

	public float slowAmount = 0;

	public float Effect() {
		return slowAmount;
	}

	// Update is called once per frame
	private void OnTriggerEnter2D(Collider2D collision) {

		Debug.Log(slowAmount);
	}

	private void OnTriggerExit2D(Collider2D collision) {
		Debug.Log("IZASO");
	}
}
