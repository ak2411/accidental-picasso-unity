using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using UnityEngine.Events;

public class ButtonBehavior : MonoBehaviour
{
    [SerializeField]
    private Renderer cubeRenderer;
    [SerializeField]
    private Color originalColor;
    [SerializeField]
    private Color hoverColor;
    [SerializeField]
    private UnityEvent WhenSelect;
    public bool pressButton;
    [SerializeField]
    private AudioClip pressSound;
    [SerializeField]
    public AudioSource audioSource;

    private IInteractableView InteractableView { get; set; }
    protected bool _started = false;

    protected virtual void Awake()
    {
        InteractableView = GetComponent<PokeInteractable>();
        if(GetComponent<AudioSource>() != null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    protected virtual void Start()
    {
        this.BeginStart(ref _started);
        this.AssertField(InteractableView, nameof(InteractableView));
        this.AssertField(cubeRenderer, nameof(Renderer));
        this.EndStart(ref _started);
    }

    private void Update()
    {
        if(pressButton)
        {
            audioSource.clip = pressSound;
            audioSource.Play();
            WhenSelect.Invoke();
            pressButton = false;
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
        switch (args.NewState)
        {
            case InteractableState.Hover:
                EnterHoverHandler();
                return;
            case InteractableState.Select:
                audioSource.clip = pressSound;
                audioSource.Play();
                WhenSelect.Invoke();
                return;
            default:
                ExitHoverHandler();
                return;
        }
    }

    private void EnterHoverHandler()
    {
        cubeRenderer.material.color = hoverColor;
    }

    private void ExitHoverHandler()
    {
        cubeRenderer.material.color = originalColor;
    }
}
