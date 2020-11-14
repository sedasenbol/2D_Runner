using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private UIManager uIManager;
    private int coins = 0;
    private bool isAlive;
    private CameraFollow cameraFollow;
    // Start is called before the first frame update
    void Start()
    {
        cameraFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        uIManager = GameObject.Find("UI_Manager").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GameOver()
    {
        cameraFollow.GameOver();
    }
    public void GetAndSendCoins()
    {
        coins++;
        uIManager.ShowCoins();
    }
}
