using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFactory : MonoBehaviour {

	//Inspector editable
	public Transform player;
	public Transform basePrefab;
	public static int numberToStart = 2;
	public static int numberToEnd = 10;
	public float spawnFrequency = 120.0f; //can change / speed up
	public static float mapSize = 2500.0f;

	private Transform[] bases = new Transform[numberToEnd];
	private int numberOfBases = 0;

	// Use this for initialization
	void Start () {
		if (numberToEnd < numberToStart) {
			print ("ERROR: MORE STARTING BASES THAN ENDING AMOUNT");
		} else {
			for (int i = 0; i < numberToStart; i++) {
				SpawnBase ();
			}

			float x = bases [0].position.x;
			float y = bases [0].position.y;

			player.transform.position = new Vector3 (x, y, player.transform.position.z);

			InvokeRepeating ("SpawnBase", spawnFrequency, spawnFrequency);
		}
	}

	private void SpawnBase(){
		//Get random point in map
		float halfMapSize = mapSize / 2.0f;
		float x = Random.Range (-halfMapSize, halfMapSize);
		float y = Random.Range (-halfMapSize, halfMapSize);

		//Instantiate from prefab
		GameObject b = Instantiate(basePrefab, new Vector3(x, y, 0.0f), Quaternion.identity).gameObject;
		b.transform.parent = transform;

		bases [numberOfBases] = b.transform;
		numberOfBases++;
	}
}
