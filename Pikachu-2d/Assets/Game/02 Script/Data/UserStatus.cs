using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserStatus
{
    public int level = 1;
    public int coinCount;
    public int starCount;
    public string name = "player";
}

[Serializable]
public class DecorData
{
    public int tilePackIndex = 0;

    public string currentThemeID = "theme_11";
    public List<string> unlockedThemeIDs;
}

[Serializable]
public class BoosterData
{
    public int findMatchAdsCount;
    public int shuffleAdsCount;
    public int swapTexAdsCount;

    public int findMatchCount = 5;
    public int shuffleCount = 5;
    public int swapTexCount = 5;
}

[Serializable]
public class RewardData
{
    public int dailyBonusProgress;
    public string lastReceiveDailyBonusTime;

    public int freeCoinProgress;
    public string lastReceiveFinalFreeCoinTime;

    public bool removedAds;
}

[Serializable]
public class GoldPigData
{
    public int level;
    public int coinAmount;
    public bool removed;
}

[Serializable]
public class TempData
{
    public bool rate;
}