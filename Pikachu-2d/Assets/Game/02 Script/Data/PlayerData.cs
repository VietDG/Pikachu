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

#if UNITY_EDITOR
    private static readonly string directory = @"E:\Data\LevelDataPikachu";
#else
    private static readonly string directory = Application.persistentDataPath;
#endif
    private static string _fileName = "UserData" + "" + ".txt";

    [SerializeField] TextAsset _textAsset;

    private void Start()
    {
        Load();
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
