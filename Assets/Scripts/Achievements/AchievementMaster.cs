using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AchievementMaster : MonoBehaviour {

	List<IAchievement> achivements;
	void Start()
    {
		DontDestroyOnLoad(gameObject);

		achivements = new List<IAchievement>() {
			gameObject.AddComponent<FinishWinterRun>(),
			gameObject.AddComponent<FinishSantasMarathoner>(),
			gameObject.AddComponent<FinishSampleMap>(),
			gameObject.AddComponent<WinterRunJumper>()
		};

		GetUnachievedAndEligibleCollection();

		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void Update() {
		if (Input.GetKeyDown("x")) {
			var comps = GetComponents<IAchievement>();
			foreach(var comp in comps) {
				PPAchivements.DeleteAchievement(comp.Name);
			}
		}
	}

	void RebuildAchivementsList() {
		achivements = new List<IAchievement>();
		var comps = GetComponents<IAchievement>();
		foreach(var comp in comps) {
			achivements.Add(comp);
		}
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		RebuildAchivementsList();
		GetUnachievedAndEligibleCollection();
		DistributeAchivements();
	}

	void GetUnachievedAndEligibleCollection() {
		List<IAchievement> achievedOrNotEligible = new List<IAchievement>();
		foreach (var ach in achivements) {
			ach.Initialize();
			if (ach.Achieved || !ach.EligibleScenes.Contains<string>(SceneManager.GetActiveScene().name)) achievedOrNotEligible.Add(ach);
		}
		achivements = achivements.Except(achievedOrNotEligible).ToList();
	}

	void DistributeAchivements() {
		foreach(var ach in achivements) {
			try {
				var comp = GameObject.Find(ach.TargetObjectName);
				comp.AddComponent(ach.GetType());
			}
			catch { 
				Debug.Log(ach.TargetObjectName + " not Found!");
			}
		}
	}
}
