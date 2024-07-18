using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;

public class SystemGesturePauseGrabbableHandler : MonoBehaviour
{
    private HandGrabInteractable _handGrabInteractable;

    private void Awake()
    {
        _handGrabInteractable = GetComponentInChildren<HandGrabInteractable>();
        AccidentalPicassoAppController.Instance.OnSystemGestureUpdate += UpdateGrabbable;
    }

    public void UpdateGrabbable(bool isUsingSystemGesture)
    {
        _handGrabInteractable.enabled = !isUsingSystemGesture;

    }

    private void OnDestroy()
    {
        AccidentalPicassoAppController.Instance.OnSystemGestureUpdate -= UpdateGrabbable;
    }
}
