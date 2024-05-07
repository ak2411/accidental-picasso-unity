using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

[RequireComponent(typeof(SyncRealtimeToShape))]
public class ShapeSync : RealtimeComponent<ShapeModel>
{
    private SyncRealtimeToShape syncRealtimeToShape;

    private void Awake()
    {
        syncRealtimeToShape = GetComponent<SyncRealtimeToShape>();
    }

    protected override void OnRealtimeModelReplaced(ShapeModel previousModel, ShapeModel currentModel)
    {
        if(previousModel != null)
        {
            Debug.Log("should not be called");
            previousModel.shapeDidChange -= ShapeDidChange;
            previousModel.colorDidChange -= ColorDidChange;
        }
        if(currentModel != null)
        {
            Debug.Log("curr model is not null");
            if(currentModel.isFreshModel)
            {
                Debug.Log("is fresh model");
                currentModel.shape = syncRealtimeToShape.GetShapeType();
                currentModel.color = syncRealtimeToShape.GetColor();
            }
            currentModel.colorDidChange += ColorDidChange;
            currentModel.shapeDidChange += ShapeDidChange;
            Debug.Log("added the events");
        }
    }

    public void SetColor(Color color)
    {
        Debug.Log("color set" + color);
        model.color = color;
    }

    public void SetShape(ShapeType shape)
    {
        Debug.Log("shape set" + shape);
        model.shape = shape;
    }

    private void ShapeDidChange(ShapeModel model, ShapeType shapeType)
    {
        Debug.Log("shape changed" + shapeType);
        syncRealtimeToShape.SetShapeType(shapeType);
    }
    private void ColorDidChange(ShapeModel model, Color color)
    {
        Debug.Log("color changed" + color);
        syncRealtimeToShape.UpdateLocalColor(color);
    }
}
