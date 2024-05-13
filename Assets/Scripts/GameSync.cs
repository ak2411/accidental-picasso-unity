using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class GameSync : RealtimeComponent<GameModel>
{
    public GameState State
    {
        get
        {
            if (model == null) return GameState.Idle;
            return model.state;
        }
    }

    public void UpdateGameState(GameState newState)
    {
        model.state = newState;
    }

    public void UpdateCurrentRound(int newRound)
    {
        model.currRound = newRound;
    }

    public void AddPlayer()
    {
        model.numOfPlayers += 1;
    }


}
