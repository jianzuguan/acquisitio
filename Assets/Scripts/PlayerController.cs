using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public Team team;
	public float moveSpeed = 100.0f;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		float right = 0;
		float up = 0;

		if (team == Team.RED) {
			right = moveSpeed * Input.GetAxis ("p1_h");
			up = moveSpeed * Input.GetAxis ("p1_v");
		} else if (team == Team.BLUE) {
			right = moveSpeed * Input.GetAxis ("p2_h");
			up = moveSpeed * Input.GetAxis ("p2_v");
		}

		right *= Time.deltaTime;
		up *= Time.deltaTime;

		transform.Translate (right, up, 0);
	}
}
