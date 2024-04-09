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
    [SerializeField]
    private GameObject _shapes;
    public List<GameObject> createdShapes = new List<GameObject>();

    public Color selectedColor = Color.red;

    private GameObject _createdShape;
    private IHand _hand;
    private bool _unselectedButStillPinching = false;

    public void UpdateColor(Color color)
    {
        if (selectedColor == color) return;
        selectedColor = color;
        foreach(Transform shape in _shapes.transform)
        {
            shape.gameObject.GetComponent<PaletteShapeInteractableBehavior>().UpdateColor(color);
        }
    }

    public void AddShape(PrimitiveType type, Vector3 position) {
        _hand = _palette.GetDominantHand();
        GameObject shape = Instantiate(_cubePrefab);
        createdShapes.Add(shape);
        shape.transform.position = position;
        shape.transform.SetParent(this.transform, true);
        _createdShape = shape;
    }

    public void UnselectShape(bool checkFinger)
    {
        if(checkFinger)
        {
            var isFingerPinching = _hand.GetIndexFingerIsPinching();
            if (isFingerPinching)
            {
                _unselectedButStillPinching = true;
                return;
            }
        }
        _createdShape = null;
        _hand = null;
        _unselectedButStillPinching = false;
    }

    private void UpdateShapePosition()
    {
        Pose fingerTipPose;
        Pose thumbTipPose;
        if (_hand.GetJointPose(HandJointId.HandIndexTip, out fingerTipPose) && _hand.GetJointPose(HandJointId.HandThumbTip, out thumbTipPose))
        {
            _createdShape.transform.position = (fingerTipPose.position + thumbTipPose.position) / 2 ;
        }
    }

    protected void Start()
    {
        UpdateColor(selectedColor);
    }

    protected void Update() {
        if (_createdShape != null && _hand != null)
        {
            UpdateShapePosition();
            var isFingerPinching = _hand.GetIndexFingerIsPinching();
            if(_unselectedButStillPinching && !isFingerPinching)
            {
                UnselectShape(false);
            }
        }
    }
}
