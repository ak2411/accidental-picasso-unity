using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class PlatformSync : RealtimeComponent<PlatformModel>
{
    private PlatformBehavior _platformBehavior;

    private void Awake()
    {
        _platformBehavior = GetComponent<PlatformBehavior>();
    }

    public void SendUpdateUserID(string userID)
    {
        Debug.Log("updated user id");
        model.userID = userID;
    }

    protected override void OnRealtimeModelReplaced(PlatformModel previousModel, PlatformModel currentModel)
    {
        if(previousModel != null)
        {
            previousModel.userIDDidChange -= OnReceiveUserIDUpdate;
        }
        if(currentModel != null)
        {
            if (currentModel.isFreshModel)
            {
                currentModel.userID = "";
            }
            currentModel.userIDDidChange += OnReceiveUserIDUpdate;
        }
        
    }

    private void OnReceiveUserIDUpdate(PlatformModel model, string value)
    {
        Debug.Log("called");
        _platformBehavior.ConnectWithRemoteUser(value);
    }
}
