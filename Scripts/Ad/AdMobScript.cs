using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Common;
using System;

public class AdMobScript : MonoBehaviour
{
    public static AdMobScript instance;


    //testler bitince
    
     #if UNITY_ANDROID
    string App_ID = "ca-app-pub-7568605238512922~7459565699";
    string Banner_Ad_ID = "ca-app-pub-7568605238512922/4229060831";
    string Revarded_Ad_ID = "ca-app-pub-7568605238512922/9289815823";
    string Interstitial_Ad_ID = "ca-app-pub-7568605238512922/2915979165";
    #elif UNITY_IPHONE
    string App_ID = "ca-app-pub-7568605238512922~6470577768";
    string Banner_Ad_ID = "ca-app-pub-7568605238512922/3767079725";
    string Revarded_Ad_ID = "ca-app-pub-7568605238512922/5668631989";
    string Interstitial_Ad_ID = "ca-app-pub-7568605238512922/2537758532";
    #endif
    
    /*
    #if UNITY_ANDROID
    string App_ID = "ca-app-pub-7568605238512922~7459565699";
    string Banner_Ad_ID = "ca-app-pub-3940256099942544/6300978111";
    //string Revarded_Ad_ID = "ca-app-pub-7568605238512922/9289815823";
    string Revarded_Ad_ID = "ca-app-pub-3940256099942544/5224354917";
    string Interstitial_Ad_ID = "ca-app-pub-3940256099942544/1033173712";
    #elif UNITY_IPHONE
    string App_ID = "ca-app-pub-7568605238512922~6470577768";
    string Banner_Ad_ID = "ca-app-pub-3940256099942544/2934735716";
    string Revarded_Ad_ID = "ca-app-pub-3940256099942544/1712485313";
    string Interstitial_Ad_ID = "ca-app-pub-3940256099942544/4411468910";
     #endif*/

    private BannerView bannerView;
    private RewardBasedVideoAd rewardBasedVideoAd;
    public InterstitialAd interstitial;
    private RewardedAd rewardedAd;
    private AdSize adSize;
    private player playerScript;
    public bool goToMainMenu;
    public bool adRewarded;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Logo")
        {
            MobileAds.Initialize(initStatus => { });
            adSize = new AdSize(320, 50);

            //_RequestBanner();
        }
        //this.rewardedAd = new RewardedAd(Revarded_Ad_ID);

        /*
        // Called when an ad request has successfully loaded.
        rewardBasedVideoAd.OnAdLoaded += HandleRewardBasedVideoLoaded;
        // Called when an ad request failed to load.
        rewardBasedVideoAd.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        // Called when an ad is shown.
        rewardBasedVideoAd.OnAdOpening += HandleRewardBasedVideoOpened;
        // Called when the ad starts to play.
        rewardBasedVideoAd.OnAdStarted += HandleRewardBasedVideoStarted;
        // Called when the user should be rewarded for watching a video.
        rewardBasedVideoAd.OnAdRewarded += HandleRewardBasedVideoRewarded;
        // Called when the ad is closed.
        rewardBasedVideoAd.OnAdClosed += HandleRewardBasedVideoClosed;
        // Called when the ad click caused the user to leave the application.
        rewardBasedVideoAd.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;
        */

    }

 

    

    public void RequestInterstitial()
    {

                // Clean up interstitial ad before creating a new one.
        if (this.interstitial != null)
        {
            this.interstitial.Destroy();
        }

        this.interstitial = new InterstitialAd(Interstitial_Ad_ID);

        // Called when an ad request has successfully loaded.
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;
        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);
    }


    public void ShowInterstitialAd()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
            if (goToMainMenu)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
        else
        {
            if (goToMainMenu)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }

    public void DestroyInterstitialAd()
    {
        interstitial.Destroy();
    }

  

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
       // MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
       // MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "+ args.Message);
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
       // MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        // MonoBehaviour.print("HandleAdClosed event received");

    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
       // MonoBehaviour.print("HandleAdLeavingApplication event received");
    }

    
    
    //adidini degistirmedim ama artik rewarded ad yuklyecek
    public void RequestRewardBasedVideo()
    {

        this.rewardedAd = new RewardedAd(Revarded_Ad_ID);
         // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;
        //this.rewardBasedVideoAd = new RewardBasedVideoAd();
        AdRequest request = new AdRequest.Builder().Build();
        this.rewardedAd.LoadAd(request);
        //this.rewardBasedVideoAd.LoadAd(request, Revarded_Ad_ID);
    }

    //adidini degistirmedim ama artik rewarded ad yuklyecek
    public void ShowRewardBasedVideo()
    {


         if (this.rewardedAd.IsLoaded()) {
              this.rewardedAd.Show();
            }else{
            SceneManager.LoadScene("MainMenu");
        }

        /*    
        if (rewardBasedVideoAd.IsLoaded())
        {
            rewardBasedVideoAd.Show();
            
        }else{
            SceneManager.LoadScene("MainMenu");
        }*/
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
        adRewarded = false;

    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");
         this.RequestRewardBasedVideo();
         if (!adRewarded){
          SceneManager.LoadScene("MainMenu"); 
         }

    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);
        adRewarded = true;
        GameObject.Find("player").GetComponent<player>().reklamIzlendi();
    }

    public void _RequestBanner()
    {
        this.bannerView = new BannerView(Banner_Ad_ID, AdSize.Banner, AdPosition.Top);
        AdRequest requestForBanner = new AdRequest.Builder().Build();
        this.bannerView.LoadAd(requestForBanner);
        this.bannerView.Hide();
    }

    public void _ShowBanner()
    {
        this.bannerView.Show();
    }

    public void _HideBanner()
    {
        this.bannerView.Hide();
    }

    public void _DestroyBanner()
    {
        this.bannerView.Destroy();
    }


}


/*public void ShowRewardedVideo()
    {

        if (rewardedAd.IsLoaded())
        {
           
            rewardedAd.Show();
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
    */

 /* public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        PlayerPrefs.SetInt("rewardbasedvideo", 0);
        MonoBehaviour.print(
            "HandleRewardBasedVideoFailedToLoad event received with message: "
                             + args.Message);

    }

    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoOpened event received");

    }

    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
        this.RequestRewardBasedVideo();
       
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
       string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardBasedVideoRewarded event received for "
                        + amount.ToString() + " " + type);
        GameObject.Find("player").GetComponent<player>().reklamIzlendi();

        //adRewarded = true;
        //PlayerPrefs.SetInt("rewardbasedvideo", 1);
        //loveBar LB = GameObject.Find("loveBar").GetComponent<loveBar>();
        //LB.timer = LB.setTimeForLoveBar;
        //LB.barIMG.fillAmount = 1f;
    }

    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
    }
    */
    


  
