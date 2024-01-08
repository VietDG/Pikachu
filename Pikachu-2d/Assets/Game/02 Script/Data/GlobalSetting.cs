using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSetting : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
