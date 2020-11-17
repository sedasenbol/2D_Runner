﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject player;
    private Player playerScript;
    private Vector3 lastPos;
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.SendCameraPosition(transform.position);
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<Player>();
    }
    private void Update()
    {
        MoveForward();
    }
    private void MoveForward()
    {
        if (gameManager.StateOfTheGame.isAlive && !playerScript.isJumping)
        {
            transform.position = new Vector3(player.transform.position.x + 7f, player.transform.position.y + 1f, transform.position.z);
            lastPos = transform.position;
        }
        else if (gameManager.StateOfTheGame.isAlive && playerScript.isJumping)
        {
            transform.position = new Vector3(player.transform.position.x + 7f, lastPos.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(player.transform.position.x + 1f, player.transform.position.y + 1f, transform.position.z);
        }
        gameManager.SendCameraPosition(transform.position);
    }
}
