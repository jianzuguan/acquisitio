using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGenerator : MonoBehaviour {
    public int iteration = 2;

    [Header("Private")]
    [SerializeField]
    private GameObject backgroundTile;
    [SerializeField] private int length = 2;
    [SerializeField] private int step = 10;
    [SerializeField] private float latDiff = 0.00339f;
    [SerializeField] private float lonDiff = 0.00536f;
    private Vector3 tilePosition;
    private GameObject tile;
    private GoogleMapOnSprite gmsPrevious;
    private GoogleMapOnSprite gms;

    // Use this for initialization
    void Start() {
        tilePosition = transform.position;
        GenerateTile(0, 0, 0, 0);

        for (int it = 0; it < iteration; it++) {
            GenerateTile(0, step, latDiff, 0);

            for (int position = 1; position < length; position++) {
                GenerateTile(step, 0, 0, lonDiff);
            }

            for (int position = 0; position < length; position++) {
                GenerateTile(0, -step, -latDiff, 0);
            }

            for (int position = 0; position < length; position++) {
                GenerateTile(-step, 0, 0, -lonDiff);
            }

            for (int position = 0; position < length; position++) {
                GenerateTile(0, step, latDiff, 0);
            }

            length += 2;

        }
    }

    // Update is called once per frame
    void Update() {

    }

    private void GenerateTile(float xDiff, float yDiff, float latDiff, float lonDiff) {
        tilePosition = new Vector3(tilePosition.x + xDiff, tilePosition.y + yDiff, tilePosition.z);
        tile = Instantiate(backgroundTile, tilePosition, Quaternion.identity, transform);
        gms = tile.GetComponent<GoogleMapOnSprite>();
        gms.UpdateMap(gmsPrevious, latDiff, lonDiff);
        gmsPrevious = gms;
    }
}

/*
 * note
 * 
 * lat: 50.93574
 * lat: 50.93913
 * lat diff: 0.00339
 * 
 * lon: -1.39664
 * lon: -1.39128
 * lon diff: 0.00536
 */
