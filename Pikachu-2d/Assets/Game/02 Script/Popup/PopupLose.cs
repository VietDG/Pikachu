using PopupSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopupLose : SingletonPopup<PopupLose>
{
    [SerializeField] GameObject _haveWifi, _noWifi;
    public void Show()
    {
        base.Show();
        StateGame.IsPause();
    }

    private void Start()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            _noWifi.gameObject.SetActive(true);
        }
        else
        {
            _haveWifi.gameObject.SetActive(true);
        }

        GameController.Instance.time = LevelData.Instance.GetLevelConfig(PlayerData.Instance.HighestLevel).leveltime;
        GameController.Instance.uiGamePlayManager.InitTimeToLevel(LevelData.Instance.GetLevelConfig(PlayerData.Instance.HighestLevel).leveltime);
        GameController.Instance.uiGamePlayManager.SetTime(LevelData.Instance.GetLevelConfig(PlayerData.Instance.HighestLevel).leveltime);
    }

    public void OnClickReplay()
    {
        base.Hide(() =>
        {
            SceneManager.LoadScene(Const.SCENE_GAME);
        });
    }

    public void OnClickHome()
    {
        AdsManager.Instance.ShowInterstitial(() =>
        {
            base.Hide(() =>
        {
            SceneManager.LoadScene(Const.SCENE_HOME);
        });
        });
    }

    public void OnClickRevive()
    {
        bool isShowVideo = false;
        AdsManager.Instance.ShowRewardedAd(() =>
        {
            isShowVideo = true;
        }, () =>
        {
            if (isShowVideo)
            {
                base.Hide(() =>
                {
                    SoundManager.Instance.PlaySfxRewind(GlobalSetting.GetSFX("booster_revive"));
                    EventAction.OnRevive?.Invoke();

                });
            }
            else
            {
                Debug.LogError("xem het quang cao di con me may");
            }

        }, null);
    }
}
