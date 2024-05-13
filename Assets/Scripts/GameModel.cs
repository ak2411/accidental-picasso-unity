using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RealtimeModel]
public partial class GameModel
{
    [RealtimeProperty(1, true, true)]
    private GameState _state;
    [RealtimeProperty(2, true, true)]
    private int _numOfPlayers;
    [RealtimeProperty(3, true, true)]
    private int _currRound;
}
