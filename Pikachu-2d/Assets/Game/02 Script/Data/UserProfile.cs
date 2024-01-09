using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserProfile
{
    public int level = 1;
    public int coinCount;
    public int starCount;
}

[Serializable]
public class SpriteData
{
    public int tileSpriteIndex = 0;
}

[Serializable]
public class DataBooster
{
    public int findMatch = 100;
    public int shuffle = 100;
    public int swap = 100;
}
