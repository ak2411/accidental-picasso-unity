using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoteBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject votePanelPrefab;
    [SerializeField]
    private GameObject _menuParent;
    [SerializeField]
    private Vector3 startPos = new Vector3(0.0107f, 0.0355f, 0.011f);
    [SerializeField]
    private Vector3 offset = new Vector3(-0.1f, 0, 0);
    [SerializeField]
    private Material redMaterial;
    [SerializeField]
    private Material greenMaterial;
    [SerializeField]
    private Material blueMaterial;
    [SerializeField]
    private Material orangeMaterial;

    private List<GameObject> panels = new List<GameObject>();
    

    public void CreatePanels(List<GameObject> platforms)
    {
        if (panels.Count > 0) return;
        var panelPosition = startPos;
        foreach(var platform in platforms)
        {
            var panel = GameObject.Instantiate(votePanelPrefab, _menuParent.transform);
            panel.transform.localPosition = panelPosition;
            var platformType = platform.GetComponent<PlatformBehavior>().platformId;
            panel.GetComponent<VotePanelBehavior>().back.GetComponent<MeshRenderer>().material = getMaterial(platformType);
            panel.GetComponent<VotePanelBehavior>().platformRef = platform;
            panelPosition += offset;
        }
    }

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

    private Material getMaterial(PlatformType type)
    {
        switch(type)
        {
            case PlatformType.Red:
                return redMaterial;
            case PlatformType.Green:
                return greenMaterial;
            case PlatformType.Blue:
                return blueMaterial;
            case PlatformType.Orange:
                return orangeMaterial;
        }
        return null;
    }
}
