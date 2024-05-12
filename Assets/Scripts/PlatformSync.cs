using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class PlatformSync : RealtimeComponent<PlatformModel>
{
    public void UpdateUserID(string userID)
    {
        model.owner = userID;
    }
}
