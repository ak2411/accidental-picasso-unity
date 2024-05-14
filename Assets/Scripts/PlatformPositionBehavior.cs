using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class PlatformPositionBehavior : MonoBehaviour
{
    [SerializeField]
    private RealtimeTransform realtimeTransform;
    [SerializeField]
    private Vector3 increment = new Vector3(0, 0.01f, 0);
    [SerializeField]
    private float maxHeight =  2;
    [SerializeField]
    private float minHeight = 0.2f;

    private void Awake()
    {
        if(!realtimeTransform)
        {
            realtimeTransform = GetComponent<RealtimeTransform>();
        }
    }

    public void MoveUp()
    {
        realtimeTransform.RequestOwnership();
        if (transform.localPosition.y >= maxHeight) return;
        transform.localPosition += increment;
    }
    public void MoveDown()
    {
        realtimeTransform.RequestOwnership();
        if (transform.localPosition.y <= minHeight) return;
        transform.localPosition -= increment;
    }
}
