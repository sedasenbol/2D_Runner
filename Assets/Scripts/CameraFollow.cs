using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject player;
    private Player playerScript;
    private Vector3 lastPos;
    private void MoveForward()
    {
        if (gameManager.StateOfTheGame.IsAlive && !playerScript.IsJumping)
        {
            transform.position = new Vector3(player.transform.position.x + 7f, player.transform.position.y + 1f, transform.position.z);
            lastPos = transform.position;
        }
        else if (gameManager.StateOfTheGame.IsAlive && playerScript.IsJumping)
        {
            transform.position = new Vector3(player.transform.position.x + 7f, lastPos.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(player.transform.position.x + 1f, player.transform.position.y + 1f, transform.position.z);
        }
        gameManager.GetCameraPosition(transform.position);
    }
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.GetCameraPosition(transform.position);
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<Player>();
    }
    private void Update()
    {
        MoveForward();
    }
}
