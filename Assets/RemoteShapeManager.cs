using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteShapeManager : MonoBehaviour
{
    [SerializeField]
    private GameObject cubeRef;
    [SerializeField]
    private GameObject cylinderRef;
    [SerializeField]
    private GameObject sphereRef;
    [SerializeField]
    private GameObject coneRef;


    public GameObject CreateRemoteShape(ShapeType shapeType)
    {
        GameObject primitiveRef = getShapePrimitive(shapeType);
        GameObject remotePrimitive = GameObject.Instantiate(primitiveRef);
        remotePrimitive.transform.SetParent(null, false);
        return remotePrimitive;
    }

    private GameObject getShapePrimitive(ShapeType shapeType)
    {
        switch (shapeType)
        {
            case ShapeType.Cube:
                return cubeRef;
            case ShapeType.Sphere:
                return sphereRef;
            case ShapeType.Cone:
                return coneRef;
            case ShapeType.Cylinder:
                return cylinderRef;
        }
        return null;
    }

    public ShapeType? prefabNameToShapeType(string name)
    {
        if(name.Contains(cubeRef.name))
                return ShapeType.Cube;
        else if(name.Contains(cylinderRef.name))
                return ShapeType.Cylinder;
        else if (name.Contains(coneRef.name))
                return ShapeType.Cone;
        else if (name.Contains(sphereRef.name))
                return ShapeType.Sphere;
        return null;
    }
}