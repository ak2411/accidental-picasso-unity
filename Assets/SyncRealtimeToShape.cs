using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using Oculus.Interaction;

[RequireComponent(typeof(RealtimeView))]
public class SyncRealtimeToShape : MonoBehaviour
{
    private RealtimeView realtimeView;
    public GameObject shape;
    public Color localColor = Color.gray;
    public ShapeType localShapeType = ShapeType.Default;
    private RemoteShapeManager remoteShapeManager;
    private ShapeSync shapeSync;

    // Start is called before the first frame update
    void Awake()
    {
        realtimeView = GetComponent<RealtimeView>();
        shapeSync = GetComponent<ShapeSync>();
        remoteShapeManager = FindObjectOfType<RemoteShapeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(realtimeView.isOwnedLocallySelf)
        {
            UpdateRealtimeTransform();
        } else
        {
            Debug.Log("else condition");
            UpdateShapeTransform();
            if(shape == null && localColor != Color.gray && localShapeType != ShapeType.Default)
            {
                CreateShape(localShapeType, localColor);
            }
        }
    }

    public ShapeType GetShapeType()
    {
        return localShapeType;
    }

    public void SetShapeType(string prefabName)
    {
        ShapeType shapeType = remoteShapeManager.prefabNameToShapeType(prefabName);
        localShapeType = shapeType;
        shapeSync.SetShape(localShapeType);
    }

    public void SetShapeType(ShapeType shapeType)
    {
        localShapeType = shapeType;
    }

    public void SetColor(Color color)
    {
        localColor = color;
        shapeSync.SetColor(localColor);
    }

    public Color GetColor()
    {
        return localColor;
    }

    private void UpdateShapeTransform()
    {
        if (shape == null) return;
        shape.transform.SetLocalPositionAndRotation(transform.localPosition, transform.localRotation);
        shape.transform.localScale = transform.localScale;
    }

    private void UpdateRealtimeTransform()
    {
        if (shape == null) return;
        transform.SetLocalPositionAndRotation(shape.transform.localPosition, shape.transform.localRotation);
        transform.localScale = shape.transform.localScale;
    }

    public void UpdateLocalColor(Color color)
    {
        localColor = color;
        if (shape == null) return;
        shape.GetComponentInChildren<MaterialPropertyBlockEditor>().MaterialPropertyBlock.SetColor("_Color", color);
    }

    public void CreateShape(ShapeType shapeType, Color color)
    {
        if (shape == null)
            shape = remoteShapeManager.CreateRemoteShape(shapeType);
        UpdateLocalColor(color);
    }
}
