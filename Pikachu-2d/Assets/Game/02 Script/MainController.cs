using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    private static MainController Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    private int count;

    public static bool Block()
    {
        return Instance.count > 0;
    }

    public static void Augment()
    {
        Instance.count++;
    }

    public static void SetAllTileSize()
    {
        Instance.count = Mathf.Max(0, Instance.count - 1);
    }
}
