using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnManager : MonoBehaviour
{
    private GameObject player;
    private GameObject platform;
    private GameObject starCoin;
    private GameObject heart;
    private GameObject snowman;
    private GameObject diamond;
    private GameObject spawnedPlatform;
    private Vector3 spawningPoint = new Vector3(9.3f,-4.53f,-4f);
    private Vector3 platformLength = new Vector3(5f,0f,0f);
    private Vector3 verticalGap = new Vector3(0f,2f,0f);
    private Vector3 horizontalGap = new Vector3(3f,0f,0f);
    private Vector3 spawnDistance = new Vector3(26.25f,0f,0f);
    private int queueLength = 10;
    Queue<GameObject> platformQueue = new Queue<GameObject>();
    private GameObject[] heartArray = new GameObject[10];
    private GameObject[] snowmanArray = new GameObject[10];
    private GameObject[] starCoinArray = new GameObject[10];
    private GameObject[] diamondArray = new GameObject[10];
    private float LowestYOfPlatform = -5;
    private void Start()
    {
        player = GameObject.Find("Player");
        platform = Resources.Load("Prefabs/Platform") as GameObject;
        starCoin = Resources.Load("Prefabs/StarCoin") as GameObject;
        heart = Resources.Load("Prefabs/Heart") as GameObject;
        snowman = Resources.Load("Prefabs/Snowman") as GameObject;
        diamond = Resources.Load("Prefabs/Diamond") as GameObject;
        for (int i = 0; i<queueLength; i++)
        {
            platformQueue.Enqueue(GameObject.Find("Stage").transform.Find("Spawned_Platform_Container").transform.GetChild(i).gameObject);
        }
        
    }
    public void Spawn()
    {
        if (player.transform.position.x + spawnDistance.x >= spawningPoint.x)
        {
            int whichOne =  Random.Range(0,8);
            switch(whichOne)
            {
                case 0:
                    spawningPoint += SpawnPlatformSameAway(spawningPoint);
                    break;
                case 1:
                    spawningPoint += SpawnPlatformLowerAway(spawningPoint);
                    break;
                case 2:
                    spawningPoint += SpawnPlatformHigherAway(spawningPoint);
                    break;
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                    spawningPoint += SpawnPlatformNext(spawningPoint);
                    break;
            }
            platformQueue.Enqueue(platformQueue.Peek());
            platformQueue.Dequeue();
        }
    }
    private void SpawnCoin(Vector3 pos)
    {

    }
    private void SpawnDiamond()
    {

    }
    private void SpawnTripleCoins(Vector3 pos)
    {

    }
    private void SpawnSnowman(Vector3 pos)
    {

    }
    private void SpawnHeart(Vector3 pos)
    {

    }
    private Vector3 SpawnPlatformHigherAway(Vector3 pos)
    {
        platformQueue.Peek().transform.position = pos + horizontalGap + verticalGap;
        return platformLength + verticalGap + horizontalGap;
    }
    private Vector3 SpawnPlatformLowerAway(Vector3 pos)
    {
        platformQueue.Peek().transform.position = pos + horizontalGap - verticalGap;
        return platformLength - verticalGap + horizontalGap;
    }
    private Vector3 SpawnPlatformSameAway(Vector3 pos)
    {
        platformQueue.Peek().transform.position = pos + horizontalGap;
        return platformLength + horizontalGap;
    }
    private Vector3 SpawnPlatformNext(Vector3 pos)
    {
        platformQueue.Peek().transform.position = pos;
        return platformLength;
    }
    public float LowestYPosition()
    {
        if (LowestYOfPlatform > platformQueue.Peek().transform.position.y)
        {
            return platformQueue.Peek().transform.position.y;
        }
        return LowestYOfPlatform;
    }
}
