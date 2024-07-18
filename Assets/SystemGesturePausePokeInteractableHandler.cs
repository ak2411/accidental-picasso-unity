using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class SystemGesturePausePokeInteractableHandler : MonoBehaviour
{
    private PokeInteractable _pokeInteractable;

    private void Awake()
    {
        _pokeInteractable = GetComponentInChildren<PokeInteractable>();
        AccidentalPicassoAppController.Instance.OnSystemGestureUpdate += UpdateGrabbable;
    }

    public void UpdateGrabbable(bool isUsingSystemGesture)
    {
        _pokeInteractable.enabled = !isUsingSystemGesture;

    }

    private void OnDestroy()
    {
        AccidentalPicassoAppController.Instance.OnSystemGestureUpdate -= UpdateGrabbable;
    }
}
