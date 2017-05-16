using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour {
    public float countdown = 300f;
    public GameObject countdownUI;
    public Text countdownText;

    public GameObject endScreen;

    [Header("Should not change")]
    public float latOrigin = 50.93574f;
    public float lonOrigin = -1.396641f;

    public float latPerUnit = 0.000339f;
    public float lonPerUnit = 0.000536f;

    // Gaps of background tiles
    public float latDiff = 0.00339f;
    public float lonDiff = 0.00536f;

    // Use this for initialization
    void Start() {
        if (countdownUI == null) {
            countdownUI = GameObject.Find("/CanvasGameState/Countdown");
        }
        countdownUI.SetActive(true);

        if (countdownText == null) {
            countdownText = GameObject.Find("/CanvasGameState/Countdown/CountdownText").GetComponent<Text>();
        }

        if (endScreen == null) {
            endScreen = GameObject.Find("/CanvasGameState/EndScreen");
        }
        endScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        // Countdown
        if (countdown >= 0) {
            countdown -= Time.deltaTime;
            countdownText.text = countdown.ToString("F2");
        } else {
            countdownText.text = "0";
            countdownUI.SetActive(false);
            DisplayEndScreen();
        }
    }

    void DisplayEndScreen() {
        endScreen.SetActive(true);
    }

    public void InitGameState() {
        countdown = 300f;
        // Initialise score

        // Setup UI
        endScreen.SetActive(false);
        countdownUI.SetActive(true);
        //scoreUI.SetActive(true);

    }
}
