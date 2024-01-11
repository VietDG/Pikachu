using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public static PlayerData playerData;

    public UserProfile userProfile = new UserProfile();

    public TileSpriteData spriteData = new TileSpriteData();

    public DataBooster dataBooster = new DataBooster();

    private static bool isLoad = false;

#if UNITY_EDITOR
    private static readonly string directory = @"E:\Data\LevelDataPikachu";
#else
    private static readonly string directory = Application.persistentDataPath;
#endif
    private static string _fileName = "UserData" + "" + ".txt";


    public static bool IsLoad
    {
        get
        {
            return isLoad;
        }
    }

    public static void Save()
    {
        if (playerData == null || !isLoad) return;

        string filePath = directory + _fileName;

        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(filePath, json);
    }

    public static bool Load(bool isLoadAgain = false)
    {
        if (isLoad == true && isLoadAgain == false) return false;

        string filePath = directory + _fileName;

        FileStream fileStream = File.Open(filePath, FileMode.OpenOrCreate);
        StreamReader sr = new StreamReader(fileStream);
        string playerDataJson = sr.ReadToEnd();
        sr.Close();
        fileStream.Close();

        playerData = JsonUtility.FromJson<PlayerData>(playerDataJson);

        if (playerData == null)
        {
            playerData = new PlayerData();
        }

        isLoad = true;

        return isLoad;
    }
}
