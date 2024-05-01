using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayerController : MonoBehaviour
{
    public string userID;
    public GameObject platformSelected;
    public bool isPlaying = false;

    public void SetPlatformSelected(GameObject platform)
    {
        if (platformSelected != null) return;
        platformSelected = platform;
        TogglePalette(true);
        isPlaying = true;
        Debug.Log("platformSelected " + platformSelected);
        // update user's hands to that color
    }

    public void UnsetPlatformSelected()
    {
        platformSelected = null;
        TogglePalette(false);
        isPlaying = false;
    }

    private void TogglePalette(bool active)
    {
        GameObject palette = GameObject.Find("Palette");
        Debug.Log("===palette===" + palette);
        foreach (Transform child in palette.transform)
        {
            child.gameObject.SetActive(active); // or false
        }
    }
}
