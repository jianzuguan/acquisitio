using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

    public ScoreSystem ss;
    public DirectionController dc;
    public int scoreNeeded = 2;
	private bool hasStarted = false;

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
		print (ss.GetScore (dc.team));
		if (ss.GetScore(dc.team) >= scoreNeeded && hasStarted == false) {
			hasStarted = true;
			SceneManager.LoadScene("__Scenes/Game");
        }
	}
}
