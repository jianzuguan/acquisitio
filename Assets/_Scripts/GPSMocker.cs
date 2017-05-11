using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPSMocker : MonoBehaviour {

    public GoogleMapOnSprite gms;

    public float step = 0.0000001f;
    public int horizontal;
    public int vertical;

    // Use this for initialization
    void Start () {
        gms = GetComponent<GoogleMapOnSprite>();
	}
	
	// Update is called once per frame
	void Update () {
        float inputHorizontal = Input.GetAxisRaw("Horizontal");
        float intputVertical = Input.GetAxisRaw("Vertical");

        if (inputHorizontal > 0) {
            horizontal++;
        } else if (inputHorizontal < 0) {
            horizontal--;
        }

        if (intputVertical > 0) {
            vertical++;
        } else if (intputVertical < 0) {
            vertical--;
        }

        Debug.Log("movements: " + vertical + "\t" + horizontal);

        // Update map when there are a parameters changed
        if (Mathf.Abs(horizontal) >= 10 || Mathf.Abs(vertical) >= 10 || Input.GetButtonDown("Zoom")) {
            Debug.Log("change made");

            gms.lat += step * vertical;
            gms.lon += step * horizontal;

            if (Input.GetButtonDown("Zoom")) {
                gms.zoom += Input.GetAxisRaw("Zoom") > 0 ? 1 : -1;
                gms.zoom = Mathf.Clamp(gms.zoom, 0, 20);
            }

            gms.UpdateMap(gms.lat, gms.lon, gms.zoom);

            vertical = 0;
            horizontal = 0;
        }
    }
}
