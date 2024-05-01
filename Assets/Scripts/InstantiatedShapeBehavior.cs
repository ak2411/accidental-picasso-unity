using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using Normal.Realtime;

public class InstantiatedShapeBehavior : MonoBehaviour
{
    // Handles detection whenever it collides with a gameobject to see if it is a platform. if it is stil within the boundaries of the platform once the user stops selecting, update its parent to the patform parent
    private IInteractableView InteractableView { get; set; }
    private GamePlayerController gamePlayerController;
    private GameObject realtimeCounterpart;
    private Realtime _realtime;
    protected bool _started = false;
    protected virtual void Awake()
    {
        InteractableView = GetComponentInChildren<InteractableGroupView>();
        gamePlayerController = FindObjectOfType<GamePlayerController>();
        _realtime = FindObjectOfType<Realtime>();
    }

    protected virtual void Start()
    {
        this.BeginStart(ref _started);
        this.AssertField(InteractableView, nameof(InteractableView));
        this.EndStart(ref _started);
    }

    protected void Update()
    {
        if (!_realtime.connected)
            return;
        if(!realtimeCounterpart)
        {
            realtimeCounterpart = Realtime.Instantiate("RealtimeShape");
            realtimeCounterpart.GetComponent<SyncRealtimeToShape>().shape = gameObject;
        }
    }

    protected virtual void OnEnable()
    {
        if (_started)
        {
            InteractableView.WhenStateChanged += HandleStateChange;
        }
    }

    protected virtual void OnDisable()
    {
        if (_started)
        {
            InteractableView.WhenStateChanged -= HandleStateChange;
        }
    }

    private void HandleStateChange(InteractableStateChangeArgs args)
    {
        switch(args.NewState)
        {
            case InteractableState.Select:
                OnSelect();
                return;
            default:
                if(args.PreviousState == InteractableState.Select)
                {
                    OnUnselect();
                }
                return;
        }
    }
    private void OnSelect()
    {
        if (!gamePlayerController.isPlaying) return;
        // check collision
        // if collide, is it the platform the user is assigned to?
        // if not, return
        // if it is, then check if shape is within bounds
        // if not, remove shape from that platform if it is part of it
        // if it is, then synchronize shape
    }

    private void OnUnselect()
    {

    }
}