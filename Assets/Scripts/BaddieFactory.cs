using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaddieFactory : MonoBehaviour {

	public Transform baddiePrefab;
	public static int numberToStart = 2;
	public float spawnFrequency = 60.0f; //can change / speed up
	public static float mapSize = 2500.0f;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < numberToStart - 1; i++) {
			SpawnBaddie ();
		}
		InvokeRepeating ("SpawnBaddie", 0.0f, spawnFrequency);
	}

	private void SpawnBaddie(){
		//Get random point in map
		float halfMapSize = mapSize / 2.0f;
		float x = Random.Range (-halfMapSize, halfMapSize);
		float y = Random.Range (-halfMapSize, halfMapSize);

		//Instantiate from prefab
		GameObject b = Instantiate(baddiePrefab, new Vector3(x, y, -1.0f), Quaternion.identity).gameObject;
		b.transform.parent = transform;
	}
}
