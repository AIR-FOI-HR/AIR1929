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
			gameObject.AddComponent<FinishSampleMap>()
		};

		GetUnachievedCollection();

		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		GetUnachievedCollection();
		DistributeAchivements();
	}

	void GetUnachievedCollection() {
		List<IAchievement> achieved = new List<IAchievement>();
		foreach (var ach in achivements) {
			ach.Initialize();
			if (ach.Achieved) achieved.Add(ach);
		}
		achivements = achivements.Except(achieved).ToList();
	}

	void DistributeAchivements() {
		foreach(var ach in achivements) {
			try {
				var comp = GameObject.Find(ach.TargetObjectName);
				var a = comp.AddComponent(ach.GetType());
				comp.GetComponent(ach.GetType());
			}
			catch { 
				Debug.Log(ach.TargetObjectName + " not Found!");
			}
		}
	}
}
