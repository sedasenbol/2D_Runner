using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private CameraFollow cameraFollow;
    private ConstantDistance constantDist1;
    private ConstantDistance constantDist2;
    private ConstantDistance constantDist3;
    private ConstantDistance constantDist4;
    private GameState gameState = new GameState();
    public GameState StateOfTheGame => gameState;

    private void Start()
    {
        cameraFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        constantDist1 = GameObject.Find("Mountains").GetComponent<ConstantDistance>();
        constantDist2 = GameObject.Find("UI_Coin").GetComponent<ConstantDistance>();
        constantDist3 = GameObject.Find("Clouds").GetComponent<ConstantDistance>();
        constantDist4 = GameObject.Find("Heart_Container").GetComponent<ConstantDistance>();
        gameState.state = GameState.State.OnPlay;
    }
    private void Update()
    {
        
    }
    public void IsPlayerDead()
    {
        gameState.Hearts--;
        cameraFollow.isAlive = true;
        gameState.state = GameState.State.IsDead;
        Time.timeScale = 0;
        if (gameState.Hearts == 0)
        {
            gameState.state = GameState.State.GameOver;
            Application.Quit();
        }
    }
    public void GetCoins()
    {
        gameState.Coins++;
    }
    public void SendCameraPosition(Vector3 cameraPos)
    {
        constantDist1.cameraPos = cameraPos;
        constantDist2.cameraPos = cameraPos;
        constantDist3.cameraPos = cameraPos;
        constantDist4.cameraPos = cameraPos;
    }
    public void PauseSwitch()
    {
        if (gameState.state == GameState.State.Paused)
        {
            gameState.state = GameState.State.Resuming;
        }
        else 
        {
            gameState.state = GameState.State.Paused; 
        }
        Time.timeScale = Mathf.Abs(Time.timeScale-1);
    }
    public void ReplayGame() //tamamla
    {
        if (gameState.state == GameState.State.IsDead)
        {
            gameState.state = GameState.State.Replaying;
        }
        else
        {
            gameState.state = GameState.State.Restarted;
            gameState.state = GameState.State.OnPlay;
        }
        Time.timeScale = 1;
    }
}
