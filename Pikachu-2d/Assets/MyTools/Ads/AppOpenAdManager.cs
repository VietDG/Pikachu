//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using GoogleMobileAds.Api;
//using System;
//public class AppOpenAdManager
//{
//#if UNITY_ANDROID
//    public string AD_UNIT_ID = "ca-app-pub-6256074067607531/3067810957";
//#elif UNITY_IOS
//    private const string AD_UNIT_ID = "ca-app-pub-3940256099942544/5662855259";
//#else
//    private const string AD_UNIT_ID = "unexpected_platform";
//#endif

//    private static AppOpenAdManager instance;

//    private AppOpenAd ad;

//    public bool isShowingAd = false;

//    public static AppOpenAdManager Instance
//    {
//        get
//        {
//            if (instance == null)
//            {
//                instance = new AppOpenAdManager();
//            }

//            return instance;
//        }
//    }

//    public bool IsAdAvailable
//    {
//        get
//        {
//            return ad != null;
//        }
//    }

//    // Start is called before the first frame update
//    void Start()
//    {
//        //MobileAds.Initialize(initStatus => { });
//        //LoadAd();
//    }

//    public void LoadAd()
//    {
//        // We will implement this below.
//        AdRequest request = new AdRequest.Builder().Build();

//        // Load an app open ad for portrait orientation
//        AppOpenAd.LoadAd(AD_UNIT_ID, ScreenOrientation.Portrait, request, ((appOpenAd, error) =>
//        {
//            if (error != null)
//            {
//                // Handle the error.
//                Debug.LogFormat("Failed to load the ad. (reason: {0})", error.LoadAdError.GetMessage());
//                return;
//            }

//            // App open ad is loaded.
//            ad = appOpenAd;
//        }));

//    }

//    public void ShowAdIfAvailable()
//    {
//        if (!IsAdAvailable || isShowingAd || !AdsManager.NetworkRequirement() || !AdsManager.Instance.canShowOpenAd || !PlayerData.Instance.CanShowAds)
//        {
//           // Debug.LogError(IsAdAvailable);
//            return;
//        }

//        ad.OnAdDidDismissFullScreenContent += HandleAdDidDismissFullScreenContent;
//        ad.OnAdFailedToPresentFullScreenContent += HandleAdFailedToPresentFullScreenContent;
//        ad.OnAdDidPresentFullScreenContent += HandleAdDidPresentFullScreenContent;
//        ad.OnAdDidRecordImpression += HandleAdDidRecordImpression;
//        ad.OnPaidEvent += HandlePaidEvent;
//        //Debug.LogError(isShowingAd);
//        ad.Show();
//    }


//    private void HandleAdDidDismissFullScreenContent(object sender, EventArgs args)
//    {
//        Debug.Log("Closed app open ad");
//        // Set the ad to null to indicate that AppOpenAdManager no longer has another ad to show.
//        ad = null;
//        isShowingAd = false;
//        LoadAd();
//    }

//    private void HandleAdFailedToPresentFullScreenContent(object sender, AdErrorEventArgs args)
//    {
//        Debug.LogFormat("Failed to present the ad (reason: {0})", args.AdError.GetMessage());
//        // Set the ad to null to indicate that AppOpenAdManager no longer has another ad to show.
//        ad = null;
//        LoadAd();
//    }

//    private void HandleAdDidPresentFullScreenContent(object sender, EventArgs args)
//    {
//        Debug.Log("Displayed app open ad");
//        isShowingAd = true;
//    }

//    private void HandleAdDidRecordImpression(object sender, EventArgs args)
//    {
//        Debug.Log("Recorded ad impression");
//    }

//    private void HandlePaidEvent(object sender, AdValueEventArgs args)
//    {
//        Debug.LogFormat("Received paid event. (currency: {0}, value: {1}",
//                args.AdValue.CurrencyCode, args.AdValue.Value);
//    }

//    //public void OnApplicationPause(bool paused)
//    //{
//    //    // Display the app open ad when the app is foregrounded
//    //    if (!paused)
//    //    {
//    //       ShowAdIfAvailable();
//    //        Debug.LogError("CC");

//    //    }
//    //}

//    //public void Show()
//    //{
//    //    ShowAdIfAvailable();
//    //}
//}


  

    

