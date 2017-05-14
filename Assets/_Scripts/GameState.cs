using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {
    public float latOrigin = 50.93574f;
    public float lonOrigin = -1.396641f;

    public float latPerUnit = 0.000339f;
    public float lonPerUnit = 0.000536f;

    // Gaps of background tiles
    public float latDiff = 0.00339f;
    public float lonDiff = 0.00536f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
