using PopupSystem;
using SS.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupLanguage : SingletonPopup<PopupLanguage>
{
    public void Show()
    {
        base.Show();
    }

    public void Close()
    {
        base.Hide(() =>
        {
            PopupSetting.Instance.Show();
        });
    }

    public void SetLanguage(string langCode)
    {
        base.Hide(() =>
        {
            GameLanguage.Instance.SetLanguage(langCode);
            Manager.Load(Const.SCENE_HOME);
        });
    }
}
