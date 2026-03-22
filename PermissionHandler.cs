using System.Collections;
using UnityEngine;
using UnityEngine.Android;

public class PermissionHandler : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(RequestPermissions());
    }

    IEnumerator RequestPermissions()
    {
        // Wait a frame before requesting
        yield return null;

        // Request Location Permission
        if (!Permission.HasUserAuthorizedPermission(
            Permission.FineLocation))
        {
            Permission.RequestUserPermission(
                Permission.FineLocation);
        }

        yield return new WaitForSeconds(0.5f);

        // Request Notification Permission (Android 13+)
        #if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(
            "android.permission.POST_NOTIFICATIONS"))
        {
            Permission.RequestUserPermission(
                "android.permission.POST_NOTIFICATIONS");
        }
        #endif
    }
}