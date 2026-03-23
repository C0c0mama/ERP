using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;

public class MapController : MonoBehaviour
{
    [Header("Map Settings")]
    public float Latitude  = 15.1472f;   // Angeles City
    public float Longitude = 120.5892f;
    public int   ZoomLevel = 15;

    [Header("UI Reference")]
    public RawImage MapDisplay;

    [Header("Maptiler API Key")]
    public string APIKey = "YOUR_MAPTILER_API_KEY";

    // Tile size in pixels
    private int tileSize = 256;

    // How many tiles to load (3x3 grid)
    private int tilesPerRow = 3;

    private Texture2D mapTexture;

    void Start()
    {
        if (MapDisplay != null)
            MapDisplay.gameObject.SetActive(false);

        if (MapDisplay != null)
        MapDisplay.gameObject.SetActive(false);

        #if UNITY_EDITOR
            // Show colored placeholder in editor
            if (MapDisplay != null)
            {
                MapDisplay.gameObject.SetActive(true);
                MapDisplay.color = new Color(0.6f, 0.8f, 0.6f, 1f); // light green
            }
        #endif
    }

    public void ShowMap()
    {
        if (MapDisplay != null)
        {
            // Remove placeholder green color
            MapDisplay.color = Color.white;
            MapDisplay.gameObject.SetActive(true);
            StartCoroutine(LoadMapTiles());
        }
    }

    public void HideMap()
    {
        if (MapDisplay != null)
            MapDisplay.gameObject.SetActive(false);
    }

    IEnumerator LoadMapTiles()
    {
        int centerTileX = LonToTileX(Longitude, ZoomLevel);
        int centerTileY = LatToTileY(Latitude, ZoomLevel);

        int offset      = tilesPerRow / 2;
        int totalSize   = tileSize * tilesPerRow;

        // Create combined texture
        mapTexture = new Texture2D(totalSize, totalSize);

        for (int x = 0; x < tilesPerRow; x++)
        {
            for (int y = 0; y < tilesPerRow; y++)
            {
                int tileX = centerTileX + (x - offset);
                int tileY = centerTileY + (y - offset);

                string url = string.Format(
                    "https://api.maptiler.com/maps/streets-v2/{0}/{1}/{2}.png?key={3}",
                    ZoomLevel, tileX, tileY, APIKey
                );


                UnityWebRequest request =
                    UnityWebRequestTexture.GetTexture(url);
                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    Texture2D tile =
                        ((DownloadHandlerTexture)request.downloadHandler).texture;

                    // Place tile in correct position
                    int pixelX = x * tileSize;
                    int pixelY = (tilesPerRow - 1 - y) * tileSize;

                    Color[] pixels = tile.GetPixels();
                    mapTexture.SetPixels(pixelX, pixelY, tileSize, tileSize, pixels);
                }
                else
                {
                    Debug.LogError("Tile load failed: " + request.error);
                }
            }
        }

        mapTexture.Apply();
        MapDisplay.texture = mapTexture;
        Debug.Log("Map loaded successfully!");
    }

    // ==============================
    // Zoom Controls
    // ==============================
    public void ZoomIn()
    {
        if (ZoomLevel < 19)
        {
            ZoomLevel++;
            StartCoroutine(LoadMapTiles());
        }
    }

    public void ZoomOut()
    {
        if (ZoomLevel > 1)
        {
            ZoomLevel--;
            StartCoroutine(LoadMapTiles());
        }
    }

    // ==============================
    // GPS Location
    // ==============================
    public void GoToMyLocation()
    {
        StartCoroutine(GetGPSAndLoad());
    }

    IEnumerator GetGPSAndLoad()
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

        Latitude  = Input.location.lastData.latitude;
        Longitude = Input.location.lastData.longitude;

        StartCoroutine(LoadMapTiles());
        Input.location.Stop();
    }

    // ==============================
    // Tile Coordinate Conversion
    // ==============================
    int LonToTileX(float lon, int zoom)
    {
        return (int)((lon + 180.0f) / 360.0f * (1 << zoom));
    }

    int LatToTileY(float lat, int zoom)
    {
        float latRad = lat * Mathf.Deg2Rad;
        return (int)(
            (1.0f - Mathf.Log(
                Mathf.Tan(latRad) + 1.0f / Mathf.Cos(latRad)
            ) / Mathf.PI) / 2.0f * (1 << zoom)
        );
    }
}