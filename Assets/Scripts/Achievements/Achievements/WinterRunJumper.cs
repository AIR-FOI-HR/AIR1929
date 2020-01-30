using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinterRunJumper : MonoBehaviour, IAchievement {
	public string Name { get; set; } = "The floor is lava! But cold...";
	public string Description { get; set; } = "Jump 15 times while playing on \"Winter Run\" map.";
	public Sprite ImageAchieved { get; set; }
	public Sprite ImageNotAchieved { get; set; }
	public bool Achieved { get; set; }
	public string TargetObjectName { get; set; } = "FemaleNinja";
	public string[] EligibleScenes { get; set; } = { "WinterRun (Map)" };

	public void Initialize() {
		if (PPAchivements.LoadAchievement(Name) < 15) {
			Achieved = false;
		}
		else Achieved = true;
	}

	
}
