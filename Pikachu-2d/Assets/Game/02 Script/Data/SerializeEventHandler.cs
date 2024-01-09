using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerializeEventHandler : MonoBehaviour
{
    public bool saveData = true;

    public UserData userData;

    private void Awake()
    {
        UserData.Load();
        userData = UserData.current;

        DontDestroyOnLoad(gameObject);
    }

    public void OnApplicationQuit()
    {
        SaveData();
    }

    public void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveData();
        }
    }

    public void SaveData()
    {
#if !UNITY_EDITOR
        saveData = true;
#endif
        if (saveData)
        {
            UserData.Save();
            PlayerPrefs.Save();
        }
    }
}
