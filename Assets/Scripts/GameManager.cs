using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private CameraFollow cameraFollow;
    private Player player;
    private UIManager uIManager;
    private ConstantDistance constantDist1;
    private ConstantDistance constantDist2;
    private ConstantDistance constantDist3;
    private ConstantDistance constantDist4;
    private GameState gameState = new GameState();
    public GameState StateOfTheGame => gameState;

    private void Start()
    {
        uIManager = GameObject.Find("UI_Manager").GetComponent<UIManager>();
        if (new List<GameState.State> { GameState.State.CountDown, GameState.State.Start }.Contains(gameState.state))
        {
            return;
        }
        cameraFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        player = GameObject.Find("Player").GetComponent<Player>();
        constantDist1 = GameObject.Find("Mountains").GetComponent<ConstantDistance>();
        constantDist2 = GameObject.Find("UI_Coin").GetComponent<ConstantDistance>();
        constantDist3 = GameObject.Find("Clouds").GetComponent<ConstantDistance>();
        constantDist4 = GameObject.Find("Heart_Container").GetComponent<ConstantDistance>();
    }
    public void StartCountDown()
    {
        uIManager.isCountDownActive = true;
        gameState.state = GameState.State.CountDown;
    }
    public void StartGame()
    {
        gameState.state = GameState.State.OnPlay;
        SceneManager.LoadScene(1);
    }
    public void IsPlayerDead()
    {
        gameState.Hearts--;
        cameraFollow.isPlayerAlive = false;
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
    public void GetHearts()
    {
        gameState.Hearts++;
    }
    public void SendCameraPosition(Vector3 cameraPos)
    {
        constantDist1.cameraPos = cameraPos;
        constantDist2.cameraPos = cameraPos;
        constantDist3.cameraPos = cameraPos;
        constantDist4.cameraPos = cameraPos;
    }
    private void PauseSwitch()
    {
        if (gameState.state == GameState.State.Paused)
        {
            gameState.state = GameState.State.Resuming;
            gameState.state = GameState.State.OnPlay;
        }
        else 
        {
            gameState.state = GameState.State.Paused; 
        }
        Time.timeScale = Mathf.Abs(Time.timeScale-1);
    }
    private void ReplayGame()
    {
        player.StartAgain();
        if (gameState.state == GameState.State.IsDead)
        {
            gameState.state = GameState.State.Replaying;
            gameState.state = GameState.State.OnPlay;
        }
        else
        {
            gameState.Hearts = 3;
            gameState.Coins = 0;
            gameState.state = GameState.State.Restarted;
            gameState.state = GameState.State.OnPlay;
        }
        Time.timeScale = 1;
    }
}
