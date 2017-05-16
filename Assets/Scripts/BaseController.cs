using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour {

	public int x = 0;
	public int y = 0;
	public static int maxTime = 60;
	public static int minTime = 10;
	public static int secondsToTake = 3;
	private static float refreshRate = (float) secondsToTake / 100;

	GameObject colour;
	GameObject center;
	GameObject countdown;

	//Assume no occupants or team at start
	public int[] occupants = {0, 0}; //{red, blue}
	public Team team = Team.NONE;
	private int percentTaken = 0;
	public bool changing = false;
	private int timeRemaining;

	// Use this for initialization
	void Start () {
		colour = transform.Find ("Colour").gameObject;
		center = transform.Find ("Center").gameObject;
		colour.GetComponent <SpriteRenderer>().color = Color.grey;
	}
	
	// Update is called once per frame
	void Update () {
		if (!changing) {
			if (team == Team.NONE) {
				if (occupants [(int)Team.RED] > 0) {
					team = Team.RED;
					colour.GetComponent <SpriteRenderer> ().color = Color.red;
					InvokeRepeating ("IncreaseHold", refreshRate, refreshRate);
					changing = true;
				} else if (occupants [(int)Team.BLUE] > 0) {
					team = Team.BLUE;
					colour.GetComponent <SpriteRenderer> ().color = Color.blue;
					InvokeRepeating ("IncreaseHold", refreshRate, refreshRate);
					changing = true;
				}
			} else if (team == Team.RED && occupants [(int)Team.RED] == 0 && occupants [(int)Team.BLUE] > 0) {
				InvokeRepeating ("DecreaseHold", refreshRate, refreshRate);
				changing = true;
			} else if (team == Team.BLUE && occupants [(int)Team.BLUE] == 0 && occupants [(int)Team.RED] > 0) {
				InvokeRepeating ("DecreaseHold", refreshRate, refreshRate);
				changing = true;
			} else if (percentTaken < 100 && occupants [(int)team] > 0) {
				InvokeRepeating ("IncreaseHold", refreshRate, refreshRate);
				changing = true;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player" || other.tag == "Baddie") {
			PlayerController player = other.GetComponent <PlayerController> ();
			occupants [(int) player.team]++;
		}
	}
	

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player") {
			occupants [(int) Team.RED]--;
		}else if (other.tag == "Baddie") {
			BaddieController baddie = other.GetComponent <BaddieController> ();
			occupants [(int) Team.BLUE]--;
			if (team == Team.BLUE) {
				baddie.setLastVisitedBase (this);
			}
		}
	}

	private void IncreaseHold(){
		percentTaken++;

		//If has been fully taken, stop
		if (percentTaken == 100) { 
			center.GetComponent <SpriteRenderer>().color = team == Team.RED ? Color.red : Color.blue;
			CancelInvoke ("IncreaseHold");
			changing = false;
		//If no players in team left in area, stop
		} else if (occupants [(int)team] == 0) { 
			CancelInvoke ("IncreaseHold");
			changing = false;
		}

		//Show percent taken
		float scale = (float) percentTaken / 100;
		colour.transform.localScale = new Vector3 (scale, scale, 1);
	}

	private void DecreaseHold(){
		percentTaken--;

		//Attacking team = team not currently controlling this base
		Team attackingTeam = team == Team.BLUE ? Team.RED : Team.BLUE;

		//If 0% taken, another team can claim
		if (percentTaken == 0) {
			CancelInvoke ("DecreaseHold");
			team = Team.NONE;
			center.GetComponent <SpriteRenderer> ().color = Color.grey;
			changing = false;
		//If someone from the team returns to the base, they can increase their hold 
		} else if (occupants [(int)team] > 0) {
			CancelInvoke ("DecreaseHold");
			InvokeRepeating ("IncreaseHold", refreshRate, refreshRate);
			changing = true;
		//If attacking team leaves, they stop decreasing the current hold
		} else if (occupants [(int)attackingTeam] == 0) {
			CancelInvoke ("DecreaseHold");
			changing = false;
		}

		//Show percent taken
		float scale = (float) percentTaken / 100;
		colour.transform.localScale = new Vector3 (scale, scale, 1);
	}

	public int StartCountDown(){
		countdown = transform.Find ("Countdown").gameObject;

		timeRemaining = Random.Range (minTime, maxTime + 1);

		countdown.GetComponent <TextMesh>().text = timeRemaining.ToString ();
		InvokeRepeating ("CountDown", 1.0f, 1.0f);
		return timeRemaining;
	}

	private void CountDown(){
		timeRemaining--;
		if (timeRemaining == 0) {
			BaseFactory.grid [x, y] = null;
			ScoreSystem.IncrementScore (team);
			Destroy (gameObject);
		} else {
			countdown.GetComponent <TextMesh>().text = timeRemaining.ToString ();
		}
	}
}
