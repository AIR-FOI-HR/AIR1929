using UnityEngine;
using UnityEngine.Events;

public interface IAchievement {
	string Name { get; set; }
	string Description { get; set; }
	bool Achieved { get; set; }
	string TargetObjectName { get; set; }
	string[] EligibleScenes { get; set; }

	void Initialize();
}
