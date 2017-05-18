using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour {
    public static bool isRunning;

    public float countdown = 300f;
    public GameObject countdownUI;
    public Text countdownText;

    public GameObject scoresUI;

    public GameObject endScreen;
    public Text endState;

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
        if (isRunning && countdownText == null) {
            countdownText = GameObject.Find("/CanvasGameState/Countdown/CountdownText").GetComponent<Text>();
        }
        if (isRunning && countdownUI == null) {
            countdownUI = GameObject.Find("/CanvasGameState/Countdown");
        }
        if (countdownUI != null) {
            countdownUI.SetActive(true);
        }

        if (scoresUI == null) {
            scoresUI = GameObject.Find("/CanvasGameState/Scores");
        }

        if (isRunning && endState == null) {
            endState = GameObject.Find("/CanvasGameState/EndScreen/Text").GetComponent<Text>();
        }
        if (isRunning && endScreen == null) {
            endScreen = GameObject.Find("/CanvasGameState/EndScreen");
        }
        if (endScreen != null) {
            endScreen.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update() {
        // Countdown
        if (isRunning) {
            if (countdown >= 0) {
                countdown -= Time.deltaTime;
                countdownText.text = countdown.ToString("F2");
            } else {
                countdownText.text = "0";
                countdownUI.SetActive(false);
                DisplayEndScreen();
            }
        }
    }

    void DisplayEndScreen() {
        GameObject.Find("Base Factory").GetComponent<BaseFactory>().ClearBases();

        //scoresUI.SetActive(false);

        // Show wining or lose
        Team playersTeam = GameObject.Find("Player").GetComponent<DirectionController>().team;
        Team winningTeam = GetComponent<ScoreSystem>().GetWiningTeam();
        if (winningTeam == Team.NONE) {
            // Draw
            endState.text = "Draw";
        } else if (winningTeam == playersTeam) {
            // Win
            endState.text = "Win";
        } else {
            // Lost
            endState.text = "Lose";
        }

        endScreen.SetActive(true);
    }

    // Restart game
    public void InitGameState() {
        GameObject.Find("Base Factory").GetComponent<BaseFactory>().Restart();
        GetComponent<ScoreSystem>().ResetScore();
        countdown = 300f;

        // Setup UI
        endScreen.SetActive(false);
        countdownUI.SetActive(true);
        scoresUI.SetActive(true);

    }
}
