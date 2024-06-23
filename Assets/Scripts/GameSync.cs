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

    public List<int> indexes;
    public const int ROUNDS = 3;

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

    public void SetIndexes()
    {
        model.modelIdxs = GenerateRandomIdxs();
        SetIndexes(model.modelIdxs);
    }

    protected override void OnRealtimeModelReplaced(GameModel previousModel, GameModel currentModel)
    {
        if (previousModel != null)
        {
            previousModel.modelIdxsDidChange -= ModelIdxsDidChange;
        }
        if (currentModel != null)
        {
            if (currentModel.isFreshModel)
            {
                var modelIdxs = GenerateRandomIdxs();
                currentModel.modelIdxs = modelIdxs;
                SetIndexes(modelIdxs);
            }
            if (currentModel.modelIdxs.Length > 0)
            {
                SetIndexes(currentModel.modelIdxs);
            }
            currentModel.modelIdxsDidChange += ModelIdxsDidChange;
            Debug.Log("models" + model.modelIdxs);
        }
    }

    private string GenerateRandomIdxs()
    {
        HashSet<int> uniqueIndexes = new HashSet<int>();
        int numOfModels = AccidentalPicassoAppController.Instance.gameController.numOfModels;
        while (uniqueIndexes.Count < ROUNDS)
        {
            int idx = Random.Range(0, numOfModels);
            uniqueIndexes.Add(idx);
        }
        return string.Join("-", uniqueIndexes);
    }

    private void ModelIdxsDidChange(GameModel model, string modelIdxs)
    {
        SetIndexes(modelIdxs);
    }
    private void SetIndexes(string modelIdxs)
    {
        indexes = new List<int>();
        foreach (string idx in modelIdxs.Split("-"))
        {
            indexes.Add(int.Parse(idx));
        }
    }
}
