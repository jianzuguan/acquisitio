using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour {

	private static int[] score = {0, 0}; //{red, blue}
	private static Text red;
	private static Text blue;

	// Use this for initialization
	void Start () {
		red = GameObject.Find ("Red Score").GetComponent <Text> ();
		blue = GameObject.Find ("Blue Score").GetComponent <Text> ();
	}

	public static void IncrementScore(Team t){
		if (t != Team.NONE) {
			score [(int)t]++;
			red.text = score [(int) Team.RED].ToString ();
			blue.text = score [(int) Team.BLUE].ToString ();
		}
	}
}
