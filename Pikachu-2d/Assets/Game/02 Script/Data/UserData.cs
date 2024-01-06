using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class UserData
{
    public static UserData current;

    public DateTime activeTime;

    public UserStatus userStatus = new UserStatus();

    public DecorData decorData = new DecorData();

    public BoosterData boosterData = new BoosterData();

    public RewardData rewardData = new RewardData();

    public GoldPigData goldPigData = new GoldPigData();

    public TempData tempData;

    public void OnBeforeSerialize()
    {

    }

    public void OnAfterDeserialize()
    {
        activeTime = DateTime.UtcNow;
    }

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

        current.OnBeforeSerialize();

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

        current = JsonUtility.FromJson<UserData>(playerDataJson);

        if (current == null)
        {
            current = new UserData();
        }

        current.OnAfterDeserialize();

        isLoaded = true;

        return isLoaded;
    }
    #endregion
}
