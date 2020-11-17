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
    private GameObject diamond;
    private GameObject spawnedPlatform;
    private Vector3 spawningPoint = new Vector3(9.3f,-4.53f,-4f);
    private Vector3 platformLength = new Vector3(5f,0f,0f);
    private Vector3 verticalGap = new Vector3(0f,2f,0f);
    private Vector3 horizontalGap = new Vector3(3f,0f,0f);
    private Vector3 spawnDistance = new Vector3(26.25f,0f,0f);
    private GameObject[] platformList = new GameObject[10];
    private List<GameObject> heartList;
    private List<GameObject> snowmanList;
    private List<GameObject> starCoinList;
    private List<GameObject> diamondList;
    private float LowestYOfPlatform = -5;
    private void Start()
    {
        player = GameObject.Find("Player");
        platform = Resources.Load("Prefabs/Platform") as GameObject;
        starCoin = Resources.Load("Prefabs/StarCoin") as GameObject;
        heart = Resources.Load("Prefabs/Heart") as GameObject;
        snowman = Resources.Load("Prefabs/Snowman") as GameObject;
        diamond = Resources.Load("Prefabs/Diamond") as GameObject;
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
            AddToList(platformList, spawnedPlatform);
        }

    }
    private void AddToList(GameObject[] list, GameObject gObject)
    {    
        for (int i = 1; i < list.Length; i++)
        {
            list[i - 1] = list[i];
        }
        list[list.Length - 1] = gObject;
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
        spawnedPlatform = Instantiate(platform, pos + verticalGap + horizontalGap, Quaternion.identity);
        return platformLength + verticalGap + horizontalGap;
    }
    private Vector3 SpawnPlatformLowerAway(Vector3 pos)
    {
        spawnedPlatform = Instantiate(platform, pos - verticalGap + horizontalGap, Quaternion.identity);
        return platformLength - verticalGap + horizontalGap;
    }
    private Vector3 SpawnPlatformSameAway(Vector3 pos)
    {
        spawnedPlatform = Instantiate(platform, pos + horizontalGap, Quaternion.identity);
        return platformLength + horizontalGap;
    }
    private Vector3 SpawnPlatformNext(Vector3 pos)
    {
        spawnedPlatform = Instantiate(platform, pos, Quaternion.identity);
        return platformLength;
    }
    public float LowestYPosition()
    {

        if (LowestYOfPlatform > platformList[platformList.Length - 1].transform.position.y)
        {
            return platformList[platformList.Length - 1].transform.position.y;
        }
        return LowestYOfPlatform;
    }
}
