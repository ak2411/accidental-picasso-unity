using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RealtimeModel(createMetaModel: true)]
public partial class PlatformModel
{
    [RealtimeProperty(1, true, true)]
    private string _userID;
}
