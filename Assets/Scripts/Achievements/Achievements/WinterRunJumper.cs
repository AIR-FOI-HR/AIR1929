using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinterRunJumper : MonoBehaviour, IAchievement {
	public string Name { get; set; } = "The floor is lava! But cold";
	public string Description { get; set; } = "Jump 15 times while playing on \"Winter Run\" map.";
	public bool Achieved { get; set; }
	public string TargetObjectName { get; set; }
	public string[] EligibleScenes { get; set; } = { "WinterRun (Map)" };

	int jumps = 0;
	public void Initialize() {
		if (PPAchivements.LoadAchievement(Name) < 15) {
			Achieved = false;
			return;
		}
		else Achieved = true;
	}

	void Start() {
		TargetObjectName = PlayerPrefs.GetString("PlayerName");
		if (gameObject.name != TargetObjectName) enabled = false;
	}

	void FixedUpdate() {
		if (Input.GetButtonDown("Jump") == true) {
			if (++jumps <= 15) PPAchivements.UpdateAchivement(Name, jumps);
			if (jumps == 15) Debug.Log("You got \"" + Name + "\" achievement!");
		}
	}
}
