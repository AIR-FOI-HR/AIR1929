using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapEditorRunner : MonoBehaviour {

	private Grid grid;
	public List<string> simples; // lista imena "obicnih" tileova

	private void Start() {
		// ucitavanje svih tileova u mapi resources (TODO: Resources\Simple folder) i spremanje njihovih naziva u
		// listu simples koja predstavlja kolekciju imena bicnih resursa
		List<GameObject> allRes;
		allRes = new List<GameObject>(Resources.LoadAll<GameObject>(""));
		foreach (GameObject res in allRes) simples.Add(res.name);
		grid = new Grid(10, 4, 10f, new Vector3(-50,-20), simples);
	}

	// fukcija za hvacanje inputa korisnika (TODO: uvjeti, ocito)
	private void Update() {
		if (Input.GetMouseButtonDown(0)) {
			grid.SetTile(GetMouseWorldPosition());
		}
		else if (Input.GetMouseButtonDown(1)) {
			grid.DelTile(GetMouseWorldPosition());
		}
		else if (Input.GetKeyDown("1")) {
			grid.LoadResource(1);
		}
		else if (Input.GetKeyDown("2")) {
			grid.LoadResource(2);
		}
		else if (Input.GetKeyDown("3")) {
			grid.LoadResource(3);
		}
	}

	// funkcije za dohvacanje pozicije kursora
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
