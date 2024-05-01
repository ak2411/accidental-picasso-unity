using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class StarterPositionBehavior : MonoBehaviour
{
    [SerializeField]
    private Vector3 starterOffset = new Vector3(0.0f,-0.2f, 0.5f);
    [SerializeField]
    private GameObject centerEye;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PositionBeforeUser());
    }

    private IEnumerator PositionBeforeUser()
    {
        yield return new WaitForEndOfFrame();
        var cameraTransform = centerEye.transform;
        var starterPos = cameraTransform.position + cameraTransform.forward * starterOffset.z + cameraTransform.up * starterOffset.y;
        this.transform.position = starterPos;
    }
}
