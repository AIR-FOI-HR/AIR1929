using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour{

	private int width, height; // visina i širina grida
	private float cellSize; // velicina celija
	private Vector3 originPosition; // pozicija izvorista grida (primarna svrha korekcija/centriranje na ekranu (nakon implementacije kamere, nece biti bitno)
	GameObject[,] tileArr; // služi za praćenje objekata kako se ne bi stvarali duplikati
	List<string> simples; // lista sa svim "obicnim" tileovima
	int chosenRes = 0; // odabrani index tilea (TODO: stardardizacija jer sada radi samo sa "obicnim" tileovima)

	public Grid(int width, int height, float cellSize, Vector3 originPosition, List<string> simples) {
		this.simples = simples;
		this.width = width;
		this.height = height;
		this.cellSize = cellSize;
		this.originPosition = originPosition;
		tileArr = new GameObject[width, height];

		// stvaranje "gizmo" grida
		for(int x = 0; x<width; x++)
			for(int y = 0; y<height; y++) {
				Debug.DrawLine(GetWorldPosition(x, y) , GetWorldPosition(x, y + 1) , Color.white, 100f);
				Debug.DrawLine(GetWorldPosition(x, y) , GetWorldPosition(x + 1, y) , Color.white, 100f);
			}
		Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height) , Color.white, 100f);
		Debug.DrawLine(GetWorldPosition(width, 0) , GetWorldPosition(width, height) , Color.white, 100f);
	}

	// selekcija resursa
	public void LoadResource(int i) {
		chosenRes = i - 1;
	}

	// funkcija koja generira i prilagodava tile (TODO: naziv) i azurira evidenciju postojecih tileova
	private void GenerateGrid(int x, int y) {
		Destroy(tileArr[x, y]);
		ITileMaster tile = new SimpleTile();
		GameObject go = tile.CreateTile(simples[chosenRes]);
		go.transform.position = GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f;
		go.transform.localScale = new Vector3(cellSize-2, cellSize-2);
		tileArr[x, y] = go;
	}

	// funkcija dohvaca poziciju u "svijetu"
	private Vector3 GetWorldPosition(int x, int y) {
		return new Vector3(x,y) * cellSize + originPosition;
	}

	// pozicju u "svijetu" pretvara u koordinate
	private void GetXY(Vector3 worldPosition, out int x, out int y) {
		x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
		y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
	}

	// stvaranje tilea
	public void SetTile(Vector3 worldPosition) {
		int x, y;
		GetXY(worldPosition, out x, out y);
		SetTile(x, y);
	}

	public void SetTile(int x, int y) {
		if (x >= 0 && y >= 0 && x < width && y < height) {
			GenerateGrid(x, y);
		}
	}

	// brisanje tilea
	public void DelTile(Vector3 worldPosition) {
		int x, y;
		GetXY(worldPosition, out x, out y);
		DeleteGrid(x, y);
	}

	private void DeleteGrid(int x, int y) {
		Destroy(tileArr[x, y]);
	}
}
