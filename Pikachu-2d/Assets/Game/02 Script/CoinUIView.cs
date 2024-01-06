using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUIView : MonoBehaviour
{
    public Text textCoin;

    private void Start()
    {
        UpdateCoin(UserData.current.userStatus.coinCount);

        EventDispatcher.Instance.RegisterEvent("coin_update", UpdateCoin);
    }

    private void OnDestroy()
    {
        EventDispatcher.Instance.RemoveEvent("coin_update", UpdateCoin);
    }

    private void UpdateCoin(object param)
    {
        textCoin.text = param.ToString();
    }

    public void OpenShop()
    {

    }
}
