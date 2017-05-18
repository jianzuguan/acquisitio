using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

    public ScoreSystem ss;
    public DirectionController dc;
    public int scoreNeeded = 2;

	// Use this for initialization
	void Start () {
		if (ss == null ) {
            ss = GetComponent<ScoreSystem>();
        }
        if (dc == null ) {
            dc = GameObject.Find("Player").GetComponent<DirectionController>();
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (ss.GetScore(dc.team) >= scoreNeeded) {
            SceneManager.LoadScene("__Scenes/Game");
        }
	}
}
