using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaddieController : PlayerController {

	public float maxStrayAngle = 90.0f;
	private float strayAngle = 0.0f;

	// Update is called once per frame
	void Update () {
		Vector3 destination = getDestination ();

		if (destination.z != float.MaxValue) {
			Vector3 direction = destination - transform.position;
			direction.z = 0.0f;
			strayAngle += Random.Range (-15.0f, 15.0f);
			strayAngle = Mathf.Min (strayAngle, maxStrayAngle);
			strayAngle = Mathf.Max (strayAngle, -maxStrayAngle);

			direction = Quaternion.Euler(0.0f, 0.0f, strayAngle) * direction;
			direction.Normalize ();

			float right = direction.x * moveSpeed * Time.deltaTime;
			float up = direction.y * moveSpeed * Time.deltaTime;

			transform.Translate (right, up, 0);
		}
	}
}
