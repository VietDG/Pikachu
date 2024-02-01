using PopupSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupTutPlay : SingletonPopup<PopupTutPlay>
{
    [Header("------------------REFERENCE-------------------")]
    [SerializeField] GameObject _tut1, _tut2, _tut3;
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

    public void OnclickTut1()
    {
        _tut1.SetActive(false);
        _tut2.SetActive(true);
    }

    public void OnClickTut2()
    {
        _tut2.SetActive(false);
        _tut3.SetActive(true);
    }
}
