using PopupSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;

public class PopupWin : SingletonPopup<PopupWin>
{
    [SerializeField] TMP_Text _coinTxt;

    private int _coin;

    public void Show()
    {
        base.Show();
        InitReward();
    }

    public void InitReward()
    {
        GamePlayState.Pause();
        int coin = UnityEngine.Random.Range(20, 25);
        _coinTxt.text = $"{coin}";
        _coin = coin;

    }
    public void Close()
    {
        base.Hide();
    }

    private void Start()
    {
        _coinTxt.text = "20";
    }

    public void NextLevel()
    {
        base.Hide(() =>
        {
            GamePlayState.NextLevel();
            var userStatus = PlayerData.current.userStatus;//

            userStatus.coinCount += _coin;//

            EventDispatcher.Instance.NotifyEvent("coin_update", userStatus.coinCount);// + gold mỗi màn
            SceneManager.LoadScene(Const.SCENE_GAME);

        });
    }
}
