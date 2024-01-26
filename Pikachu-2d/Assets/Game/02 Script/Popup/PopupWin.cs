using PopupSystem;
using SS.View;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopupWin : SingletonPopup<PopupWin>
{
    [SerializeField] TMP_Text _levelTxt;

    public void Show(int value)
    {
        base.Show();
        _levelTxt.text = string.Format(GameLanguage.Get("txt_level_value"), PlayerData.Instance.HighestLevel);
    }

    public void Close()
    {
        base.Hide();
    }

    public void NextLevel()
    {
        base.Hide(() =>
        {
            StateGame.NextLevels();
        });
    }

    public void Home()
    {
        base.Hide(() =>
        {
            Manager.Load(DHome.SCENE_NAME);
        });
    }
}
