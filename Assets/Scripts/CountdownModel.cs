using UnityEngine;
using System.Collections;
using Normal.Realtime;
using Normal.Realtime.Serialization;

[RealtimeModel]
public partial class CountdownModel
{
    [RealtimeProperty(1, true, true)]
    private double _timerEnd;
}
