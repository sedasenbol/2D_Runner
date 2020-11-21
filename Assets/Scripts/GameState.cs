using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public enum State
    {
        Start,
        CountDown,
        OnPlay,
        Paused,
        Resuming,
        IsDead,
        Replaying,
        GameOver,
        Restarted,
    }
    public enum Scene
    {
        Start,
        Game,
    }
    private int hearts = 3;
    private int coins = 0;
    private int score = 0;
    private bool isAlive = true;
    private State currentState = State.OnPlay;
    private Scene currentScene = Scene.Game;
    public int Hearts { get { return hearts; } set { hearts = value; } }
    public int Coins { get { return coins; } set { coins = value; } }
    public int Score { get { return score; } set { score = value; } }
    public bool IsAlive { get { return isAlive; } set { isAlive= value; } }
    public State CurrentState { get { return currentState; } set { currentState = value; } }
    public Scene CurrentScene { get { return currentScene; } set { currentScene = value; } }



}
