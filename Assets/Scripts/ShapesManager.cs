using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapesManager : MonoBehaviour
{
    public List<GameObject> createdShapes = new List<GameObject>();
    public bool addShape = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    void AddShape(PrimitiveType type) {
        GameObject shape = GameObject.CreatePrimitive(type);
        createdShapes.Add(shape);
        shape.transform.SetParent(this.transform, true);        
    }

    void Update() {
        if (addShape) {
            AddShape(PrimitiveType.Cube);
            addShape = false;
        }
    }
}
