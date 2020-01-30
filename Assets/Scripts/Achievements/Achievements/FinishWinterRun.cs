using UnityEngine;
using UnityEngine.Events;

public class FinishWinterRun : MonoBehaviour, IAchievement {
	public string Name { get; set; } = "It's cold outside like your ex's heart.";
	public string Description { get; set; } = "Finish \"Winter Run\" map for the first time.";
	public Sprite ImageAchieved { get; set; }
	public Sprite ImageNotAchieved { get; set; }
	public bool Achieved { get; set; }
	public string TargetObjectName { get; set; } = "WinterRunFlag";
	public string[] EligibleScenes { get; set; } = { "WinterRun (Map)" };


	public void Initialize() {
		if (PPAchivements.LoadAchievement(Name) == 1) Achieved = true;
		else Achieved = false;
	}

	bool triggered = false;
	public void OnTriggerEnter2D(Collider2D collider) {
		if(collider.tag == "Player" && !triggered) {
			triggered = true;
			PPAchivements.UpdateAchivement(Name, 1);
			Debug.Log("You got \"" + Name + "\" achievement!");
		}
	}

}
