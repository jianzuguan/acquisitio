using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaddieController : PlayerController {

	private GameObject baseFactory;

	public BaseController lastVisitedBase = null;

	// Use this for initialization
	void Start () {
		baseFactory = GameObject.Find ("Base Factory");
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 destination = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
		BaseController destC = null;

		foreach (Transform b in baseFactory.transform) {
			BaseController baseC = b.GetComponent <BaseController> ();
			//If base is nearer than current destination...
			if (Vector3.Distance (transform.position, b.position) < Vector3.Distance (transform.position, destination)) {
				///...and it's not the last visited base or occupied by a red player...
				if (baseC != lastVisitedBase && baseC.occupants [(int)Team.RED] == 0) {
					//...and that it's not 100% blue already
					if (baseC.team != Team.BLUE || baseC.changing) {
						destination = b.position;
						destC = baseC;
					}
				}
			}
		}

		if (destination.z == float.MaxValue) {
			destination = new Vector3 (0.0f, 0.0f, 0.0f);
		}
			
		Vector3 direction = destination - transform.position;
		float randomness = Random.Range (-40.0f, 40.0f);
		direction = Quaternion.Euler(0.0f, randomness ,0.0f) * direction;
		direction = direction.normalized;

		float right = direction.x * moveSpeed * Time.deltaTime;
		float up = direction.y * moveSpeed * Time.deltaTime;

		transform.Translate (right, up, 0);
	}
}
