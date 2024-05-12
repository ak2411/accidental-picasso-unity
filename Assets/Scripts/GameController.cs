using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Normal.Realtime;

public enum GameState
{
    Idle, Start, End
}

public class GameController : MonoBehaviour
{
    private float countdown;
    private int numOfRounds;
    private int currentRound = 0;
    private GameState gameState = GameState.Idle;
    [SerializeField]
    private TMP_Text countdownText;
    [SerializeField]
    private Countdown realtimeCountdown;
    [SerializeField]
    private GameObject startButton;
    [SerializeField]
    private TMP_Text header;
    public bool startGame = false;

    private void Awake()
    {
        realtimeCountdown.OnTimerStarted += StartGame;
    }

    private void OnDestroy()
    {
        realtimeCountdown.OnTimerStarted -= StartGame;
    }

    public void StartGame()
    {
        currentRound += 1;
        startButton.SetActive(false);
        countdownText.gameObject.SetActive(true);
        gameState = GameState.Start;
        if(realtimeCountdown.time <= 0)
        {
            realtimeCountdown.StartCountdown(60.0f);
        }
    }

    private void EndRound()
    {
        gameState = GameState.End;
        header.text = "Time's up! Vote for each sculpture";
        if(currentRound < numOfRounds)
        {
            // Show next round
            startButton.SetActive(true);
            countdownText.gameObject.SetActive(false);
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
        if(gameState == GameState.Start)
        {
            countdown = realtimeCountdown.time;
            if(countdown > 0)
            {
                countdownText.text = string.Format("{0:00}", countdown);
            }
            if (countdown <= 0)
            {
                EndRound();
            }
        }
    }
}
