using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid {

	private int width, height;
	private int[,] arr;
	private float cellSize;
	private TextMesh[,] debugTextArray;
	private Vector3 originPosition;


	public Grid(int width, int height, float cellSize, Vector3 originPosition) {
		this.width = width;
		this.height = height;
		this.cellSize = cellSize;
		this.originPosition = originPosition;
		arr = new int[width, height];
		debugTextArray = new TextMesh[width, height];

		for(int x = 0; x<width; x++)
			for(int y = 0; y<height; y++) {
				debugTextArray[x, y] = CreateTextMesh(arr[x, y].ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f , TextAnchor.MiddleCenter, TextAlignment.Center, Color.white, 40, 1);
				Debug.DrawLine(GetWorldPosition(x, y) , GetWorldPosition(x, y + 1) , Color.white, 100f);
				Debug.DrawLine(GetWorldPosition(x, y) , GetWorldPosition(x + 1, y) , Color.white, 100f);
			}
		Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height) , Color.white, 100f);
		Debug.DrawLine(GetWorldPosition(width, 0) , GetWorldPosition(width, height) , Color.white, 100f);

		SetValue(2, 1, 56);
	}

	public static TextMesh CreateTextMesh(string text, Transform parent, Vector3 localPosition, TextAnchor txtAnchor, TextAlignment txtAlignment, Color color, int fontSize, int sortingOrder) {
		GameObject go = new GameObject("Hello World", typeof(TextMesh));
		Transform transform = go.transform;
		transform.SetParent(parent, false);
		transform.localPosition = localPosition;
		TextMesh txtMesh = go.GetComponent<TextMesh>();
		txtMesh.anchor = txtAnchor;
		txtMesh.alignment = txtAlignment;
		txtMesh.text = text;
		txtMesh.fontSize = fontSize;
		txtMesh.color = color;
		txtMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
		return txtMesh;
	}

	private Vector3 GetWorldPosition(int x, int y) {
		return new Vector3(x,y) * cellSize + originPosition;
	}

	public void SetValue(int x, int y, int value) {
		if (x >= 0 && x >= 0 && x < width && y < height) {
			arr[x, y] = value;
			debugTextArray[x, y].text = value.ToString();
		}
	}

	private void GetXY(Vector3 worldPosition, out int x, out int y) {
		x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
		y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
	}

	public void SetValue(Vector3 worldPosition, int value) {
		int x, y;
		GetXY(worldPosition, out x, out y);
		SetValue(x, y, value);
	}
}
