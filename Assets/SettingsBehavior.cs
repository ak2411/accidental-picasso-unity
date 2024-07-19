using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject _menuParent;
    /// <summary>
    /// Show/hide the menu
    /// </summary>
    public void ToggleMenu(bool visible)
    {
        if (!visible && _menuParent.activeSelf)
        {
            _menuParent.SetActive(false);
        }
        else if (visible && !_menuParent.activeSelf)
        {
            _menuParent.SetActive(true);
        }
    }
}
