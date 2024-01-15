using PopupSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupBuyBooster : SingletonPopup<PopupBuyBooster>
{
    public void Show()
    {
        base.Show();
    }

    public void Close()
    {
        base.Hide();
    }

    public void InitDataBooster(TypeBooster typeBooster)
    {
        if (typeBooster == TypeBooster.Swap)
        {

        }

    }
}
