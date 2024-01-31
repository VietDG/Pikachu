using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class AdsManager : SingletonMonoBehaviour<AdsManager>
{
    public string SDK_key;
    public string banner_AdUnitId; // Retrieve the ID from your account
    public string Interstitial_AdUnitId;
    public string Rerwarded_AdUnitId;
    public string AppOpen_AdUnitId;

    private Action actVideoRewardedCallback, actVideoClosedCallback, actVideoClickedCallback;
    private Action acCloseInter;

    public static bool isShowBanner;

    private DateTime _lastTimeShowInter;
    private bool canShowInterAd => (int)(DateTime.Now - _lastTimeShowInter).TotalSeconds > AppConfig.Instance.AdFrequencyTime;

    private DateTime _lastTimeShowAppOpen;
    private bool canShowAppOpen => (int)(DateTime.Now - _lastTimeShowAppOpen).TotalSeconds > AppConfig.Instance.AdFrequencyTime;
    public DateTime LastTimeShowInter
    {
        set { _lastTimeShowInter = value; }
    }

    public override void Awake()
    {
        InitAds();
        EventAction.OnShowBanner += ShowBanner;
        EventAction.OnHideBanner += HideBanner;
    }

    public static float GetBannerHeight()
    {
        #region Don't Use
        if (MaxSdkUtils.IsTablet())
        {
            return Mathf.RoundToInt(90 * Screen.dpi / 160);
        }
        else
        {
            if (Screen.height <= 400 * Mathf.RoundToInt(Screen.dpi / 160))
            {
                return 32 * Mathf.RoundToInt(Screen.dpi / 160);
            }
            else if (Screen.height <= 720 * Mathf.RoundToInt(Screen.dpi / 160))
            {
                return 42 * Mathf.RoundToInt(Screen.dpi / 160);
            }
            else
            {
                return 50 * Mathf.RoundToInt(Screen.dpi / 160);
            }
        }
        #endregion
    }

    private void InitAds()
    {
        MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) =>
        {
            // AppLovin SDK is initialized, start loading ads
            MaxSdkCallbacks.AppOpen.OnAdHiddenEvent += OnAppOpenDismissedEvent;
        };

        _lastTimeShowInter = DateTime.Now.AddSeconds(0 - AppConfig.Instance.AdFrequencyTime);
        _lastTimeShowAppOpen = DateTime.Now.AddSeconds(0 - AppConfig.Instance.AdFrequencyTime);

        MaxSdk.SetSdkKey(SDK_key);
        MaxSdk.SetUserId("USER_ID");
        MaxSdk.InitializeSdk();

        InitializeBannerAds();
        InitializeInterstitialAds();
        InitializeRewardedAds();

        InitializeAd_impression();
    }

    public void OnAppOpenDismissedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        MaxSdk.LoadAppOpenAd(AppOpen_AdUnitId);
        //   PlayerData.Instance.CountTotalAdAppOpenClosed += 1;
    }

    #region InitBanner
    public void InitializeBannerAds()
    {
        // Banners are automatically sized to 320×50 on phones and 728×90 on tablets
        // You may call the utility method MaxSdkUtils.isTablet() to help with view sizing adjustments
        MaxSdk.CreateBanner(banner_AdUnitId, MaxSdk.BannerPosition.BottomCenter);

        // Set background or background color for banners to be fully functional
        // MaxSdk.SetBannerBackgroundColor(banner_AdUnitId, Color.black);

        MaxSdkCallbacks.Banner.OnAdLoadedEvent += OnBannerAdLoadedEvent;
        MaxSdkCallbacks.Banner.OnAdLoadFailedEvent += OnBannerAdLoadFailedEvent;
        MaxSdkCallbacks.Banner.OnAdClickedEvent += OnBannerAdClickedEvent;
        MaxSdkCallbacks.Banner.OnAdRevenuePaidEvent += OnBannerAdRevenuePaidEvent;
        MaxSdkCallbacks.Banner.OnAdExpandedEvent += OnBannerAdExpandedEvent;
        MaxSdkCallbacks.Banner.OnAdCollapsedEvent += OnBannerAdCollapsedEvent;
    }

    private void OnBannerAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }
    private void OnBannerAdLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo) { }
    private void OnBannerAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }
    private void OnBannerAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }
    private void OnBannerAdExpandedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }
    private void OnBannerAdCollapsedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }


    public void ShowBanner()
    {
        if (NetworkRequirement() /*&& PlayerData.Instance.IsNotRemoveAds*/ && AppConfig.Instance.CanShowBanner)
        {
            MaxSdk.ShowBanner(banner_AdUnitId);
            isShowBanner = true;
        }
    }

    private void HideBanner()
    {
        MaxSdk.HideBanner(banner_AdUnitId);
    }

    public static void SetPosWithBanner(RectTransform rectTransform)
    {
        if (!isShowBanner) return;
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y - GetBannerHeight());
    }

    #endregion

    private bool _showLoadingAd = false;
    public bool ShowLoadingAd
    {
        get { return _showLoadingAd; }
        set { _showLoadingAd = value; }
    }

    public void ShopAppOpen()
    {
        //if (!NetworkRequirement() || !PlayerData.Instance.IsNotRemoveAds || !AppConfig.Instance.CanShowInter || !canShowAppOpen) return;
        //if (MaxSdk.IsAppOpenAdReady(AppOpen_AdUnitId))
        //{
        //    Noti.Instance.ShowLoadingAd(() =>
        //    {
        //        MaxSdk.ShowAppOpenAd(AppOpen_AdUnitId);
        //    });
        //    //   PlayerData.Instance.CountTotalAdAppOpenShown += 1;
        //    _lastTimeShowAppOpen = DateTime.Now;
        //}
        //else
        //{
        //    MaxSdk.LoadAppOpenAd(AppOpen_AdUnitId);
        //}
    }

    #region InitInter
    public void InitializeInterstitialAds()
    {
        // Attach callback
        MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += OnInterstitialLoadedEvent;
        MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += OnInterstitialLoadFailedEvent;
        MaxSdkCallbacks.Interstitial.OnAdDisplayedEvent += OnInterstitialDisplayedEvent;
        MaxSdkCallbacks.Interstitial.OnAdClickedEvent += OnInterstitialClickedEvent;
        MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnInterstitialHiddenEvent;
        MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent += OnInterstitialAdFailedToDisplayEvent;

        // Load the first interstitial
        LoadInterstitial();
    }

    private void LoadInterstitial()
    {
        MaxSdk.LoadInterstitial(Interstitial_AdUnitId);
    }

    private void OnInterstitialLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad is ready for you to show. MaxSdk.IsInterstitialReady(adUnitId) now returns 'true'
    }

    private void OnInterstitialLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        // Interstitial ad failed to load
        LoadInterstitial();
        // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds)
    }

    private void OnInterstitialDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        //PlayerData.Instance.CountTotalAdInterShown += 1;
        LastTimeShowInter = DateTime.Now;
    }

    private void OnInterstitialAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad failed to display. AppLovin recommends that you load the next ad.
        LoadInterstitial();
    }

    private void OnInterstitialClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        //  PlayerData.Instance.CountTotalAdInterClicked += 1;
    }

    private void OnInterstitialHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        acCloseInter?.Invoke();
        acCloseInter = null;
        LastTimeShowInter = DateTime.Now;
        // Interstitial ad is hidden. Pre-load the next ad.
        LoadInterstitial();
    }

    public void ShowInterstitial(Action Close_CallBack = null, bool forceShow = false)
    {
        if (ShowLoadingAd) return;
        if (!forceShow && (!NetworkRequirement() || /*!PlayerData.Instance.IsNotRemoveAds ||*/ !AppConfig.Instance.CanShowInter /*|| !canShowInterAd*/))
        {
            Close_CallBack?.Invoke();
            return;
        }
        ShowLoadingAd = true;
        CheckIntertitialAdConditionAndShow(Close_CallBack);
    }

    public void CheckIntertitialAdConditionAndShow(Action closeCallback)
    {
        if (ShowInter(() =>
        {
            // When close ads
            closeCallback?.Invoke();
            ShowLoadingAd = false;
            _lastTimeShowInter = DateTime.Now;
        }))
        {
            // When show inter ads
        }
        else
        {
            closeCallback?.Invoke();
            ShowLoadingAd = false;
        }
    }

    public bool ShowInter(Action closeCallback)
    {
        acCloseInter = closeCallback;
        if (MaxSdk.IsInterstitialReady(Interstitial_AdUnitId))
        {
            //GlobalEventManager.Instance.AdLocation = adLocation;
            //Noti.Instance.ShowLoadingAd(() =>
            //{
            MaxSdk.ShowInterstitial(Interstitial_AdUnitId);
            // });
            return true;
        }
        else
        {
            LoadInterstitial();
            return false;
        }
    }
    #endregion

    #region Init Rewarded
    public void InitializeRewardedAds()
    {
        // Attach callback
        MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += OnRewardedAdLoadedEvent;
        MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += OnRewardedAdLoadFailedEvent;
        MaxSdkCallbacks.Rewarded.OnAdDisplayedEvent += OnRewardedAdDisplayedEvent;
        MaxSdkCallbacks.Rewarded.OnAdClickedEvent += OnRewardedAdClickedEvent;
        MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += OnRewardedAdRevenuePaidEvent;
        MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += OnRewardedAdHiddenEvent;
        MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += OnRewardedAdFailedToDisplayEvent;
        MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;
        LoadRewardedAd();
    }

    private void LoadRewardedAd()
    {
        MaxSdk.LoadRewardedAd(Rerwarded_AdUnitId);
    }

    private void OnRewardedAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad is ready for you to show. MaxSdk.IsRewardedAdReady(adUnitId) now returns 'true'.
    }

    private void OnRewardedAdLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        // Rewarded ad failed to load 
        // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds).
    }

    private void OnRewardedAdDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        //Debug.LogError($"OnRewardedAdDisplayedEvent");
        //  PlayerData.Instance.CountTotalAdVideoShown += 1;
        _lastTimeShowAppOpen = DateTime.Now;
    }

    private void OnRewardedAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad failed to display. AppLovin recommends that you load the next ad.
        LoadRewardedAd();
    }
    private void OnRewardedAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        //Debug.LogError($"OnRewardedAdClickedEvent");
        actVideoClickedCallback?.Invoke();
        actVideoClickedCallback = null;
        //   PlayerData.Instance.CountTotalAdVideoClicked += 1;
    }

    private void OnRewardedAdHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad is hidden. Pre-load the next ad
        actVideoClosedCallback?.Invoke();
        actVideoClosedCallback = null;
        _lastTimeShowAppOpen = DateTime.Now;
        LoadRewardedAd();
    }

    private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward, MaxSdkBase.AdInfo adInfo)
    {
        // The rewarded ad displayed and the user should receive the reward.
        actVideoRewardedCallback?.Invoke();
        actVideoRewardedCallback = null;
        //  PlayerData.Instance.CountTotalAdVideoCompleted += 1;
    }

    private void OnRewardedAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Ad revenue paid. Use this callback to track user revenue.
    }

    public void ShowRewardedAd(Action rewardedCallback, Action closedCallback, Action clickedCallback = null)
    {
        if (!NetworkRequirement())
        {
            //    Noti.Instance.ShowNoti(GameLanguage.Get(Const.KEY_NO_INTERNET));
            return;
        }

        if (!MaxSdk.IsRewardedAdReady(Rerwarded_AdUnitId))
        {
            //  Noti.Instance.ShowNoti(GameLanguage.Get(Const.KEY_NO_AD));
            LoadRewardedAd();
            return;
        }

        if (MaxSdk.IsRewardedAdReady(Rerwarded_AdUnitId))
        {
            actVideoRewardedCallback = rewardedCallback;
            actVideoClosedCallback = closedCallback;
            actVideoClickedCallback = clickedCallback;
            //    GlobalEventManager.Instance.AdLocation = adLocation;
            MaxSdk.ShowRewardedAd(Rerwarded_AdUnitId);
        }
        else
        {
            LoadRewardedAd();
        }
    }

    public static bool NetworkRequirement()
    {
        return Application.internetReachability != NetworkReachability.NotReachable;
    }

    //public void CheckConnectionInternet(Action<bool> callBack)
    //{
    //    StartCoroutine(WaitCheckConnectInternet(callBack));
    //}

    private IEnumerator WaitCheckConnectInternet(Action<bool> callBack)
    {
        UnityWebRequest www = UnityWebRequest.Get("https://www.google.com/");
        if (www != null)
        {
            yield return www.SendWebRequest();

            if (!string.IsNullOrEmpty(www.error) || www.result == UnityWebRequest.Result.ProtocolError)
            {
                callBack?.Invoke(false);
                Debug.Log("Form upload Error!");
            }
            else
            {
                callBack?.Invoke(true);
                Debug.Log("Form upload complete!");
            }
        }
        else
        {
            callBack?.Invoke(false);
            Debug.Log("UnityWebRequest was null");
        }

    }
    #endregion

    public void InitializeAd_impression()
    {
        MaxSdkCallbacks.Interstitial.OnAdRevenuePaidEvent += OnAdRevenuePaidEvent;
        MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += OnAdRevenuePaidEvent;
        MaxSdkCallbacks.Banner.OnAdRevenuePaidEvent += OnAdRevenuePaidEvent;
        MaxSdkCallbacks.MRec.OnAdRevenuePaidEvent += OnAdRevenuePaidEvent;
    }

    private void OnAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo impressionData)
    {
        if (impressionData != null)
        {
            PlayerPrefs.SetFloat("Total_Revenue", PlayerPrefs.GetFloat("Total_Revenue", 0) + (float)impressionData.Revenue);

            //Event
            Firebase.Analytics.Parameter[] AdParameters = {
                new Firebase.Analytics.Parameter("countryCode", MaxSdk.GetSdkConfiguration().CountryCode),
                new Firebase.Analytics.Parameter("ad_unit",impressionData.AdUnitIdentifier),
                new Firebase.Analytics.Parameter("ad_format",impressionData.AdFormat),
                new Firebase.Analytics.Parameter("ad_network",impressionData.NetworkName),
                new Firebase.Analytics.Parameter("placement", impressionData.Placement),
                new Firebase.Analytics.Parameter("networkPlacement", impressionData.NetworkPlacement),
                new Firebase.Analytics.Parameter("currency","usd"),
                new Firebase.Analytics.Parameter("value", (double)impressionData.Revenue)
            };
            Firebase.Analytics.FirebaseAnalytics.LogEvent("paid_ad_impression", AdParameters);
        }
    }

    private void OnDestroy()
    {
        //Banner
        MaxSdkCallbacks.Banner.OnAdLoadedEvent -= OnBannerAdLoadedEvent;
        MaxSdkCallbacks.Banner.OnAdLoadFailedEvent -= OnBannerAdLoadFailedEvent;
        MaxSdkCallbacks.Banner.OnAdClickedEvent -= OnBannerAdClickedEvent;
        MaxSdkCallbacks.Banner.OnAdRevenuePaidEvent -= OnBannerAdRevenuePaidEvent;
        MaxSdkCallbacks.Banner.OnAdExpandedEvent -= OnBannerAdExpandedEvent;
        MaxSdkCallbacks.Banner.OnAdCollapsedEvent -= OnBannerAdCollapsedEvent;

        //Inter
        MaxSdkCallbacks.Interstitial.OnAdLoadedEvent -= OnInterstitialLoadedEvent;
        MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent -= OnInterstitialLoadFailedEvent;
        MaxSdkCallbacks.Interstitial.OnAdDisplayedEvent -= OnInterstitialDisplayedEvent;
        MaxSdkCallbacks.Interstitial.OnAdClickedEvent -= OnInterstitialClickedEvent;
        MaxSdkCallbacks.Interstitial.OnAdHiddenEvent -= OnInterstitialHiddenEvent;
        MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent -= OnInterstitialAdFailedToDisplayEvent;

        //Reward
        MaxSdkCallbacks.Rewarded.OnAdLoadedEvent -= OnRewardedAdLoadedEvent;
        MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent -= OnRewardedAdLoadFailedEvent;
        MaxSdkCallbacks.Rewarded.OnAdDisplayedEvent -= OnRewardedAdDisplayedEvent;
        MaxSdkCallbacks.Rewarded.OnAdClickedEvent -= OnRewardedAdClickedEvent;
        MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent -= OnRewardedAdRevenuePaidEvent;
        MaxSdkCallbacks.Rewarded.OnAdHiddenEvent -= OnRewardedAdHiddenEvent;
        MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent -= OnRewardedAdFailedToDisplayEvent;
        MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent -= OnRewardedAdReceivedRewardEvent;

        //impression
        MaxSdkCallbacks.Interstitial.OnAdRevenuePaidEvent -= OnAdRevenuePaidEvent;
        MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent -= OnAdRevenuePaidEvent;
        MaxSdkCallbacks.Banner.OnAdRevenuePaidEvent -= OnAdRevenuePaidEvent;
        MaxSdkCallbacks.MRec.OnAdRevenuePaidEvent -= OnAdRevenuePaidEvent;

        //Banner
        EventAction.OnShowBanner -= ShowBanner;
        EventAction.OnHideBanner -= HideBanner;
    }
}

public class AdsEvent
{
    public delegate void NoParamEvent();

    public delegate void BoolEvent(bool result);

    public delegate void OneParamsEvent(object param1);

    public delegate void TwoParamsEvent(object param1, object param2);
}
