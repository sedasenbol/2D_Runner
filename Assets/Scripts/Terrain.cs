using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
    private GameObject player;
    private SpawnManager spawnManager;
    private void Start()
    {
        player = GameObject.Find("Player");
        spawnManager = GameObject.Find("GameManager").GetComponent<SpawnManager>();
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        transform.position = new Vector3(player.transform.position.x + 7.38f, spawnManager.LowestYPosition() - 55f, transform.position.z);
    }
}
