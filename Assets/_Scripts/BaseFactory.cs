﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFactory : MonoBehaviour {

	//Inspector editable
	public Transform basePrefab;
	public int maxBases = 8;
	public int gridSize = 8;

	public Transform[,] grid;
	public float mapSize;
	private float interval;
	private int numberOfBases;

	private GameState gs;

	void Start () {
		grid = new Transform[gridSize, gridSize];
		gs = GameObject.Find ("GameState").GetComponent <GameState> ();
		if (!gs.isRunning) {
			gridSize = 2;
		}
		numberOfBases = maxBases;
		interval = mapSize / gridSize;

		for (int i = 0; i < maxBases; i++) {
			SpawnBase ();
		}
	}

	void Update () {
		if (numberOfBases < maxBases) {
			numberOfBases++;
			SpawnBase ();
		}
	}

	private void SpawnBase(){
		//Get random place in grid
		int x = 0;
		int y = 0;
		bool spotFound = false;
		while (!spotFound) {
			x = Random.Range (0, gridSize);
			y = Random.Range (0, gridSize);
			if (grid [x, y] == null) {
				spotFound = true;
			}
		}

		//Scale
		float mapX = x * interval;
		float mapY = y * interval;
		//Center
		mapX += interval / 2.0f;
		mapY += interval / 2.0f;
		//Adjust
		mapX += -mapSize / 2.0f;
		mapY += -mapSize / 2.0f;

		//Instantiate from prefab
		GameObject b = Instantiate(basePrefab, new Vector3(mapX, mapY, -5.0f), Quaternion.identity).gameObject;
		b.transform.parent = transform;
		grid [x, y] = b.transform;

		BaseController bc = b.GetComponent<BaseController> ();
		bc.x = x;
		bc.y = y;

		Invoke ("DecrementBases", (float) bc.StartCountDown ());
	}

	private void DecrementBases(){
		numberOfBases--;
	}

	public void ClearBases(){
		foreach (Transform b in transform) {
			Destroy (b.gameObject);
		}
	}

	public void Restart(){
		for (int i = 0; i < maxBases; i++) {
			SpawnBase ();
		}
	}
}
