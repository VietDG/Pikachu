using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GamePlayState 
{
    private static bool isPlaying;

    private static bool isPausing;

    public static bool IsPlaying()
    {
        return isPlaying;
    }

    public static bool IsPausing()
    {
        return isPausing;
    }

    public static void Play()
    {
        isPlaying = true;
        isPausing = false;
    }

    public static void Pause()
    {
        isPlaying = false;
        isPausing = true;

        GamePauseEvent?.Invoke();
    }

    public static void Stop()
    {

    }

    public static void Continue()
    {
        isPlaying = true;
        isPausing = false;

        GameContinueEvent?.Invoke();
    }

    public static void Restart()
    {
        GamePreRestartEvent?.Invoke();

        GamePostRestartEvent?.Invoke();
    }

    public static void NextLevel()
    {
        GameNextLevelEvent?.Invoke();
    }

    public static event Action GamePreRestartEvent;

    public static event Action GamePostRestartEvent;

    public static event Action GamePauseEvent;

    public static event Action GameContinueEvent;

    public static event Action GameNextLevelEvent;
}
