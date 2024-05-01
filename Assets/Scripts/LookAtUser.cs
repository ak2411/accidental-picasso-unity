using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtUser : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField]
    private float offset = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        var directionToCamera = (mainCamera.transform.position - transform.position).normalized * offset;
        this.transform.rotation = Quaternion.LookRotation(new Vector3(directionToCamera.x, 0.0f, directionToCamera.z));
    }
}
