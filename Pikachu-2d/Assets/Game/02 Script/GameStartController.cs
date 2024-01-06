using System;
//using Facebook.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameStartController : MonoBehaviour
{
    private static bool pluginLoaded = false;

    public ButtonGoldPig buttonGoldPig;

    public Text textName;

    public static UnityAction<string> ChangeNameAction;

    public Button test;

    public UserStatus userStatus;
    public UserData userdata;

    public void Start()
    {
        if (!pluginLoaded)
        {
            //IAPManager.Instance.Initialized();
            //FB.Init();
            //AdvertiseManager.Instance.Initialize();

            pluginLoaded = true;
        }

        buttonGoldPig.PlayAction = PlayButtonPress;

        ChangeName(UserData.current.userStatus.name);
        ChangeNameAction = ChangeName;
    }

    private void ChangeName(string name)
    {
        textName.text = name;
        UserData.current.userStatus.name = name;
    }

    public void PlayButtonPress()
    {
        //var userData = UserData.current;

        //if (userData.rewardData.removedAds == false)
        //    AdvertiseManager.Instance.ShowInterstitial(LoadGamePlayScene);
        //else
        //    LoadGamePlayScene();

        //AudioManager.PlaySFX(SFXId.Button);

        LoadGamePlayScene();
    }
    public void test1()
    {
        //var userStatus = UserData.current.userStatus;
        //userStatus.coinCount += 10;
        //EventDispatcher.Instance.NotifyEvent("coin_update", userStatus.coinCount);
        //Debug.LogError(userStatus.coinCount);
        // + gold;

    }


    private void LoadGamePlayScene()
    {
        LoadSceneManager.Instance.LoadScene(LoadSceneUtility.PlaySceneName);
    }

    public void SettingButtonPress()
    {

    }

    public void DailyBonusButtonPress()
    {

    }

    public void ProfileButtonPress()
    {

    }
    public void Awake()
    {

    }

}
