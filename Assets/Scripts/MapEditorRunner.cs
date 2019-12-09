using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Testing : MonoBehaviour {

	private Grid grid;

	private void Start() {
		grid = new Grid(4, 2, 10f, new Vector3(-20,-10));
	}

	private void Update() {
		if (Input.GetMouseButtonDown(0)) {
			GetMouseWorldPosition();
			grid.SetValue(GetMouseWorldPosition(), 54);
		}
	}

	private Vector3 GetMouseWorldPosition() {
		Vector3 position = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
		position.z = 0f;
		return position;

	}

	private static Vector3 GetMouseWorldPositionWithZ() {
		return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
	}

	private static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera) {
		return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
	}

	private static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera) {
		return worldCamera.ScreenToWorldPoint(screenPosition);
	}
}
