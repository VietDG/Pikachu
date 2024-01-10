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
        StateGame.PauseGame();
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
        _coinTxt.text = $"{_coin}";
    }

    public void NextLevel()
    {
        base.Hide(() =>
        {
            StateGame.NextLevels();
            var userStatus = PlayerData.playerData.userProfile;

            userStatus.coinCount += _coin;

            SceneManager.LoadScene(Const.SCENE_GAME);

        });
    }
}
