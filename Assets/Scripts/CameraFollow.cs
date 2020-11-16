using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject player;
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.SendCameraPosition(transform.position);
        player = GameObject.Find("Player");
    }
    private void Update()
    {
        
    }
    private void FixedUpdate()
    {
        MoveForward();
    }
    private void MoveForward()
    {
        if (gameManager.StateOfTheGame.isAlive)
        {
            transform.position = new Vector3(player.transform.position.x + 7f, player.transform.position.y + 3.22f, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(player.transform.position.x + 1f, player.transform.position.y + 3.22f, transform.position.z);
        }
        gameManager.SendCameraPosition(transform.position);
    }
}
