using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.Input;
using System.Linq;

public enum PlatformType
{
    Red, Green, Blue, Orange
};

public class PlatformBehavior : MonoBehaviour
{
    private string owner;
    [SerializeField]
    private PlatformType platformId;
    [SerializeField]
    private GameObject setOwnerButton;
    [SerializeField]
    private GameObject registeredUserIcon;
    public bool connect;
    private GamePlayerController gamePlayerController;
    protected void Awake()
    {
        // Using the platformId, update the appropriate material
        // Update button colors
        setOwnerButton = GetChildGameObject(transform, "Set Owner Button", true);
        registeredUserIcon = GetChildGameObject(transform, "Registered User", true);
        gamePlayerController = FindObjectOfType<GamePlayerController>();
    }

    protected void Update()
    {
        if(connect)
        {
            ConnectWithLocalUser();
            connect = false;
        }
    }

    public void ConnectWithLocalUser()
    {
        Debug.Log("called user set " + owner);
        if (owner != null || gamePlayerController.isPlaying) return;
        owner = gamePlayerController.userID;
        Debug.Log("set user " + owner);
        registeredUserIcon.SetActive(true);
        setOwnerButton.SetActive(false);
        gamePlayerController.SetPlatformSelected(gameObject);
    }

    public GameObject GetChildGameObject(Transform parent, string name, bool includeInactive)
    {
        Transform[] allChildren = parent.GetComponentsInChildren<Transform>(includeInactive);
        Transform foundChild = allChildren.FirstOrDefault(child => child.gameObject.name == name);
        return foundChild.gameObject;
    }

    private void disconnectFromOwner()
    {
        owner = null;
    }
}
