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

    public int ThemeIndex
    {
        get
        {
            return PlayerPrefs.GetInt(Const.KEY_THEME, 0);
        }
        set
        {
            PlayerPrefs.SetInt(Const.KEY_THEME, value);
        }
    }

    public int BoosterFindMatch
    {
        get
        {
            return PlayerPrefs.GetInt(Const.KEY_BOOSTER_FIND_MATCH, 5);
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
            return PlayerPrefs.GetInt(Const.KEY_BOOSTER_SHUFFLE, 5);
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
            return PlayerPrefs.GetInt(Const.KEY_BOOSTER_SWAP, 5);
        }
        set
        {
            PlayerPrefs.SetInt(Const.KEY_BOOSTER_SWAP, value);
        }
    }

    #endregion

    #region Tutorial

    public bool IsShowTutLevel3
    {
        get
        {
            return PlayerPrefs.GetInt(Const.TUTORIAL_LEVEL_3, 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(Const.TUTORIAL_LEVEL_3, value ? 1 : 0);
        }
    }

    public bool IsShowTutLevel4
    {
        get
        {
            return PlayerPrefs.GetInt(Const.TUTORIAL_LEVEL_4, 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(Const.TUTORIAL_LEVEL_4, value ? 1 : 0);
        }
    }

    public bool IsShowTutLevel5
    {
        get
        {
            return PlayerPrefs.GetInt(Const.TUTORIAL_LEVEL_5, 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(Const.TUTORIAL_LEVEL_5, value ? 1 : 0);
        }
    }
    #endregion


    #region Ads
    public bool IsNotRemoveAds
    {
        get
        {
            return PlayerPrefs.GetInt(Const.REMOVE_ADS, 1) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(Const.REMOVE_ADS, !value ? 0 : 1);
        }
    }

    //public int CountTotalAdInterClicked
    //{
    //    get { return PlayerPrefs.GetInt(Const.COUNT_TOTAL_AD_INTER_CLICKED, 0); }
    //    set
    //    {
    //        PlayerPrefs.SetInt(Const.COUNT_TOTAL_AD_INTER_CLICKED, value);
    //        EvtTotalAdInterClicked?.Invoke();
    //    }
    //}

    public int TotalPlay
    {
        get
        {
            return PlayerPrefs.GetInt(Const.KEY_TOTAL_PLAY, 0);
        }
        set
        {
            PlayerPrefs.SetInt(Const.KEY_TOTAL_PLAY, value);
            EventAction.EventTrackLevelPlay?.Invoke();
        }
    }

    public int TotalWin
    {
        get
        {
            return PlayerPrefs.GetInt(Const.KEY_TOTAL_WIN, 0);
        }
        set
        {
            PlayerPrefs.SetInt(Const.KEY_TOTAL_WIN, value);
            EventAction.EventTrackLevelWin?.Invoke();
        }
    }

    public int TotalLose
    {
        get
        {
            return PlayerPrefs.GetInt(Const.KEY_TOTAL_LOSE, 0);
        }
        set
        {
            PlayerPrefs.SetInt(Const.KEY_TOTAL_LOSE, value);
            EventAction.EventTrackLoseLevel?.Invoke();
        }
    }

    public int CountTotalAdInterShown
    {
        get { return PlayerPrefs.GetInt(Const.COUNT_TOTAL_AD_INTER_SHOWN, 0); }
        set
        {
            PlayerPrefs.SetInt(Const.COUNT_TOTAL_AD_INTER_SHOWN, value);
            EventAction.EventtTotalAdInterShown?.Invoke();
        }
    }

    public int CountTotalAdInterHidden
    {
        get { return PlayerPrefs.GetInt(Const.COUNT_TOTAL_AD_INTER_CLOSE, 0); }
        set
        {
            PlayerPrefs.SetInt(Const.COUNT_TOTAL_AD_INTER_CLOSE, value);
            EventAction.EventInterClose?.Invoke();
        }
    }

    public int CountTotalShowReward
    {
        get { return PlayerPrefs.GetInt(Const.COUNT_TOTAL_AD_REWARD_SHOWN, 0); }
        set
        {
            PlayerPrefs.SetInt(Const.COUNT_TOTAL_AD_REWARD_SHOWN, value);
            EventAction.EventRewardShow?.Invoke();
        }
    }


    public int CoutTotalAdRewardComplete
    {
        get { return PlayerPrefs.GetInt(Const.COUNT_TOTAL_AD_REWARD_COMPLETED, 0); }
        set
        {
            PlayerPrefs.SetInt(Const.COUNT_TOTAL_AD_REWARD_COMPLETED, value);
            EventAction.EventRewardComplete?.Invoke();
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
