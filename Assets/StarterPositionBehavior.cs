using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class StarterPositionBehavior : MonoBehaviour
{
    [SerializeField]
    private Vector3 starterOffset = new Vector3(0.0f, 0.01f, 0.05f);
    // Start is called before the first frame update
    void Start()
    {
        PositionBeforeUser();
    }

    private void PositionBeforeUser()
    {
        var cameraTransform = Camera.main.transform;
        var starterPos = cameraTransform.position + cameraTransform.forward * starterOffset.z + cameraTransform.up * starterOffset.y;
        var directionToCamera = (cameraTransform.position - starterPos).normalized;
        this.transform.position = starterPos;
        //this.transform.SetPositionAndRotation(starterPos, Quaternion.LookRotation(directionToCamera * 0.01f));
    }
}
