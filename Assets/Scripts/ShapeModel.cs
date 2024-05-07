using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShapeType
{
    Cube, Sphere, Cone, Cylinder, Default
}

[RealtimeModel]
public partial class ShapeModel
{
    [RealtimeProperty(1, true, true)]
    private ShapeType _shape;
    [RealtimeProperty(2, true, true)]
    private Color _color;
}
