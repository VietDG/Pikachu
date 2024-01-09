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
}

[Serializable]
public class BoosterData
{
    public int findMatchAdsCount;
    public int shuffleAdsCount;
    public int swapTexAdsCount;

    public int findMatchCount = 100;
    public int shuffleCount = 100;
    public int swapTexCount = 100;
}

//[Serializable]
//public class RewardData
//{
//    public int dailyBonusProgress;
//    public string lastReceiveDailyBonusTime;

//    public int freeCoinProgress;
//    public string lastReceiveFinalFreeCoinTime;

//    public bool removedAds;
//}
