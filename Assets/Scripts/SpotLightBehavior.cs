using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.Input;

public class SpotLightBehavior : MonoBehaviour
{
    [SerializeField, Interface(typeof(IHand))]
    private Object _leftHand;

    [SerializeField, Interface(typeof(IHand))]
    private Object _rightHand;

    [SerializeField]
    private GameObject _leftThumbTipRef;
    [SerializeField]
    private GameObject _leftIndexTipRef;

    [SerializeField]
    private GameObject _rightThumbTipRef;
    [SerializeField]
    private GameObject _rightIndexTipRef;

    private IHand LeftHand { get; set; }
    private IHand RightHand { get; set; }

    protected virtual void Awake()
    {
        LeftHand = _leftHand as IHand;
        RightHand = _rightHand as IHand;
    }

    private void Update()
    {
        var hand = LeftHand.IsDominantHand ? LeftHand : RightHand;
        Pose rayPose;
        if(hand.GetPointerPose(out rayPose)) {
            transform.SetPositionAndRotation(rayPose.position, rayPose.rotation);
        }
    }
}
