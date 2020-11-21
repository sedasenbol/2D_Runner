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
    private GameState stateOfTheGame = new GameState();
    public void StartCountDown()
    {
        uIManager.IsCountDownActive = true;
        stateOfTheGame.CurrentState = GameState.State.CountDown;
    }
    public void StartGame()
    {
        stateOfTheGame.CurrentState = GameState.State.OnPlay;
        stateOfTheGame.IsAlive = true;
        SceneManager.LoadScene(1);
    }
    public void IncreaseScore()
    {
        stateOfTheGame.Score++;
    }
    public void IsPlayerDead()
    {
        stateOfTheGame.Hearts--;
        stateOfTheGame.IsAlive = false;
        if (stateOfTheGame.Hearts == 0)
        {
            stateOfTheGame.CurrentState = GameState.State.GameOver;
            Application.Quit();
            return;
        }
        stateOfTheGame.CurrentState = GameState.State.IsDead;
    }
    public void GetCoins()
    {
        stateOfTheGame.Coins++;
    }
    public void GetHearts()
    {
        stateOfTheGame.Hearts++;
    }
    public void GetCameraPosition(Vector3 cameraPos)
    {
        for (int i = 0; i< constantDist.Length; i++)
        {
            constantDist[i].CameraPos = cameraPos;
        }
    }
    private void PauseSwitch()
    {
        if (stateOfTheGame.CurrentState == GameState.State.Paused)
        {
            stateOfTheGame.CurrentState = GameState.State.Resuming;
            stateOfTheGame.CurrentState = GameState.State.OnPlay;
            stateOfTheGame.IsAlive = true;
        }
        else
        {
            stateOfTheGame.IsAlive = false;
            stateOfTheGame.CurrentState = GameState.State.Paused; 
        }
        Time.timeScale = Mathf.Abs(Time.timeScale-1);
    }
    private void ReplayGame()
    {
        player.StartAgain();
        if (stateOfTheGame.CurrentState == GameState.State.IsDead)
        {
            stateOfTheGame.CurrentState = GameState.State.Replaying;
            stateOfTheGame.Score = 0;
            stateOfTheGame.Coins = 0;

        }
        else if (stateOfTheGame.CurrentState == GameState.State.GameOver)
        {
            stateOfTheGame.CurrentState = GameState.State.Restarted;
            stateOfTheGame = new GameState();
        }
        else
        {
            throw new System.Exception("Play/replay button is misplaced.");
        }
        spawnManager.SpawnFromScratch();
        stateOfTheGame.CurrentState = GameState.State.OnPlay;
        stateOfTheGame.IsAlive = true;
        Time.timeScale = 1;
    }
    private void Start()
    {
        uIManager = GameObject.Find("UI_Manager").GetComponent<UIManager>();
        if (SceneManager.GetActiveScene().name == "Start")
        {
            stateOfTheGame.CurrentState = GameState.State.Start;
            stateOfTheGame.CurrentScene = GameState.Scene.Start;
            stateOfTheGame.IsAlive = false;
        }
        if (stateOfTheGame.IsAlive)
        {
            spawnManager = GetComponent<SpawnManager>();
            player = GameObject.Find("Player").GetComponent<Player>();
            constantDist = FindObjectsOfType<ConstantDistance>();
        }
    }
    private void Update()
    {
        if (spawnManager && stateOfTheGame.IsAlive)
        {
            spawnManager.Spawn();
        }
    }
    public GameState StateOfTheGame { get { return stateOfTheGame; } }
}
