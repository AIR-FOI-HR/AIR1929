using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinishSantasMarathoner : MonoBehaviour, IAchievement {
	public string Name { get; set; } = "Christmas is over :(...";
	public string Description { get; set; } = "Finish \"Santa's Marathoner\" map for the first time.";
	public Sprite ImageAchieved { get ; set; }
	public Sprite ImageNotAchieved { get; set; }
	public bool Achieved { get; set; }
	public string TargetObjectName { get; set; } = "SantasMarathonerFlag";
	public string[] EligibleScenes { get; set; } = { "Santa'sMarathoner" };

	public void Initialize() {
		if (PPAchivements.LoadAchievement(Name) == 0) {
			Achieved = false;
		}
		else Achieved = true;
	}

	bool triggered = false;
	public void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == "Player" && !triggered) {
			triggered = true;
			PPAchivements.UpdateAchivement(Name, 1);
			Debug.Log("You got \"" + Name + "\" achievement!");
		}
	}
}
