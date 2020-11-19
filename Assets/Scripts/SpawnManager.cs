using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnManager : MonoBehaviour
{
    public bool isPlayerFlying = false;
    public float playerFlyingHeight = 100f;
    private GameObject player;
    private Vector3 spawningPoint = new Vector3(9.3f,-4.53f,-4f);
    private readonly Vector3 platformLength = new Vector3(5f,0f,0f);
    private readonly Vector3 verticalGap = new Vector3(0f,3f,0f);
    private readonly Vector3 horizontalGap = new Vector3(3f,0f,0f);
    private readonly Vector3 spawnDistance = new Vector3(26.25f,0f,0f);
    private readonly Vector3 pickUpHeight = new Vector3(0f,4f,0f);
    private Vector3 oldSpawningPoint;
    private int queueLength = 10;
    GameObject[] platformArray = new GameObject[10];
    Queue<GameObject> platformQueue = new Queue<GameObject>();
    Queue<GameObject> heartQueue = new Queue<GameObject>();
    Queue<GameObject> snowmanQueue = new Queue<GameObject>();
    Queue<GameObject> starCoinQueue = new Queue<GameObject>();
    Queue<GameObject> diamondQueue = new Queue<GameObject>();
    public float LowestYOfPlatform = -5;
    private void Start()
    {
        player = GameObject.Find("Player");
        SpawnInitialization();
    }
    private void SpawnInitialization()
    {
        var platformContainer = GameObject.Find("Spawned_Platform_Container").transform;
        var starCoinContainer = GameObject.Find("Spawned_StarCoin_Container").transform;
        var heartContainer = GameObject.Find("Spawned_Heart_Container").transform;
        var snowmanContainer = GameObject.Find("Spawned_Snowman_Container").transform;
        var diamondContainer = GameObject.Find("Spawned_Diamond_Container").transform;
        //5 times.
        for (int i = 0; i < queueLength; i++)
        {
            platformQueue.Enqueue(platformContainer.GetChild(i).gameObject);
            starCoinQueue.Enqueue(starCoinContainer.GetChild(i).gameObject);
            heartQueue.Enqueue(heartContainer.GetChild(i).gameObject);
            snowmanQueue.Enqueue(snowmanContainer.transform.GetChild(i).gameObject);
            diamondQueue.Enqueue(diamondContainer.GetChild(i).gameObject);
        }
    }
    public void Spawn()
    {
        if (player.transform.position.x + spawnDistance.x >= spawningPoint.x)
        {
            bool spawnedSnowman = false;
            int whichPlatform =  Random.Range(0,8);
            if(isPlayerFlying)
            {
                whichPlatform = 7;
            }
            oldSpawningPoint = spawningPoint;
            switch(whichPlatform)
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
                    int randomSnowman = Random.Range(0, 8);
                    if (randomSnowman == 7 && !isPlayerFlying)
                    {
                        spawningPoint += SpawnPlatformNext(spawningPoint);
                        platformQueue.Enqueue(platformQueue.Peek());
                        platformQueue.Dequeue();
                        SpawnSnowman();
                        spawnedSnowman = true;
                        spawningPoint += SpawnPlatformNext(spawningPoint);
                        platformQueue.Enqueue(platformQueue.Peek());
                        platformQueue.Dequeue();
                    }
                    platformQueue.Peek();
                    spawningPoint += SpawnPlatformNext(spawningPoint);
                    break;
            }
            platformQueue.Enqueue(platformQueue.Peek());
            if (platformQueue.Peek().transform.position.y < LowestYOfPlatform)
            {
                LowestYOfPlatform = platformQueue.Peek().transform.position.y;
            }
            platformQueue.Dequeue();
            if (!spawnedSnowman)
            {
                int whichPickup = Random.Range(0, 100);
                if (whichPickup < 95)
                {
                    if (isPlayerFlying)
                    {
                        SpawnStarCoinHigh();
                    }
                    else
                    {
                        SpawnStarCoin();
                    }
                }
                else if (whichPickup < 99)
                {
                    SpawnDiamond();
                }
                else
                {
                    SpawnHeart();
                }
            }
        }
    }
    private void SpawnStarCoin()
    {
        starCoinQueue.Peek().transform.position = (oldSpawningPoint + spawningPoint) / 2 + pickUpHeight - platformLength;
        starCoinQueue.Enqueue(starCoinQueue.Peek());
        starCoinQueue.Dequeue();
    }
    private void SpawnStarCoinHigh()
    {
        starCoinQueue.Peek().transform.position = new Vector3(player.transform.position.x + 5, playerFlyingHeight + pickUpHeight.y / 3, player.transform.position.z);
        starCoinQueue.Enqueue(starCoinQueue.Peek());
        starCoinQueue.Dequeue();
    }
    private void SpawnDiamond()
    {
        diamondQueue.Peek().transform.position = (oldSpawningPoint + spawningPoint) / 2 + pickUpHeight - platformLength;
        diamondQueue.Enqueue(diamondQueue.Peek());
        diamondQueue.Dequeue();
    }
    private void SpawnSnowman()
    {
        snowmanQueue.Peek().transform.position = (oldSpawningPoint + spawningPoint) / 2 - 1 * platformLength + new Vector3(0,1.4f,0);
        snowmanQueue.Enqueue(snowmanQueue.Peek());
        snowmanQueue.Dequeue();
    }
    private void SpawnHeart()
    {
        heartQueue.Peek().transform.position = (oldSpawningPoint + spawningPoint) / 2 + pickUpHeight - platformLength;
        heartQueue.Enqueue(heartQueue.Peek());
        heartQueue.Dequeue();
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
    public void SpawnFromScratch()
    {
        for (int i=0; i < queueLength; i++)
        {
            platformQueue.ElementAt(i).transform.position = new Vector3(-60f, -3f, 0f);
            heartQueue.ElementAt(i).transform.position = new Vector3(-60f, -3f, 0f);
            diamondQueue.ElementAt(i).transform.position = new Vector3(-60f, -3f, 0f);
            starCoinQueue.ElementAt(i).transform.position = new Vector3(-60f, -3f, 0f);
            snowmanQueue.ElementAt(i).transform.position = new Vector3(-60f, -3f, 0f);
        }
        spawningPoint = new Vector3(9.3f, -4.53f, -4f);
        LowestYOfPlatform = -5;
    }
}
