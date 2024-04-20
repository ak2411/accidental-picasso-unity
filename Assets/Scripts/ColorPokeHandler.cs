using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using AccidentalPicasso.UI.Palette;

public class ColorPokeHandler : MonoBehaviour
{
    [SerializeField]
    private AudioClip startPressClip;
    [SerializeField]
    private AudioClip endPressClip;
    private PokeInteractable pokeInteractable;
    private AudioSource audioSource;
    private PaletteBehavior paletteBehavior;
    private Color _color = Color.white;
    private Vector3 startPos;
    public Vector3 StartPosition
    {
        get
        {
            return startPos;
        }
        set
        {
            startPos = value;
        }
    }
    public Color Color
    {
        get
        {
            return _color;
        }
        set
        {
            _color = value;
        }
    }

    protected void Awake()
    {
        paletteBehavior = FindObjectOfType<PaletteBehavior>();
        audioSource = GetComponentInChildren<AudioSource>();
        pokeInteractable = GetComponentInChildren<PokeInteractable>();
        pokeInteractable.WhenStateChanged += HandleStateChange;
    }

    private void HandleStateChange(InteractableStateChangeArgs args)
    {
        switch (args.NewState)
        {
            case InteractableState.Select:
                audioSource.clip = startPressClip;
                audioSource.Play();
                paletteBehavior.UpdateColor(_color);
                return;
            case InteractableState.Normal:
                if(args.PreviousState == InteractableState.Select)
                {
                    audioSource.clip = endPressClip;
                    audioSource.Play();
                }
                if (transform.localPosition != startPos && args.PreviousState == InteractableState.Hover)
                {
                    StartCoroutine(LerpHoverEffect(startPos));
                }
                return;
            case InteractableState.Hover:
                if(args.PreviousState != InteractableState.Select && transform.localPosition == startPos)
                {
                    StartCoroutine(LerpHoverEffect(startPos + new Vector3(0.0f, 0.0f, 0.006f)));
                }
                return;
            default:
                if(transform.localPosition != startPos)
                {
                    StartCoroutine(LerpHoverEffect(startPos));
                }
                return;
        }
    }

    private IEnumerator LerpHoverEffect(Vector3 endPos)
    {
        float duration = 0.015f;
        float time = 0;
        while(time < duration)
        {
            transform.localPosition = Vector3.Lerp(startPos, endPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = endPos;
    }
}
