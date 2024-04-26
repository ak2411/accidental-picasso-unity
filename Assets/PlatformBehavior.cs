using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.Input;

public enum PlatformType
{
    Red, Green, Blue, Orange
};

public class PlatformBehavior : MonoBehaviour
{
    private string owner;
    [SerializeField]
    private PlatformType platformId;
    private GameObject ownerSymbol;
    protected void Awake()
    {
        // Using the platformId, update the appropriate material
        // Update button colors
        
    }

    private void connectWithLocalUser(string requestUser)
    {
        if (owner != null) return;
        owner = requestUser;
        // make outline the appropriate color
        // update user's hands
    }

    private void disconnectFromOwner()
    {
        owner = null;
    }
}
