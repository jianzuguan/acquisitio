﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGenerator : MonoBehaviour {
    public int iteration = 2;

    public float boundaryXNegative;
    public float boundaryXPositive;
    public float boundaryYNegative;
    public float boundaryYPositive;

    [Header("Private")]
    [SerializeField] private GameObject backgroundTile;
    [SerializeField] private int length = 2;
    [SerializeField] private int step = 10;
    private Vector3 tilePosition;
    private GameObject tile;
    private GoogleMapOnSprite gmsPrevious;
    private GoogleMapOnSprite gms;

    // Use this for initialization
    void Start() {
        GameState gs = GameObject.Find("GameState").GetComponent<GameState>();

        float latDiff = gs.latDiff;
        float lonDiff = gs.lonDiff;
        // Generate map background
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

        // Set boundarys for the play area
        boundaryXNegative = -step * iteration;
        boundaryXPositive = step * iteration;
        boundaryYNegative = -step * iteration;
        boundaryYPositive = step * iteration;
    }

    // Update is called once per frame
    void Update() {

    }

    // Map background is made up with tiles
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
