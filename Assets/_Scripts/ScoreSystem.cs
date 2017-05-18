using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour {

    private int[] score = { 0, 0 }; //{red, blue}
    private Text red;
    private Text blue;
	private GameState gs;

    // Use this for initialization
    void Start() {
		gs = GameObject.Find ("GameState").GetComponent <GameState> ();
		red = GameObject.Find("Red Score").GetComponent<Text>();
		if (gs.isRunning) {
            blue = GameObject.Find("Blue Score").GetComponent<Text>();
        }
    }

    public void IncrementScore(Team t) {
        if (t != Team.NONE) {
            score[(int)t]++;
            red.text = score[(int)Team.RED].ToString();
			if (gs.isRunning) {
				blue.text = score[(int)Team.BLUE].ToString();
			}
        }
    }

    public int GetScore(Team t) {
        return score[(int)t];
    }

    public Team GetWiningTeam() {
        if (score[(int)Team.RED] > score[(int)Team.BLUE]) {
            return Team.RED;
        } else if (score[(int)Team.RED] < score[(int)Team.BLUE]) {
            return Team.BLUE;
        } else {
            return Team.NONE;
        }
    }

    public void ResetScore() {
        score[(int)Team.RED] = 0;
        score[(int)Team.BLUE] = 0;
        red.text = score[(int)Team.RED].ToString();
        blue.text = score[(int)Team.BLUE].ToString();
    }
}
