using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PPAchivements {
	public static void UpdateAchivement(string name, int progress) {
		PlayerPrefs.SetInt(name, progress);
		PlayerPrefs.Save();
	}

	public static int LoadAchievement(string name) {
		return PlayerPrefs.GetInt(name);
	}
}
