using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGoldPig : MonoBehaviour
{
    public Image image;

    public Text amountText;

    public Action PlayAction;

    public void Start()
    {
        var goldPigData = UserData.current.goldPigData;
        var smashRange = GoldPigUtility.GetSmashRange();

        amountText.text = goldPigData.coinAmount.ToString() + "/" + smashRange.Item2.ToString();
        image.fillAmount = (float)goldPigData.coinAmount / smashRange.Item2;
    }

    public void ButtonPresss()
    {
        var userStatus = UserData.current.userStatus;
    }
}
