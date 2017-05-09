using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoogleMapOnSprite : MonoBehaviour {

    public float lat = 50.935742f;
    public float lon = -1.396641f;
    // Building outlines is 17
    public int zoom = 16;
    // Google Map max size for free is 1280x1280 pixels
    public int size = 1280;
    public int scale = 2;

    // Use this for initialization
    void Start() {
        //width = Camera.main.pixelWidth*2;
        //height = Camera.main.pixelHeight*2;

        StartCoroutine(SetGoogleMapSprite(lat, lon, zoom, size, scale));
    }

    // Update is called once per frame
    void Update() {

    }

    IEnumerator SetGoogleMapSprite(float lat, float lon, int zoom, int size, int scale) {
        string url = "http://maps.googleapis.com/maps/api/staticmap?" +
            "center=" + lat + "," + lon + "&" +
            "zoom=" + zoom + "&" +
            "size=" + size + "x" + size + "&" +
            "scale=" + scale;

        Debug.Log(url);

        WWW www = new WWW(url);
        // Wait for download to complete
        yield return www;

        float width = www.texture.width;
        float height = www.texture.height;

        Debug.Log(width + "\t" + height);

        Sprite map = Sprite.Create(www.texture, new Rect(0.0f, 0.0f, width, height), new Vector2(0.5f, 0.5f));
        GetComponent<SpriteRenderer>().sprite = map;
    }
}
