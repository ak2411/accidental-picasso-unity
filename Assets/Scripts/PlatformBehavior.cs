using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.Input;
using System.Linq;
using Normal.Realtime;
using TMPro;
public enum PlatformType
{
    Red, Green, Blue, Orange
};

public class PlatformBehavior : MonoBehaviour
{
    public string owner;
    [SerializeField]
    public PlatformType platformId;
    [SerializeField]
    private GameObject setOwnerButton;
    [SerializeField]
    private GameObject registeredUserIcon;
    [SerializeField]
    private PlatformSync _platformSync;
    [SerializeField]
    private RealtimeView _realtimeView;

    public bool connect;
    private GamePlayerController gamePlayerController;
    protected void Awake()
    {
        // Using the platformId, update the appropriate material
        // Update button colors
        if(!setOwnerButton)
        {
            setOwnerButton = GetChildGameObject(transform, "Set Owner Button", true);
        }
        if(!registeredUserIcon)
        {
            registeredUserIcon = GetChildGameObject(transform, "Registered User", true);
        }
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
        if (owner.Length != 0) return;
        _realtimeView.RequestOwnership();
        _platformSync.SendUpdateUserID(gamePlayerController.userID);
        owner = gamePlayerController.userID;
        UpdateDisplay(true);
        gamePlayerController.SetPlatformSelected(gameObject);
    }

    public void ConnectWithRemoteUser(string userID)
    {
        if (owner.Length != 0) return;
        owner = userID;
        UpdateDisplay(true);
    }

    private void UpdateDisplay(bool hasUser)
    {
        registeredUserIcon.SetActive(hasUser);
        if(hasUser)
        {
            registeredUserIcon.GetComponentInChildren<TMP_Text>().text = owner;
        }
        setOwnerButton.SetActive(!hasUser);
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
