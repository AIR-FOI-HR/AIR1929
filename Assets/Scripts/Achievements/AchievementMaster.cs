using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AchievementMaster : MonoBehaviour {

	List<IAchievement> achivements;
	public GameObject achPrefab;
	public Sprite finSprite;
	void Start()
    {
		DontDestroyOnLoad(gameObject);

		achivements = new List<IAchievement>() {
			gameObject.AddComponent<FinishWinterRun>(),
			gameObject.AddComponent<FinishSantasMarathoner>(),
			gameObject.AddComponent<FinishSampleMap>(),
			gameObject.AddComponent<WinterRunJumper>()
		};
		BuildAchDisplay();
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
		if (SceneManager.GetActiveScene().name != "Achievements") BuildAchDisplay();//DistributeAchivements();
		else BuildAchDisplay();
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

	void BuildAchDisplay() {
		Debug.Log(achivements.Count);
		var vertList = GameObject.Find("General");
		foreach (var ach in achivements) {
			ach.Initialize();
			Debug.Log(ach.Achieved);
			var prefo = Instantiate(achPrefab, vertList.transform);
			if (ach.Achieved) {
				prefo.GetComponent<UnityEngine.UI.Image>().sprite = finSprite;
			}
			prefo.transform.localScale = Vector3.one;
			prefo.transform.Find("Title").GetComponent<UnityEngine.UI.Text>().text = ach.Name;
			prefo.transform.Find("Description").GetComponent<UnityEngine.UI.Text>().text = ach.Description;
			prefo.transform.Find("Image").GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>("AchievenmentSprites/"+ach.Name);
		}
		
	}
}
