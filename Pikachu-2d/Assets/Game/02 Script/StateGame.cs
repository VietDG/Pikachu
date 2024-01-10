﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StateGame
{
    private static bool isPlay = true;

    private static bool isPause;

    public static bool IsPlay()
    {
        return isPlay;
    }

    public static bool IsPause()
    {
        return isPause;
    }

    public static void Play()
    {
        isPlay = true;
        isPause = false;
    }

    public static void PauseGame()
    {
        isPlay = false;
        isPause = true;

        GamePauseEvent?.Invoke();
    }

    public static void NextLevels()
    {
        GameNextLevelEvent?.Invoke();
    }

    public static event Action GamePauseEvent;

    public static event Action GameNextLevelEvent;
}
