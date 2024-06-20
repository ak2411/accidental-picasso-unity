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
    [SerializeField]
    private AudioClip timesUpClip;
    [SerializeField]
    private AudioClip startClip;
    public bool startGame = false;
    private List<GameObject> activePlatforms = new List<GameObject>();
    [SerializeField]
    private List<GameObject> platforms = new List<GameObject>();
    [SerializeField]
    private List<GameObject> models = new List<GameObject>();
    private Dictionary<PlatformType, int> scores = new Dictionary<PlatformType, int>();
    private AudioSource audioSource;

    public List<GameObject> localShapes = new List<GameObject>();
    public List<GameObject> realtimeShapes = new List<GameObject>();
    public List<GameObject> remoteShapes = new List<GameObject>();

    private void Awake()
    {
        realtimeCountdown.OnTimerStarted += StartGame;
        numOfRounds = models.Count;
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        AccidentalPicassoAppController.Instance.OnResetAtEnd += ResetStillLife;
        AccidentalPicassoAppController.Instance.OnResetAtEnd += ResetShapes;
        AccidentalPicassoAppController.Instance.OnResetAtEnd += ResetScores;
    }

    private void OnDestroy()
    {
        AccidentalPicassoAppController.Instance.OnResetAtEnd -= ResetStillLife;
        AccidentalPicassoAppController.Instance.OnResetAtEnd -= ResetShapes;
        AccidentalPicassoAppController.Instance.OnResetAtEnd -= ResetScores;
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
        Debug.Log("start game called");
        if (!startButton.activeSelf) return; //game has already started
        currentRound += 1;
        Debug.Log("num of rounds" + numOfRounds + currentRound);
        SetupModel(currentRound - 1);
        AccidentalPicassoAppController.Instance.ResetAtStart();
        startButton.SetActive(false);
        countdownText.gameObject.SetActive(true);
        header.text = "Round " + currentRound;
        palette.GetComponent<PaletteUISwitcher>().ActivateGamePalette();
        GetActivePlatforms();

        audioSource.clip = startClip;
        audioSource.Play();

        if (realtimeCountdown.time <= 0)
        {
            gameSync.UpdateGameState(GameState.Start);
            realtimeCountdown.StartCountdown(120);
        }
    }

    public void ResetStillLife()
    {
        foreach (Transform child in stillLifeParent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void ResetScores()
    {
        scores = new Dictionary<PlatformType, int>();
    }

    private void ResetShapes()
    {
        foreach (GameObject shape in localShapes)
        {
            Destroy(shape);
        }
        localShapes = new List<GameObject>();

        foreach (GameObject shape in remoteShapes)
        {
            Destroy(shape);
            Debug.Log("destroyed shape" + shape.name);
        }
        remoteShapes = new List<GameObject>();

        foreach (GameObject shape in realtimeShapes)
        {
            Realtime.Destroy(shape);
        }
        realtimeShapes = new List<GameObject>();
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
        audioSource.clip = timesUpClip;
        audioSource.Play();
    }

    public void OnVoteUpdate(PlatformType type, int numOfVotes, int score)
    {
        Debug.Log("update vote log "+type+numOfVotes+score);
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
                Debug.Log("setting header");
                switch (winner)
                {
                    case PlatformType.Red:
                        header.text = "Red won!";
                        break;
                    case PlatformType.Green:
                        header.text = "Green won!";
                        break;
                    case PlatformType.Blue:
                        header.text = "Blue won!";
                        break;
                    case PlatformType.Orange:
                        header.text = "Orange won!";
                        break;
                }
            }
            Debug.Log("calling endround");
            EndRound();
        }
    }

    private void EndRound()
    {
        Debug.Log("Ending round");
        if(gameSync.State != GameState.End)
        {
            gameSync.UpdateGameState(GameState.End);
        }
        palette.GetComponent<PaletteUISwitcher>().DisableBoth();
        if (currentRound < numOfRounds)
        {
            Debug.Log("Reset");
            // Show next round
            startButton.SetActive(true);
            countdownText.gameObject.SetActive(false);
        } else
        {
            Debug.Log("else case reset");
        }
        AccidentalPicassoAppController.Instance.ResetAtEnd();
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
