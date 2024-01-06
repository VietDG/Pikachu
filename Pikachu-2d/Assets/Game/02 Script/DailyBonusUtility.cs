using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyBonusUtility : MonoBehaviour
{
	public static int GetCurrentDay()
	{
		return UserData.current.rewardData.dailyBonusProgress;
	}

	public static void IncreaseCurrentDay()
	{
		var rewardData = UserData.current.rewardData;
		rewardData.dailyBonusProgress = (rewardData.dailyBonusProgress + 1) % 7;
	}

	public static void SaveLastReceiveTimeAsPresent()
	{
		UserData.current.rewardData.lastReceiveDailyBonusTime = DateTime.Now.ToString();
	}

	public static bool Available()
	{
		if (true)
		{
			var rewardData = UserData.current.rewardData;

			if (string.IsNullOrEmpty(rewardData.lastReceiveDailyBonusTime))
			{
				return true;
			}
			else
			{
				var lastReceiveDateTime = DateTimeUtility.Get(rewardData.lastReceiveDailyBonusTime);
				var currentDateTime = DateTime.Now;

				if (lastReceiveDateTime >= currentDateTime
					|| (lastReceiveDateTime.Year == currentDateTime.Year
					&& lastReceiveDateTime.Month == currentDateTime.Month
					&& lastReceiveDateTime.Day == currentDateTime.Day))
				{
					return false;
				}

				return true;
			}
		}

		return false;
	}
}
