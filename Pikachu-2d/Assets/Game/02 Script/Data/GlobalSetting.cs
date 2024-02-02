using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSetting : SingletonMonoBehaviour<GlobalSetting>
{
    [SerializeField] int _buildNumber;
    public override void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public static AudioClip GetSFX(string audioName)
    {
        return Resources.Load<AudioClip>("SFX/" + audioName);
    }

    public int GetBuildNumber()
    {
        return this._buildNumber;
    }
}
