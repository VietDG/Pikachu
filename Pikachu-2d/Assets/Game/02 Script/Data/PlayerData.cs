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

    private static bool isChecker = false;

    private void Start()
    {
        Save();
    }

    public static bool IsLoad
    {
        get
        {
            return isChecker;
        }
    }

    public static void Save()
    {
        if (playerData == null || !isChecker) return;

        string json = JsonUtility.ToJson(playerData, true);
        File.WriteAllText(Application.dataPath + "/UserData.json", json);
    }

    public static bool Load(bool isCheck = false)
    {
        if (isChecker == true && isCheck == false) return false;

        if (playerData == null)
        {
            string fileContent = File.ReadAllText(Application.dataPath + "/UserData.json");

            playerData = JsonUtility.FromJson<PlayerData>(fileContent);
        }

        isChecker = true;

        return isChecker;
    }
}
