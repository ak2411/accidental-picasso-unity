using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    private TextMeshProUGUI countdownText;
    public bool startGame = false;

    private void StartGame()
    {
        countdown = 60.0f; // 1 min
        currentRound += 1;
        gameState = GameState.Start;
    }

    private void EndRound()
    {
        gameState = GameState.End;
        if(currentRound < numOfRounds)
        {
            // Show next round
        }
    }

    protected void Update()
    {
        if(startGame)
        {
            StartGame();
            startGame = false;
        }
    }

    protected void FixedUpdate()
    {
        if(gameState == GameState.Start)
        {
            if(countdown > 0)
            {
                countdown -= Time.fixedDeltaTime;
                UpdateCountdown();
            }
            if (countdown <= 0)
            {
                EndRound();
            }
        }
    }

    private void UpdateCountdown()
    { 
        // reference: https://gamedevbeginner.com/how-to-make-countdown-timer-in-unity-minutes-seconds/
        float timeRemaining = countdown + 1;

        countdownText.text = string.Format("{0:00}", timeRemaining);
    }
}
