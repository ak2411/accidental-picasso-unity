using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtUser : MonoBehaviour
{
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        var directionToCamera = (mainCamera.transform.position - transform.position).normalized;
        this.transform.rotation = Quaternion.LookRotation(directionToCamera * -0.01f);
    }
}
