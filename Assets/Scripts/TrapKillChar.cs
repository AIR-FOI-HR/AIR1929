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

	private void OnTriggerEnter2D(Collider2D other) {
        if (!triggered)
        {
            triggered = true;
            Transform[] children = GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<Transform>();
            bool flag = true;
            foreach (var item in children)
            {
                if (item.CompareTag("Shield"))
                {
                    Destroy(item.gameObject);
                    flag = false;
                    break;
                }
            }
            if (flag)
            {
                other.gameObject.GetComponent<PlayerControls>().currentSpeed = 0;
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 20, ForceMode2D.Impulse);
            }
        }
    }

	private void OnCollisionEnter2D(Collision2D other) {
		if (!triggered) {
			triggered = true;
            Transform[] children = GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<Transform>();
            bool flag = true;
            foreach (var item in children)
            {
                if (item.CompareTag("Shield"))
                {
                    Destroy(item.gameObject);
                    flag = false;
                    break;
                }
            }
            if (flag)
            {
                other.gameObject.GetComponent<PlayerControls>().currentSpeed = 0;
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 20, ForceMode2D.Impulse);
            }  
		}
	}
}
