using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapFall : MonoBehaviour, IInteractable<Object>{
	public bool Triggered { get => triggered; set => triggered = false; }
	private bool triggered;

	/// <summary>
	/// Masa objekta kada postane dinamican
	/// </summary>
	public int mass = 1;

	/// <summary>
	/// Skala gravitacije kada objekt postane dinamican
	/// </summary>
	public int gravityScale = 1;

	/// <summary>
	/// Metoda postavlja objektovu komponentu RigidBody2D iz static u dynamic i postavlja parametre
	/// </summary>
	/// <returns></returns>
	public Object Effect() {
		gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
		gameObject.GetComponent<Rigidbody2D>().mass = mass;
		gameObject.GetComponent<Rigidbody2D>().gravityScale = gravityScale;
		Destroy(gameObject, 2f);
		return null;
	}

	void Awake(){
		gameObject.AddComponent<Rigidbody2D>();
		gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }

	private void OnTriggerEnter2D(Collider2D collision) {
		if (!triggered) {
			triggered = true;
			Effect();
		}
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (!triggered) {
			triggered = true;
			Effect();
		}
	}
}
