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
    private Color _color = Color.gray;
    public Color Color
    {
        get { return _color; }
        set { _color = value; }
    }
    private ShapeType _shapeType = ShapeType.Default;
    public ShapeType ShapeType
    {
        get { return _shapeType; }
        set { _shapeType = value; }
    }
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
            UpdateShapeTransform();
            if(shape == null && Color != Color.gray && ShapeType != ShapeType.Default && AccidentalPicassoAppController.Instance.gameController.gameObject.GetComponent<GameSync>().State == GameState.Start)
            {
                CreateRemoteShape(ShapeType, Color);
            }
        }
    }

    public IEnumerator SetModelParameters(string prefabName, Color color)
    {
        yield return new WaitForSeconds(0.1f);
        LocalSetRemoteShapeType(prefabName);
        LocalSetRemoteColor(color);
    }

    public void CreateRemoteShape(ShapeType shapeType, Color color)
    {
        if (shape == null)
            shape = remoteShapeManager.CreateRemoteShape(shapeType);
        RemoteSetLocalColor(color);
    }

    public void RemoteSetLocalShapeType(ShapeType shapeType)
    {
        ShapeType = shapeType;
    }

    public void RemoteSetLocalColor(Color color)
    {
        Color = color;
        if (shape == null) return;
        shape.GetComponentInChildren<MaterialPropertyBlockEditor>().MaterialPropertyBlock.SetColor("_Color", color);
    }

    public void LocalSetRemoteShapeType(string prefabName)
    {
        ShapeType shapeType = remoteShapeManager.prefabNameToShapeType(prefabName);
        ShapeType = shapeType;
        shapeSync.SetShape(ShapeType);
    }

    public void LocalSetRemoteColor(Color color)
    {
        Color = color;
        shapeSync.SetColor(Color);
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
}
