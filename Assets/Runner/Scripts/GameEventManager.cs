using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEventManager
{
    public delegate void GameEvent();

    public static event GameEvent GameStart, GameOver;

    public static void TriggerGameStart()
    {
        GameStart?.Invoke();
    }

    public static void TriggerGameOver()
    {
        GameOver?.Invoke();
    }
}