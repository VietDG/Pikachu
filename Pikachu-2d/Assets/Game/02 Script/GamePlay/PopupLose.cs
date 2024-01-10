using PopupSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupLose : SingletonPopup<PopupLose>
{
    public void Show()
    {
        base.Show();
    }

    public void Close()
    {
        base.Hide();
    }

}
