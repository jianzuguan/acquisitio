using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public Team team;
	public float moveSpeed = 100.0f;

	private GameObject baseFactory;
	private BaseController lastVisitedBase;

	// Use this for initialization
	void Start () {
		baseFactory = GameObject.Find ("Base Factory");
	}

	// Update is called once per frame
	void Update () {
		float right = moveSpeed * Input.GetAxis ("Horizontal");
		float up = moveSpeed * Input.GetAxis ("Vertical");

		right *= Time.deltaTime;
		up *= Time.deltaTime;

		transform.Translate (right, up, 0);

		//TODO: show arrow to nearest takeable base
	}

	protected Vector3 getDestination(){
		Vector3 destination = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);

		foreach (Transform b in baseFactory.transform) {
			BaseController baseC = b.GetComponent <BaseController> ();
			//If base is nearer than current destination...
			if (Vector3.Distance (transform.position, b.position) < Vector3.Distance (transform.position, destination)) {
				///...and it's not the last visited base or occupied by a red player...
				if (baseC != lastVisitedBase && baseC.occupants [(int)Team.RED] == 0) {
					//...and that it's not 100% blue already
					if (baseC.team != Team.BLUE || baseC.changing) {
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
