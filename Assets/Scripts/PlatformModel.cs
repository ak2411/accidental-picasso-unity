using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RealtimeModel]
public partial class PlatformModel
{
    [RealtimeProperty(1, true, true)]
    private string _userID;
}
