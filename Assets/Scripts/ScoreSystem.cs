using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour {

	private static int[] score = {0, 0}; //{red, blue}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void IncrementScore(Team t){
		if (t != Team.NONE) {
			score [(int)t]++;
			print("RED: " + score[0] + " BLUE: " + score[1]);
		}
	}
}
