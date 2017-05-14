using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGPSController : MonoBehaviour {
    public GameObject errorPanel;
    public Text errorText;

    public GameObject initPanel;

    public GameState gs;

    private int maxWait = 20;

    // Use this for initialization
    void Start() {
        gs = GameObject.Find("GameState").GetComponent<GameState>();

    }

    // Update is called once per frame
    void Update() {
#if UNITY_ANDROID
        GetGPS();
#endif
    }

    public void GetGPS() {
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser) {
            errorText.text = "Please turn on your GPS tracking";
            errorPanel.SetActive(true);
            return;
        }

        // Start service before querying location
        if (Input.location.status == LocationServiceStatus.Stopped) {
            Input.location.Start();
        }

        // Wait until service initializes
        if (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0) {
            maxWait--;
            initPanel.SetActive(true);
            return;
        }
        initPanel.SetActive(false);

        // Service didn't initialize in 20 seconds
        if (maxWait < 1) {
            errorText.text = "Timed out";
            errorPanel.SetActive(true);
            return;
        }
        errorPanel.SetActive(false);

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed) {
            errorText.text = "Unable to determine device location";
            errorPanel.SetActive(true);
        } else {
            // Access granted and location value could be retrieved
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);

            float latDiff = Input.location.lastData.latitude - gs.latOrigin;
            float lonDiff = Input.location.lastData.longitude - gs.lonOrigin;

            transform.position = new Vector3(lonDiff / gs.lonPerUnit, latDiff / gs.latPerUnit, 0);
        }
    }
}
