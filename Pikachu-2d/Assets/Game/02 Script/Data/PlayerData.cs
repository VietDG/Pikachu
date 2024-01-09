using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public static PlayerData current;

    public UserProfile userStatus = new UserProfile();

    public SpriteData decorData = new SpriteData();

    public DataBooster boosterData = new DataBooster();

    #region DeviceSerialization
    private static bool isLoaded = false;

#if UNITY_EDITOR
    private static readonly string directory = @"E:\Data\LevelDataPikachu";
#else
    private static readonly string directory = Application.persistentDataPath;
#endif
    private static string fileName = "userdata_tilesconnect" + ".txt";

    public static bool IsLoaded
    {
        get
        {
            return isLoaded;
        }
    }

    public static void Save()
    {
        if (current == null || !isLoaded) return;

        string filePath = directory + fileName;

        string json = JsonUtility.ToJson(current);
        File.WriteAllText(filePath, json);
    }

    public static bool Load(bool forceReload = false)
    {
        if (isLoaded == true && forceReload == false) return false;

        string filePath = directory + fileName;

        FileStream fileStream = File.Open(filePath, FileMode.OpenOrCreate);
        StreamReader sr = new StreamReader(fileStream);
        string playerDataJson = sr.ReadToEnd();
        sr.Close();
        fileStream.Close();

        current = JsonUtility.FromJson<PlayerData>(playerDataJson);

        if (current == null)
        {
            current = new PlayerData();
        }

        isLoaded = true;

        return isLoaded;
    }
    #endregion
}
