using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum TypeBooster
{
    Swap,
    Shuffle,
    Hint,
}

public abstract class BoosterController : MonoBehaviour
{
    public Image icon;

    public GameObject ads;

    public GameObject redDot;

    public TMP_Text amoutText;

    public Func<int> boosterCount;

    public Action<int> setBoosterCount;

    public TypeBooster typeBooster;

    public UnlockBooster unlockBooster;

    public abstract bool isUseBooster();

    public void UseBooster()
    {
        if (PlayerData.Instance.HighestLevel < unlockBooster._levelUnlock) return;

        int c = boosterCount();

        if (c > 0)
        {
            if (isUseBooster())
                setBoosterCount(c - 1);

            StartAction();
        }
        else
        {
            //xem quang cao
            SetBoosterAds();
        }
    }

    public void SetBoosterAds()
    {
        switch (typeBooster)
        {
            case TypeBooster.Swap:
                PlayerData.Instance.BoosterSwap += 1;
                StartAction();
                break;
            case TypeBooster.Shuffle:
                PlayerData.Instance.BoosterShuffle += 1;
                StartAction();
                break;
            case TypeBooster.Hint:
                PlayerData.Instance.BoosterFindMatch += 1;
                StartAction();
                break;
        }
    }

    public void StartAction()
    {
        int c = boosterCount();

        if (c != 0)
        {
            redDot.SetActive(true);
            amoutText.text = c.ToString();
            ads.SetActive(false);
        }
        else
        {
            redDot.SetActive(false);
            amoutText.text = c.ToString();
            ads.SetActive(true);
        }
    }
}
