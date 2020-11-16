using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private GameObject player;
    private GameObject platform;
    private GameObject starCoin;
    private GameObject heart;
    private GameObject snowman;
    private Vector3 spawningPoint = Vector3.zero;
    private Vector3 platformLength = new Vector3(5f,0f,0f);
    private Vector3 verticalGap;
    private Vector3 horizontalGap;
    private Vector3 playerHorizontalDistance = new Vector3(0f,-0.53f,0f);
    private Vector3 spawnDistance = new Vector3(26.25f,0f,0f);
    private void Start()
    {
        player = GameObject.Find("Player");
        platform = Resources.Load("Prefabs/Platform") as GameObject;
        starCoin = Resources.Load("Prefabs/StarCoin") as GameObject;
        heart = Resources.Load("Prefabs/Heart") as GameObject;
        snowman = Resources.Load("Prefabs/Snowman") as GameObject;
    }
    public void SpawnPlatforms()
    {
        if (player.transform.position.x + spawnDistance.x >= spawningPoint.x)
        {
            spawningPoint += SpawnPlatformNext(player.transform.position + playerHorizontalDistance + spawnDistance);
        }
        
    }
    private void SpawnCoin(Vector3 pos)
    {

    }
    private void SpawnSnowman(Vector3 pos)
    {

    }
    private void SpawnHeart(Vector3 pos)
    {

    }
    private void SpawnPlatformHigherAway(Vector3 pos)
    {

    }
    private void SpawnPlatformLowerAway(Vector3 pos)
    {

    }
    private void SpawnPlatformSameAway(Vector3 pos)
    {

    }
    private Vector3 SpawnPlatformNext(Vector3 pos)
    {
        GameObject platform1 = Instantiate(platform, pos, Quaternion.identity);
        return platformLength;
    }
}
