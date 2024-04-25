using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class ButtonBehavior : MonoBehaviour
{
    [SerializeField]
    private Renderer cubeRenderer;
    [SerializeField]
    private Color originalColor;
    [SerializeField]
    private Color hoverColor;

    private IInteractableView InteractableView { get; set; }
    protected bool _started = false;

    protected virtual void Awake()
    {
        InteractableView = GetComponent<PokeInteractable>();
    }

    protected virtual void Start()
    {
        this.BeginStart(ref _started);
        this.AssertField(InteractableView, nameof(InteractableView));
        this.AssertField(cubeRenderer, nameof(Renderer));
        this.EndStart(ref _started);
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
        switch (args.NewState)
        {
            case InteractableState.Hover:
                EnterHoverHandler();
                return;
            case InteractableState.Select:
                Debug.Log("SELECTED");
                return;
            default:
                ExitHoverHandler();
                return;
        }
    }

    private void EnterHoverHandler()
    {
        Debug.Log("HOVER ENTER");
        cubeRenderer.material.color = hoverColor;
    }

    private void ExitHoverHandler()
    {
        Debug.Log("HOVER EXIT");
        cubeRenderer.material.color = originalColor;
    }
}
