using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemGestureHandler : MonoBehaviour
{
    [SerializeField]
    private OVRHand _leftHand;
    [SerializeField]
    private OVRHand _rightHand;
    [SerializeField]
    private bool testSystemGesture = false;

    // Update is called once per frame
    void Update()
    {
        CheckSystemGesture();
    }

    private void CheckSystemGesture()
    {
        bool leftHandSystemGesture = _leftHand.IsSystemGestureInProgress && _leftHand.IsDominantHand;
        bool rightHandSystemGesture = _rightHand.IsSystemGestureInProgress && _rightHand.IsDominantHand;
        bool systemGesture = leftHandSystemGesture || rightHandSystemGesture || testSystemGesture;
        if (AccidentalPicassoAppController.Instance.IsUsingSystemGesture != systemGesture)
        {
            AccidentalPicassoAppController.Instance.IsUsingSystemGesture = systemGesture;
            AccidentalPicassoAppController.Instance.SystemGestureUpdate();
        }
    }
}
