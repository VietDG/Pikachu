using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using Firebase.RemoteConfig;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseManager : SingletonMonoBehaviour<FirebaseManager>
{
    #region Inspector Variables
    #endregion

    #region Member Variables
    public FirebaseApp app;
    public bool isOk = false;
    #endregion

    #region Unity Methods

    public override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        //Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        //{
        //    FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
        //});

        Init();

        //  GlobalEventManager.EvtSendEvent += SendEvent;
        //  GlobalEventManager.EvtUpdateUserProperties += SetUserPropeties;
    }

    private void OnDestroy()
    {
        //   GlobalEventManager.EvtSendEvent -= SendEvent;
        //   GlobalEventManager.EvtUpdateUserProperties -= SetUserPropeties;
    }

    #endregion

    #region Public Methods
    public void Init()
    {
        // Debug.Log(TAG + " Init");
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                app = FirebaseApp.DefaultInstance;
                isOk = true;
                FetchValue();
                FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
                //Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
                //Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;
                //Debug.Log(TAG + " Init done");
            }
            else
            {
                //  Debug.Log(TAG + " Init fail");
            }
        });
    }

    //public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
    //{
    //    Debug.Log("Received Registration Token: " + token.Token);
    //}

    //public void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
    //{
    //    Debug.Log("Received a new message from: " + e.Message.From);
    //}
    #endregion

    #region Private Methods
    private void SendEvent(string eName, Parameter[] parameters)
    {
        if (!isOk)
            return;
        if (parameters == null)
        {
            FirebaseAnalytics.LogEvent(eName);
        }

        else
        {
            FirebaseAnalytics.LogEvent(eName, parameters);
        }

    }
    private void SetUserPropeties()
    {
        if (!isOk)
            return;
    }
    #endregion

    #region Remote Config
    private void FetchValue()
    {
        SetDefaultValue();
        TimeSpan time = new TimeSpan(0, 0, 10);
        FirebaseRemoteConfig.DefaultInstance.FetchAsync(time).ContinueWithOnMainThread(task =>
        {
            var info = FirebaseRemoteConfig.DefaultInstance.Info;
            if (info.LastFetchStatus == LastFetchStatus.Success)
            {
                // Debug.Log($"{TAG}: Read config");
                FirebaseRemoteConfig.DefaultInstance.ActivateAsync();

                AppConfig.Instance.INITIAL_BANNER_AD_LEVEL = GetFetchValue("initial_banner_ad_level", AppConfig.Instance.initialBannerAdLevel);
                AppConfig.Instance.INTER_FEQUENCY_TIME = GetFetchValue("inter_frequency_time", AppConfig.Instance.interFrequencyTime);
                // AppConfig.Instance.WELCOM_TEXT = GetFetStringValue("welcome_text", AppConfig.Instance.WelcomeText);
                AppConfig.Instance.IS_INTER_ACTIVE = GetFetBooleanValue("inter_active");

                //FetchNotiContent();
            }
        });
    }

    private void SetDefaultValue()
    {
        if (PlayerPrefs.GetInt("SetDefaultConfig", 0) == 0)
        {
            Dictionary<string, object> defaults = new Dictionary<string, object>(); //[!] Không sử dụng
            FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults);
            AppConfig.Instance.SetDefaultConfigValue();
        }
    }

    private int GetFetchValue(string valueName, int defaultValue)
    {
        string value = FirebaseRemoteConfig.DefaultInstance.GetValue(valueName).StringValue;
        if (value != null)
        {
            // Debug.Log(value);

            return int.Parse(value);
        }
        else
        {
            return defaultValue;
        }

    }

    private string GetFetStringValue(string valueName, string defaultValue)
    {
        //  Debug.LogError(valueName);
        string value = FirebaseRemoteConfig.DefaultInstance.GetValue(valueName).StringValue;
        if (value != null)
        {
            //  Debug.Log(value);
            return value;
        }
        else
        {
            //  Debug.LogError("you lost");
            return defaultValue;
        }
    }

    private bool GetFetBooleanValue(string valueName)
    {
        //  Debug.LogError(valueName);
        bool value = FirebaseRemoteConfig.DefaultInstance.GetValue(valueName).BooleanValue;

        return value;
    }

    private void FetchNotiContent()
    {
        //Fetch varible Noti event
        /*  AppConfig.Instance.CONTENT = GetFetStringValue("news_content", AppConfig.Instance.Content);
          AppConfig.Instance.ID_CONTENT = GetFetchValue("news_id", AppConfig.Instance.IdContent);
          PlayerData.Instance.IdEventCurrent = AppConfig.Instance.ID_CONTENT;
          AppConfig.Instance.REWARDS = GetFetStringValue("news_rewards", AppConfig.Instance.Rewards);
          AppConfig.Instance.TIMES = GetFetStringValue("news_time", AppConfig.Instance.Times);*/
    }
    #endregion
}