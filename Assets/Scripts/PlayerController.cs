using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public Team team;
	public float moveSpeed = 100.0f;

	private GameObject baseFactory;
	private GameObject compass;
	private BaseController lastVisitedBase;

	// Use this for initialization
	void Start () {
		baseFactory = GameObject.Find ("Base Factory");
		compass = GameObject.Find ("Compass");
	}

	// Update is called once per frame
	void Update () {
		float right = moveSpeed * Input.GetAxis ("Horizontal");
		float up = moveSpeed * Input.GetAxis ("Vertical");

		right *= Time.deltaTime;
		up *= Time.deltaTime;

		transform.Translate (right, up, 0);

		Vector3 destination = getDestination ();
		if (destination.z != float.MaxValue) {
			Vector3 direction = destination - transform.position;
			direction.z = 0.0f;
			direction.Normalize ();

			float angle = Vector3.Angle (Vector3.down, direction);
			if (direction.x < 0.0f) {
				angle = -angle;
			}

			compass.transform.localEulerAngles = new Vector3(0.0f, 0.0f, angle);
		}
	}

	protected Vector3 getDestination(){
		Vector3 destination = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
		Team enemyTeam = (team == Team.RED) ? Team.BLUE : Team.RED;

		foreach (Transform b in baseFactory.transform) {
			BaseController baseC = b.GetComponent <BaseController> ();
			//If base is nearer than current destination...
			if (Vector3.Distance (transform.position, b.position) < Vector3.Distance (transform.position, destination)) {
				///...and it's not the last visited base or occupied by a red player...
				if (baseC != lastVisitedBase && baseC.occupants [(int)enemyTeam] == 0) {
					//...and that it's not 100% blue already
					if (baseC.team != team || baseC.changing) {
						destination = b.position;
					}
				}
			}
		}

		return destination;
	}

	public void setLastVisitedBase(BaseController baseC){
		lastVisitedBase = baseC;
	}
}
