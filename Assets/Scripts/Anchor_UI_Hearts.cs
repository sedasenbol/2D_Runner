using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anchor_UI_Hearts : MonoBehaviour
{
    private Camera cam;
    private Vector3 camTopRightWorldPos;
    private Vector3 camBottomLeftWorldPos;
    private Vector3 coinPos;
    private void Start()
    {
        coinPos = GameObject.Find("UI_Manager").transform.Find("UI_Hearts").transform.position;
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        camTopRightWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        camBottomLeftWorldPos = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        float worldHeight = (camTopRightWorldPos - camBottomLeftWorldPos).y;
        float worldWidth = (camTopRightWorldPos - camBottomLeftWorldPos).x;
        float widthPercentage = (coinPos - camBottomLeftWorldPos).x / worldWidth;
        float heightPercentage = (coinPos - camBottomLeftWorldPos).y / worldHeight;
        Debug.Log(widthPercentage + "," + heightPercentage);
    }


    // Update is called once per frame
    private void Update()
    {
        SetMyPosition();
    }
    private void SetMyPosition()
    {
        transform.position = cam.ViewportToWorldPoint(new Vector3(0.05f,0.92f, 1f));
    }

}