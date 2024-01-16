using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerData : SingletonMonoBehaviour<PlayerData>
{
    #region UserData
    public int HighestLevel
    {
        get
        {
            return PlayerPrefs.GetInt(Const.KEY_HIGHEST_LEVEL, 1);
        }
        set
        {
            PlayerPrefs.SetInt(Const.KEY_HIGHEST_LEVEL, value);
        }
    }

    public int TotalCoin
    {
        get
        {
            return PlayerPrefs.GetInt(Const.KEY_TOTAL_COIN, 200);
        }
        set
        {
            PlayerPrefs.SetInt(Const.KEY_TOTAL_COIN, value);
        }
    }

    public int TotalStar
    {
        get
        {
            return PlayerPrefs.GetInt(Const.KEY_TOTAL_STARS, 0);
        }
        set
        {
            PlayerPrefs.SetInt(Const.KEY_TOTAL_STARS, value);
        }
    }

    public int TileSpriteIndex
    {
        get
        {
            return PlayerPrefs.GetInt(Const.KEY_TILE_SPRITE_INDEX, 0);
        }

        set
        {
            PlayerPrefs.SetInt(Const.KEY_TILE_SPRITE_INDEX, value);
        }
    }

    public int BoosterFindMatch
    {
        get
        {
            return PlayerPrefs.GetInt(Const.KEY_BOOSTER_FIND_MATCH, 50);
        }
        set
        {
            PlayerPrefs.SetInt(Const.KEY_BOOSTER_FIND_MATCH, value);
        }
    }

    public int BoosterShuffle
    {
        get
        {
            return PlayerPrefs.GetInt(Const.KEY_BOOSTER_SHUFFLE, 50);
        }

        set
        {
            PlayerPrefs.SetInt(Const.KEY_BOOSTER_SHUFFLE, value);
        }
    }

    public int BoosterSwap
    {
        get
        {
            return PlayerPrefs.GetInt(Const.KEY_BOOSTER_SWAP, 50);
        }
        set
        {
            PlayerPrefs.SetInt(Const.KEY_BOOSTER_SWAP, value);
        }
    }
    #endregion
    public static PlayerData playerData;

    private static bool isLoad = false;

    //#if UNITY_EDITOR
    //    private static readonly string directory = @"E:\Data\LevelDataPikachu";
    //#else
    //    private static readonly string directory = Application.persistentDataPath;
    //#endif
    //    private static string _fileName = "UserData" + "" + ".txt";

    private static string path;

    private void Start()
    {
        //   Load();
        Save();
    }

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

        // string filePath = directory + _fileName;

        // path = Application.persistentDataPath + "UserData.txt";

        string json = JsonUtility.ToJson(playerData, true);
        File.WriteAllText(Application.dataPath + "/UserData.json", json);
        Debug.LogError("Save");
    }

    public static bool Load(bool isLoadAgain = false)
    {
        if (isLoad == true && isLoadAgain == false) return false;

        //string filePath = directory + _fileName;

        //FileStream fileStream = File.Open(filePath, FileMode.OpenOrCreate);
        //StreamReader sr = new StreamReader(fileStream);
        //string playerDataJson = sr.ReadToEnd();
        //sr.Close();
        //fileStream.Close();

        //  path = Application.persistentDataPath + "UserData.txt";

        if (playerData == null)
        {
            //playerData = new PlayerData();
            string fileContent = File.ReadAllText(Application.dataPath + "/UserData.json");

            playerData = JsonUtility.FromJson<PlayerData>(fileContent);
        }

        isLoad = true;

        Debug.LogError("Load");

        return isLoad;
    }
}
