using UnityEngine;
using System.Collections;
using Normal.Realtime;
using UnityEngine.Events;

public class Countdown : RealtimeComponent<CountdownModel>
{
    public delegate void TimerHandler();
    public event TimerHandler OnTimerStarted;
    public float time
    {
        get
        {
            // Return 0 if we're not connected to the room yet.
            if (model == null) return 0.0f;

            // Make sure the countdown is running
            if (model.timerEnd == 0.0) return 0.0f;

            // Calculate how much time has passed
            return (float)(model.timerEnd - realtime.room.time);
        }
    }

    public void StartCountdown(float countdown)
    {
        model.timerEnd = realtime.room.time + countdown;
    }

    protected override void OnRealtimeModelReplaced(CountdownModel previousModel, CountdownModel currentModel)
    {
        if(previousModel != null)
        {
            currentModel.timerEndDidChange -= OnTimerEndDidChange;
        }

        if (currentModel != null) {
            if (model.timerEnd > 0)
            {
                OnTimerStarted();
            }
            currentModel.timerEndDidChange += OnTimerEndDidChange;
        }
    }

    private void OnTimerEndDidChange(CountdownModel model, double value)
    {
        if(value > 0)
        {
            OnTimerStarted();
        }
    }
}
