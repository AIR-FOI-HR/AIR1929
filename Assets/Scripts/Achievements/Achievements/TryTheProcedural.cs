using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TryTheProcedural : MonoBehaviour, IAchievement {
	public string Name { get; set; } = "Welcome to the jungle";
	public string Description { get; set; } = "Try procedurally generated map for the first time.";
	public bool Achieved { get; set; }
	public string TargetObjectName { get; set; }
	public string[] EligibleScenes { get; set; } = { "ProceduralMap" };

	public void Initialize() {
		if (PPAchivements.LoadAchievement(Name) == 1) Achieved = true;
		else Achieved = false;
		
	}
	void Start() {
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		if (!Achieved && SceneManager.GetActiveScene().name == "ProceduralMap") {
			PPAchivements.UpdateAchivement(Name, 1);
			Initialize();
			Debug.Log("You tried the procedural map");
		}
	}

}
