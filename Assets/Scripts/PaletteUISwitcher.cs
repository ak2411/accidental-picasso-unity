using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaletteUISwitcher : MonoBehaviour
{
    [SerializeField]
    public GameObject gamePalette;
    [SerializeField]
    public GameObject votePalette;

    public void ActivateGamePalette()
    {
        gamePalette.SetActive(true);
        votePalette.SetActive(false);
    }

    public void ActivateVotePalette()
    {
        gamePalette.SetActive(false);
        votePalette.SetActive(true);
    }

    public void DisableBoth()
    {
        gamePalette.SetActive(false);
        votePalette.SetActive(false);
    }
}
