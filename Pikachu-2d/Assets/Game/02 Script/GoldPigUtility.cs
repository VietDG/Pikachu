using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GoldPigUtility 
{
    public static (int, int) GetSmashRange()
    {
        return (1800, 2400);
    }

    public static void Smash()
    {
        UserData.current.goldPigData.coinAmount = 0;
    }
}
