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
                currentModel.shape = ShapeType.Cylinder;
                currentModel.color = Color.blue;
            }
            UpdateShape();
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

    private void UpdateShape()
    {
        if(syncRealtimeToShape.shape == null )
        {
            syncRealtimeToShape.CreateShape(model.shape, model.color);
        } else
        {
            syncRealtimeToShape.UpdateLocalColor(model.color);
        }
    }

    private void ShapeDidChange(ShapeModel model, ShapeType shapeType)
    {
        UpdateShape();
    }
    private void ColorDidChange(ShapeModel model, Color value)
    {
        syncRealtimeToShape.UpdateLocalColor(model.color);
    }
}
