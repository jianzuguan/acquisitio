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
		float right = moveSpeed * Input.GetAxis ("Horizontal");
		float up = moveSpeed * Input.GetAxis ("Vertical");

		right *= Time.deltaTime;
		up *= Time.deltaTime;

		transform.Translate (right, up, 0);
	}
}
