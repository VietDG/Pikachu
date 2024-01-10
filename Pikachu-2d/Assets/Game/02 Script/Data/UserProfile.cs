using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserProfile
{
    public int totalLevel = 1;
    public int totalCoin;
    public int totalStar;
}

[Serializable]
public class TileSpriteData
{
    public int tileSpriteDataIndex = 0;
}

[Serializable]
public class DataBooster
{
    public int findMatch = 100;
    public int shuffle = 100;
    public int swap = 100;
}
