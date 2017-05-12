using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoogleMapOnSprite : MonoBehaviour {

    public string url;
    public float lat = 50.935742f;
    public float lon = -1.396641f;
    // Building outlines is 17
    public int zoom = 16;
    // Google Map max size for free is 1280x1280 pixels
    public int size = 500;
    public int scale = 2;

    // Use this for initialization
    void Start() {
        //width = Camera.main.pixelWidth*2;
        //height = Camera.main.pixelHeight*2;

        //StartCoroutine(SetGoogleMapSprite(lat, lon, zoom, scale));
        //UpdateMap(lat, lon, zoom);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Submit")) {
            UpdateMap(lat, lon, zoom);
        }
    }

    IEnumerator SetGoogleMapSprite(float lat, float lon, int zoom, int scale) {
        url = "http://maps.googleapis.com/maps/api/staticmap?" +
            "center=" + lat + "," + lon + "&" +
            "zoom=" + zoom + "&" +
            "size=" + size + "x" + size + "&" +
            //"size=" + Screen.width + "x" + Screen.height + "&" +
            "scale=" + scale;
            //"&maptype=terrain";

        Debug.Log(url);

        WWW www = new WWW(url);
        // Wait for download to complete
        yield return www;

        float width = www.texture.width;
        float height = www.texture.height;

        //Debug.Log("screen size: " + width + "\t" + height);

        Sprite map = Sprite.Create(www.texture, new Rect(0.0f, 0.0f, width, height), new Vector2(0.5f, 0.5f));
        GetComponent<SpriteRenderer>().sprite = map;
        //transform.localScale = new Vector3(transform.localScale.x / 2, transform.localScale.y / 2, transform.localScale.z);
    }

    public void UpdateMap() {
        StartCoroutine(SetGoogleMapSprite(lat, lon, zoom, scale));
    }

    public void UpdateMap(float lat, float lon, int zoom) {
        StartCoroutine(SetGoogleMapSprite(lat, lon, zoom, scale));
    }

    public void UpdateMap(GoogleMapOnSprite gms, float latDiff, float lonDiff) {
        if (gms != null) {
            lat = gms.lat + latDiff;
            lon = gms.lon + lonDiff;
        }
        StartCoroutine(SetGoogleMapSprite(lat, lon, zoom, scale));
    }

}
