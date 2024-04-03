using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.Input;
using AccidentalPicasso.UI.Palette;

public class ShapesManager : MonoBehaviour
{
    [SerializeField]
    private PalettePositionBehavior _palette;

    [SerializeField]
    private GameObject _cubePrefab;
    public List<GameObject> createdShapes = new List<GameObject>();

    public Color selectedColor = Color.red;

    private GameObject _createdShape;
    private IHand _hand;

    public void AddShape(PrimitiveType type, Transform transform) {
        _hand = _palette.GetDominantHand();
        GameObject shape = Instantiate(_cubePrefab);
        createdShapes.Add(shape);
        shape.transform.SetParent(transform, true);        
    }

    void Update() {
        if(_createdShape != null && _hand != null)
        {
            var pinchStrength = _hand.GetFingerPinchStrength(HandFinger.Index);
            var isIndexFingerPinching = _hand.GetFingerIsPinching(HandFinger.Index);
            var confidence = _hand.GetFingerIsHighConfidence(HandFinger.Index);
            if(!isIndexFingerPinching)
            {
                _createdShape = null;
            } else
            {
                Pose fingerTipPose;
                if(_hand.GetJointPose(HandJointId.HandIndexTip, out fingerTipPose)) {
                    _createdShape.transform.position = fingerTipPose.position;
                }
            }
        }
    }
}
