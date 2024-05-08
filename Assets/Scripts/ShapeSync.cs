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
            previousModel.shapeDidChange -= ShapeDidChange;
            previousModel.colorDidChange -= ColorDidChange;
        }
        if(currentModel != null)
        {
            if(currentModel.isFreshModel)
            {
                currentModel.shape = syncRealtimeToShape.ShapeType;
                currentModel.color = syncRealtimeToShape.Color;
            }
            currentModel.colorDidChange += ColorDidChange;
            currentModel.shapeDidChange += ShapeDidChange;
        }
    }

    public void SetColor(Color color)
    { 
        model.color = color;
    }

    public void SetShape(ShapeType shape)
    {
        model.shape = shape;
    }

    private void ShapeDidChange(ShapeModel model, ShapeType shapeType)
    {
        syncRealtimeToShape.RemoteSetLocalShapeType(shapeType);
    }
    private void ColorDidChange(ShapeModel model, Color color)
    {
        syncRealtimeToShape.RemoteSetLocalColor(color);
    }
}
