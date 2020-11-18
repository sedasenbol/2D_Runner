using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Player player;
    private UIManager uIManager;
    private SpawnManager spawnManager;
    private ConstantDistance[] constantDist;
    private GameState gameState = new GameState();
    public GameState StateOfTheGame => gameState;

    private void Start()
    {
        uIManager = GameObject.Find("UI_Manager").GetComponent<UIManager>();
        if (SceneManager.GetActiveScene().name == "Start")
        {
            gameState.state = GameState.State.Start;
            gameState.scene = GameState.Scene.Start;
            gameState.isAlive = false;
        }
        if (!gameState.isAlive)
        {
            return;
        }
        spawnManager = GetComponent<SpawnManager>();
        player = GameObject.Find("Player").GetComponent<Player>();
        constantDist = FindObjectsOfType<ConstantDistance>();
    }
    private void Update()
    {
        GetComponent<SpawnManager>().Spawn();
    }
    public void StartCountDown()
    {
        uIManager.isCountDownActive = true;
        gameState.state = GameState.State.CountDown;
    }
    public void StartGame()
    {
        gameState.state = GameState.State.OnPlay;
        gameState.isAlive = true;
        SceneManager.LoadScene(1);
    }
    public void IncreaseScore()
    {
        gameState.score++;
    }

    public void IsPlayerDead()
    {
        gameState.hearts--;
        gameState.isAlive = false;
        if (gameState.hearts == 0)
        {
            gameState.state = GameState.State.GameOver;
            Application.Quit();
            return;
        }
        gameState.state = GameState.State.IsDead;
    }
    public void GetCoins()
    {
        gameState.coins++;
    }
    public void GetHearts()
    {
        gameState.hearts++;
    }
    public void SendCameraPosition(Vector3 cameraPos)
    {
        for (int i = 0; i< constantDist.Length; i++)
        {
            constantDist[i].cameraPos = cameraPos;
        }
    }
    private void PauseSwitch()
    {
        if (gameState.state == GameState.State.Paused)
        {
            gameState.state = GameState.State.Resuming;
            gameState.state = GameState.State.OnPlay;
            gameState.isAlive = true;
        }
        else 
        {
            gameState.isAlive = false;
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
            spawnManager.SpawnFromScratch();
            gameState.state = GameState.State.OnPlay;
            gameState.isAlive = true;
        }
        else
        {
            gameState.state = GameState.State.Restarted;
            gameState = new GameState();
            spawnManager.SpawnFromScratch();
            gameState.state = GameState.State.OnPlay;
            gameState.isAlive = true;
        }
        Time.timeScale = 1;
    }
}
