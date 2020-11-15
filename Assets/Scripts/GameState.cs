using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public int Hearts = 3;
    public int Coins = 0;
    public State state;
    public Scene scene; 
    public enum State
    {
        Start,
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
        CountDown,
        Game,
    }


}
