using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    private WebViewObject WebViewObject;

    [Header("Map Settings")]
    public float Latitude = 14.5995f;
    public float Longitude = 120.9842f;

    private bool isVisible = false;

    void Start()
    {
        StartCoroutine(LoadMap());
    }

    System.Collections.IEnumerator LoadMap()
    {
        WebViewObject = new GameObject("WebViewObject")
            .AddComponent<WebViewObject>();

        WebViewObject.Init(
            cb: (msg) => { Debug.Log("msg: " + msg); },
            err: (msg) => { Debug.LogError("err: " + msg); },
            started: (msg) => { Debug.Log("started: " + msg); },
            ld: (msg) => { Debug.Log("Map Loaded!"); },
            enableWKWebView: true
        );

        // Full screen
        WebViewObject.SetMargins(0, 0, 0, 0);

        // Hide at start
        WebViewObject.SetVisibility(false);

        LoadOpenStreetMap(Latitude, Longitude);

        yield return null;
    }

    void LoadOpenStreetMap(float lat, float lng)
    {
        string html = string.Format(@"
<!DOCTYPE html>
<html>
<head>
    <meta name='viewport' content='width=device-width, initial-scale=1.0, maximum-scale=1.0'>
    <link rel='stylesheet' href='https://unpkg.com/leaflet@1.9.4/dist/leaflet.css'/>
    <script src='https://unpkg.com/leaflet@1.9.4/dist/leaflet.js'></script>
    <style>
        * {{ margin: 0; padding: 0; box-sizing: border-box; }}
        html, body {{ width: 100%; height: 100%; overflow: hidden; }}
        #map {{ width: 100%; height: 100%; }}
    </style>
</head>
<body>
    <div id='map'></div>
    <script>
        var map = L.map('map', {{
            zoomControl: false
        }}).setView([{0}, {1}], 15);

        L.tileLayer('https://{{s}}.tile.openstreetmap.org/{{z}}/{{x}}/{{y}}.png', {{
            attribution: '© OpenStreetMap',
            maxZoom: 19
        }}).addTo(map);

        var marker = L.marker([{0}, {1}]).addTo(map)
            .bindPopup('You are here!')
            .openPopup();
    </script>
</body>
</html>", lat, lng);

        string path = System.IO.Path.Combine(
            Application.temporaryCachePath, "map.html"
        );

        System.IO.File.WriteAllText(path, html);

#if UNITY_ANDROID
        WebViewObject.LoadURL("file://" + path);
#else
            WebViewObject.LoadURL("file://" + path);
#endif
    }

    public void ShowMap()
    {
        isVisible = true;
        if (WebViewObject != null)
            WebViewObject.SetVisibility(true);
    }

    public void HideMap()
    {
        isVisible = false;
        if (WebViewObject != null)
            WebViewObject.SetVisibility(false);
    }

    public void GoToMyLocation()
    {
        StartCoroutine(GetGPSAndLoad());
    }

    System.Collections.IEnumerator GetGPSAndLoad()
    {
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("GPS not enabled!");
            yield break;
        }

        Input.location.Start();

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing
               && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.LogError("GPS Failed!");
            yield break;
        }

        float lat = Input.location.lastData.latitude;
        float lng = Input.location.lastData.longitude;

        LoadOpenStreetMap(lat, lng);
        Input.location.Stop();
    }
}