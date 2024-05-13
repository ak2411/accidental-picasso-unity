using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Normal.Realtime;

public enum GameState
{
    Idle, Start, Vote, End
}

public class GameController : MonoBehaviour
{
    private float countdown;
    private int numOfRounds;
    private int currentRound = 0;
    [SerializeField]
    private TMP_Text countdownText;
    [SerializeField]
    private Countdown realtimeCountdown;
    [SerializeField]
    private GameObject startButton;
    [SerializeField]
    private TMP_Text header;
    [SerializeField]
    private GameSync gameSync;
    [SerializeField]
    private GameObject stillLifeParent;
    [SerializeField]
    private GameObject palette;
    public bool startGame = false;
    private List<GameObject> activePlatforms = new List<GameObject>();
    [SerializeField]
    private List<GameObject> platforms = new List<GameObject>();
    [SerializeField]
    private List<GameObject> models = new List<GameObject>();
    private Dictionary<PlatformType, int> scores = new Dictionary<PlatformType, int>();

    public List<GameObject> localShapes = new List<GameObject>();
    public List<GameObject> remoteShapes = new List<GameObject>();

    private void Awake()
    {
        realtimeCountdown.OnTimerStarted += StartGame;
        numOfRounds = models.Count;
    }

    private void Start()
    {
        AccidentalPicassoAppController.Instance.OnReset += ResetStillLife;
        AccidentalPicassoAppController.Instance.OnReset += ResetShapes;
    }

    private void OnDestroy()
    {
        AccidentalPicassoAppController.Instance.OnReset -= ResetStillLife;
        AccidentalPicassoAppController.Instance.OnReset -= ResetShapes;
        realtimeCountdown.OnTimerStarted -= StartGame;
    }

    private void GetActivePlatforms()
    {
        if(activePlatforms.Count <= 0)
        {
            foreach (var platform in platforms)
            {
                if (platform.GetComponent<PlatformBehavior>().owner != null && platform.GetComponent<PlatformBehavior>().owner != "")
                {
                    platform.GetComponent<PlatformSync>().OnScoreUpdated += OnVoteUpdate;
                    activePlatforms.Add(platform);
                }
            }
        }
    }

    public void StartGame()
    {
        if (!startButton.activeSelf) return; //game has already started
        currentRound += 1;
        Debug.Log("num of rounds" + numOfRounds + currentRound);
        SetupModel(currentRound - 1);
        startButton.SetActive(false);
        countdownText.gameObject.SetActive(true);
        header.text = "Round " + currentRound;
        palette.GetComponent<PaletteUISwitcher>().ActivateGamePalette();
        GetActivePlatforms();

        if (realtimeCountdown.time <= 0)
        {
            gameSync.UpdateGameState(GameState.Start);
            realtimeCountdown.StartCountdown(60);
        }
    }

    public void ResetStillLife()
    {
        foreach (Transform child in stillLifeParent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void ResetShapes()
    {
        foreach (GameObject shape in remoteShapes)
        {
            Destroy(shape);
        }

        foreach (GameObject shape in localShapes)
        {
            Destroy(shape);
        }
    }

    private void SetupModel(int index)
    {
        var newModel = GameObject.Instantiate(models[index], stillLifeParent.transform);
        newModel.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
    }

    private void VotingRound()
    {
        if (gameSync.State != GameState.Vote)
        {
            gameSync.UpdateGameState(GameState.Vote);
        }
        palette.GetComponent<PaletteUISwitcher>().ActivateVotePalette();
        palette.GetComponent<PaletteUISwitcher>().votePalette.GetComponent<VoteBehavior>().CreatePanels(activePlatforms);
        header.text = "Time's up! Vote for each sculpture";
    }

    public void OnVoteUpdate(PlatformType type, int numOfVotes, int score)
    {
        Debug.Log("update vote log");
        if(numOfVotes == activePlatforms.Count)
        {
            scores[type] = score;
            CheckForWinner();
        }
    }

    private void CheckForWinner()
    {
        
        if(scores.Count == activePlatforms.Count)
        {
            var currMax = 0;
            PlatformType? winner = null;
            foreach (var entry in scores)
            {
                if(entry.Value > currMax)
                {
                    currMax = entry.Value;
                    winner = entry.Key;
                }
            }
            if (winner == AccidentalPicassoAppController.Instance.gamePlayerController.platformSelected.GetComponent<PlatformBehavior>().platformId)
            {
                header.text = "You WIN!";
            }
            else
            {
                switch (winner)
                {
                    case PlatformType.Red:
                        header.text = "Red won!";
                        return;
                    case PlatformType.Green:
                        header.text = "Green won!";
                        return;
                    case PlatformType.Blue:
                        header.text = "Blue won!";
                        return;
                    case PlatformType.Orange:
                        header.text = "Orange won!";
                        return;
                }
            }
            EndRound();
        }
    }

    private void EndRound()
    {
        if(gameSync.State != GameState.End)
        {
            gameSync.UpdateGameState(GameState.End);
        }
        palette.GetComponent<PaletteUISwitcher>().DisableBoth();
        if (currentRound < numOfRounds)
        {
            Debug.Log("Reset");
            // Show next round
            AccidentalPicassoAppController.Instance.Reset();
            startButton.SetActive(true);
            countdownText.gameObject.SetActive(false);
        } else
        {
            Debug.Log("else case reset");
        }
    }

    protected void Update()
    {
        if (startGame)
        {
            StartGame();
            startGame = false;
        }
    }

    protected void FixedUpdate()
    {
        if(gameSync.State == GameState.Start)
        {
            countdown = realtimeCountdown.time;
            if(countdown > 0)
            {
                countdownText.text = string.Format("{0:00}", countdown);
            }
            if (countdown <= 0)
            {
                VotingRound();
            }
        }
    }
}
