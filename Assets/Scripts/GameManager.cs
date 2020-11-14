using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private UIManager uIManager;
    private int coins = 0;
    private bool isAlive = true;
    private CameraFollow cameraFollow;
    private ConstantDistance constantDist1;
    private ConstantDistance constantDist2;
    private ConstantDistance constantDist3;
    private ConstantDistance constantDist4;

    private void Start()
    {
        cameraFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        uIManager = GameObject.Find("UI_Manager").GetComponent<UIManager>();
        constantDist1 = GameObject.Find("Mountains").GetComponent<ConstantDistance>();
        constantDist2 = GameObject.Find("UI_Coin").GetComponent<ConstantDistance>();
        constantDist3 = GameObject.Find("Clouds").GetComponent<ConstantDistance>();
        constantDist4 = GameObject.Find("Heart_Container").GetComponent<ConstantDistance>();
    }
    private void Update()
    {
        
    }
    public void GameOver()
    {
        isAlive = false;
        cameraFollow.GameOver();
    }
    public void GetAndSendCoins()
    {
        coins++;
        uIManager.ShowCoins();
    }
    public void SendCameraPosition(Vector3 cameraPos)
    {
        constantDist1.cameraPos = cameraPos;
        constantDist2.cameraPos = cameraPos;
        constantDist3.cameraPos = cameraPos;
        constantDist4.cameraPos = cameraPos;
    }

}
