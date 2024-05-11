using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;

public class PlatformWidgetBehavior : MonoBehaviour
{
    private IInteractableView InteractableView { get; set; }
    [SerializeField]
    private Vector3 hoverLocalScale = new Vector3(0.1f, 0.1f, 0.1f);
    private Vector3 originalLocalScale;
    private Color widgetColor;
    protected bool _started = false;

    protected virtual void Awake()
    {
        InteractableView = GetComponent<InteractableGroupView>();
        originalLocalScale = transform.localScale;
        widgetColor = GetComponent<Renderer>().material.color;
    }

    protected virtual void Start()
    {
        this.BeginStart(ref _started);
        this.AssertField(InteractableView, nameof(InteractableView));
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
        switch(args.NewState)
        {
            case InteractableState.Hover:
                EnterHoverHandler();
                return;
            case InteractableState.Select:
                EnterHoverHandler();
                return;
            default:
                ExitHoverHandler();
                return;
        }
    }

    private void EnterHoverHandler()
    {
        GetComponent<Renderer>().material.color = new Color(widgetColor.r, widgetColor.g, widgetColor.b, 1.0f);
        StartCoroutine(LerpHoverEffect(transform.localScale, hoverLocalScale));
    }

    private void ExitHoverHandler()
    {
        GetComponent<Renderer>().material.color = new Color(widgetColor.r, widgetColor.g, widgetColor.b, widgetColor.a);
        StartCoroutine(LerpHoverEffect(transform.localScale, originalLocalScale));
    }

    private IEnumerator LerpHoverEffect(Vector3 startScale, Vector3 targetScale)
    {
        float duration = 0.03f;
        float time = 0;
        while(time < duration)
        {
            transform.localScale = Vector3.Lerp(startScale, targetScale, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localScale = targetScale;
    }
}
