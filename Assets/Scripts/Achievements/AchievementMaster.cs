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
			gameObject.AddComponent<FinishWinterRun>()
		};

		GetUnachievedCollection();

		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		GetUnachievedCollection();
		DistributeAchivements();
	}

	void GetUnachievedCollection() {
		List<IAchievement> toDestroyAchieved = new List<IAchievement>();
		foreach (var ach in achivements) {
			ach.Initialize();
			if (ach.Achieved) toDestroyAchieved.Add(ach);
		}
		foreach (var ach in achivements) {
			Destroy(gameObject.GetComponent(ach.GetType()));
		}
		achivements = achivements.Except(toDestroyAchieved).ToList();
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
