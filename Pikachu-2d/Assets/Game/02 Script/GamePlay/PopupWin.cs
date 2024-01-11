using PopupSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopupWin : SingletonPopup<PopupWin>
{
    [SerializeField] TMP_Text _coinTxt;

    private int _coin;

    public void Show(int value)
    {
        base.Show();
        _coinTxt.text = $"{value}";
    }

    public void Close()
    {
        base.Hide();
    }

    private void Start()
    {
        _coinTxt.text = $"{_coin}";
    }

    public void NextLevel()
    {
        base.Hide(() =>
        {
            StateGame.NextLevels();
        });
    }
}
