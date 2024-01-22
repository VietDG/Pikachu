using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSetting : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public static AudioClip GetSFX(string audioName)
    {
        return Resources.Load<AudioClip>("SFX/" + audioName);
    }
}
