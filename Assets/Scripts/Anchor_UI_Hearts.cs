using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anchor_UI_Hearts : MonoBehaviour
{
    private Camera cam;
    private void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
    private void Update()
    {
        SetMyPosition();
    }
    private void SetMyPosition()
    {
        transform.position = cam.ViewportToWorldPoint(new Vector3(0.05f, 0.92f, 1f));
    }

}