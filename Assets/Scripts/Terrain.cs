using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
    private GameObject player;
    private SpawnManager spawnManager;
    private void Move()
    {
        transform.position = new Vector3(player.transform.position.x + 7.38f, spawnManager.LowestYOfPlatform - 20f, transform.position.z);
    }
    private void Start()
    {
        player = GameObject.Find("Player");
        spawnManager = GameObject.Find("GameManager").GetComponent<SpawnManager>();
    }
    private void FixedUpdate()
    {
        Move();
    }
}
