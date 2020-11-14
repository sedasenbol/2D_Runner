using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject player;
    public bool isAlive = false;
    // Start is called before the first frame update

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.SendCameraPosition(transform.position);
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
    private void FixedUpdate()
    {
        MoveForward();
    }
    private void MoveForward()
    {
        if (!isAlive)
        {
            this.transform.position = new Vector3(player.transform.position.x + 7f, player.transform.position.y + 3.22f, transform.position.z);
        }
        else
        {
            this.transform.position = new Vector3(player.transform.position.x + 1f, player.transform.position.y + 3.22f, transform.position.z);
        }
        gameManager.SendCameraPosition(transform.position);
    }
}
