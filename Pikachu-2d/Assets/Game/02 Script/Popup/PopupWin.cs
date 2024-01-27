using PopupSystem;
using Spine.Unity;
using SS.View;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopupWin : SingletonPopup<PopupWin>
{
    [SerializeField] TMP_Text _levelTxt;
    [SerializeField] AnimBase _animBase;
    [SerializeField]
    SkeletonAnimation _anim;

    public void Show(int value)
    {
        base.Show();
        _levelTxt.text = string.Format(GameLanguage.Get("txt_level_value"), PlayerData.Instance.HighestLevel);
    }

    private void Start()
    {
        PlayAnim();
    }

    private void PlayAnim()
    {
        _animBase.SetAnimationUI(0, false, timeScale: 1, () =>
        {
            _animBase.SetAnimationUI(1, false);
        });
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
