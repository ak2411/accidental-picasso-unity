using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayerController : MonoBehaviour
{
    public string userID;
    public PlatformType? platformSelected;
    public bool isPlaying = false;

    public void SetPlatformSelected(PlatformType platform)
    {
        platformSelected = platform;
        isPlaying = true;
        Debug.Log("platformSelected " + platformSelected);
        // update user's hands to that color
    }

    public void UnsetPlatformSelected(PlatformType platform)
    {
        platformSelected = null;
        isPlaying = false;
    }
}
