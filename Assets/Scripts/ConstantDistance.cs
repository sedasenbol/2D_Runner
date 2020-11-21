using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantDistance : MonoBehaviour
{
    private readonly Vector3 initialCameraPos = new Vector3(-10,-3,-10);
    private Vector3 initialDist;
    private Vector3 cameraPos;
    private void Start()
    {
        initialDist = transform.position - initialCameraPos;
    }
    private void Update()
    {
        transform.position = cameraPos + initialDist;
    }
    public Vector3 CameraPos { set { cameraPos = value; } }
}

