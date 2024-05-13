using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayerController : MonoBehaviour
{
    public string userID;
    public GameObject platformSelected;
    public bool isPlaying = false;
    [SerializeField]
    private GameObject palette;

    public void SetPlatformSelected(GameObject platform)
    {
        if (platformSelected != null) return;
        platformSelected = platform;
        isPlaying = true;
        Debug.Log("platformSelected " + platformSelected);
        // update user's hands to that color
    }

    public void UnsetPlatformSelected()
    {
        platformSelected = null;
        isPlaying = false;
    }

    private void TogglePalette(bool active)
    {
        foreach (Transform child in palette.transform)
        {
            child.gameObject.SetActive(active); // or false
        }
    }
}
