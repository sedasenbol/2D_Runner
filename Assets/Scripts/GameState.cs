using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public int Hearts = 3;
    public int Coins = 0;
    public State state = GameState.State.Start;
    public Scene scene; 
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
        StartMenu,
        Game,
    }


}
