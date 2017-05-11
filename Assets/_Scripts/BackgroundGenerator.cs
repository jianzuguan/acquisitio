using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGenerator : MonoBehaviour {
    public GameObject backgroundTile;

    public int iteration = 2;
    public int length = 2;
    public int step = 10;

	// Use this for initialization
	void Start () {
        Vector3 tilePosition = transform.position;
        Instantiate(backgroundTile, tilePosition, Quaternion.identity);

        for (int it = 0; it < iteration; it++) {

            tilePosition = new Vector3(tilePosition.x, tilePosition.y + step, tilePosition.z);
            Instantiate(backgroundTile, tilePosition, Quaternion.identity);

            for (int position = 1; position < length; position++) {
                tilePosition = new Vector3(tilePosition.x + step, tilePosition.y, tilePosition.z);
                Instantiate(backgroundTile, tilePosition, Quaternion.identity);
            }

            for (int position = 0; position < length; position++) {
                tilePosition = new Vector3(tilePosition.x, tilePosition.y - step, tilePosition.z);
                Instantiate(backgroundTile, tilePosition, Quaternion.identity);
            }

            for (int position = 0; position < length; position++) {
                tilePosition = new Vector3(tilePosition.x - step, tilePosition.y, tilePosition.z);
                Instantiate(backgroundTile, tilePosition, Quaternion.identity);
            }

            for (int position = 0; position < length; position++) {
                tilePosition = new Vector3(tilePosition.x, tilePosition.y + step, tilePosition.z);
                Instantiate(backgroundTile, tilePosition, Quaternion.identity);
            }
            length += 2;

        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
