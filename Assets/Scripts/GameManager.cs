﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private CameraFollow cameraFollow;
    // Start is called before the first frame update
    void Start()
    {
        cameraFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GameOver()
    {
        cameraFollow.GameOver();
    }
}
