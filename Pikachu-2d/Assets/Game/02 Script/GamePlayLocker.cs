using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayLocker : MonoBehaviour
{
    private static GamePlayLocker current;

    private void Awake()
    {
        current = this;
    }

    private void OnDestroy()
    {
        current = null;
    }

    private int lockCount;

    public static bool IsLocked()
    {
        return current.lockCount > 0;
    }

    public static void Retain()
    {
        current.lockCount++;
    }

    public static void Release()
    {
        current.lockCount = Mathf.Max(0, current.lockCount - 1);
    }
}
