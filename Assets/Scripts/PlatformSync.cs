using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using UnityEngine.Events;

public class PlatformSync : RealtimeComponent<PlatformModel>
{
    private PlatformBehavior _platformBehavior;
    public delegate void ScoreHandler(PlatformType type, int numOfVotes, int score);
    public event ScoreHandler OnScoreUpdated;

    private void Awake()
    {
        _platformBehavior = GetComponent<PlatformBehavior>();
        AccidentalPicassoAppController.Instance.OnReset += ResetScore;
    }

    private void OnDestroy()
    {
        AccidentalPicassoAppController.Instance.OnReset -= ResetScore;
    }

    public void ResetScore()
    {
        if(model.score != "0-0")
        {
            model.score = "0-0";
        }
    }

    public void SendUpdateUserID(string userID)
    {
        model.userID = userID;
    }

    public void UpdateVote(int score)
    {
        string[] parts = model.score.Split('-');
        int numOfVotes = int.Parse(parts[0]) + 1;
        int currScore = int.Parse(parts[1]) + score;
        model.score = numOfVotes.ToString() + "-" + currScore.ToString();
    }

    protected override void OnRealtimeModelReplaced(PlatformModel previousModel, PlatformModel currentModel)
    {
        if(previousModel != null)
        {
            previousModel.userIDDidChange -= OnReceiveUserIDUpdate;
            previousModel.scoreDidChange -= OnScoreDidChange;

        }
        if (currentModel != null)
        {
            if (currentModel.isFreshModel)
            {
                currentModel.userID = "";
                currentModel.score = "0-0";
            }
            currentModel.userIDDidChange += OnReceiveUserIDUpdate;
            currentModel.scoreDidChange += OnScoreDidChange;
        }
        
    }

    private void OnReceiveUserIDUpdate(PlatformModel _, string value)
    {
        _platformBehavior.ConnectWithRemoteUser(value);
    }

    private void OnScoreDidChange(PlatformModel _, string value)
    {
        string[] parts = value.Split('-');
        int numOfVotes = int.Parse(parts[0]);
        int currScore = int.Parse(parts[1]);
        OnScoreUpdated(_platformBehavior.platformId, numOfVotes, currScore);
    }
}
