using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public int Hearts = 3;
    public int Coins = 0;
    public int score = 0;
    public State state = State.OnPlay;
    public Scene scene = Scene.Game;
    public bool isAlive = true;
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
}
