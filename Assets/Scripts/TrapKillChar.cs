using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Skripta pokreće smrt karaktera pri kolizuiju s objektom koji ju ima
/// </summary>
public class TrapKillChar : MonoBehaviour, IInteractable<Object>{
	public bool Triggered { get => triggered; set => triggered = false ; }
	private bool triggered;

	/// <summary>
	/// Metoda pokređe "smrt" karaktera
	/// </summary>
	/// <returns></returns>
	public Object Effect() {
		return null;
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (!triggered) {
			triggered = true;
			Debug.Log("TRAP HIT!");
		}
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (!triggered) {
			triggered = true;
			Debug.Log("TRAP HIT!");
		}
	}
}
