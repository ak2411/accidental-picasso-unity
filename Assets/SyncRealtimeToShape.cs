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
    private RemoteShapeManager remoteShapeManager;

    // Start is called before the first frame update
    void Awake()
    {
        realtimeView = GetComponent<RealtimeView>();
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
            UpdateShapeTransform();
        }
    }

    private void UpdateShapeTransform()
    {
        if (shape == null) return;
        shape.transform.SetLocalPositionAndRotation(transform.localPosition, transform.localRotation);
        shape.transform.localScale = transform.localScale;
    }

    private void UpdateRealtimeTransform()
    {
        transform.SetLocalPositionAndRotation(shape.transform.localPosition, shape.transform.localRotation);
        transform.localScale = shape.transform.localScale;
    }

    public void UpdateLocalColor(Color color)
    {
        if (shape == null) return;
        shape.GetComponentInChildren<MaterialPropertyBlockEditor>().MaterialPropertyBlock.SetColor("_Color", color);
    }

    public void CreateShape(ShapeType shapeType, Color color)
    {
        shape = remoteShapeManager.CreateRemoteShape(shapeType);
        UpdateLocalColor(color);
    }
}
