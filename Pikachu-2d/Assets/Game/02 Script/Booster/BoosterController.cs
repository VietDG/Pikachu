﻿using System;
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

    public Image ads;

    public TMP_Text amoutText;

    public Func<int> getCount;

    public Action<int> setCount;

    public TypeBooster typeBooster;

    public abstract bool isUseBooster();

    public void UseBooster()
    {
        int count = getCount();

        if (count > 0)
        {
            if (isUseBooster())
                setCount(count - 1);

            StartAction();
        }
        else
        {
            Debug.LogError("Popup Buy booster");
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
        int count = getCount();

        if (count != 0)
        {
            amoutText.gameObject.SetActive(true);
            amoutText.text = count.ToString();
            ads.gameObject.SetActive(false);
        }
        else
        {
            amoutText.gameObject.SetActive(false);
            amoutText.text = count.ToString();
            ads.gameObject.SetActive(true);
        }
    }
}