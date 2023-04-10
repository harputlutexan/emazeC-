using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GiveUp : MonoBehaviour
{
    private GameObject gameCanvas,MainMenu;
    private float timeCounter,temporaryCounter;
    private Text scoreText;

    private int seconds;
    private gameMotor _motor;
    private bool interstitialAdsLoaded=false;
    private bool connection;
    private music msc;
    private AudioSource music;
    private int interstialCounter=0;
    private player playerScript;



    private void Awake()
    {
        _motor = GameObject.Find("GameMotor").GetComponent<gameMotor>();
        gameCanvas = GameObject.Find("MainGameCanvas");
        scoreText = GameObject.Find("scoreCounter").GetComponent<Text>();
        playerScript = GameObject.Find("player").GetComponent<player>();



        //PlayerPrefs.SetInt("interstitialad", 0);
    }
    void Start()
    {

       timeCounter = 10f;
        temporaryCounter = timeCounter + 1f;
        timeCounter += 1;
        scoreText.text = timeCounter.ToString();
        msc = (music)FindObjectOfType(typeof(music));
        //_motor.gamePaused = true;
    }

    private void Update()
    {
        if(timeCounter <= 0)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            timeCounter -= Time.deltaTime;
            scoreText.text = ((int)timeCounter).ToString();
        }

    }

    public void exitButton()
    {
        gameObject.SetActive(false);
        gameCanvas.SetActive(true);
        timeCounter = temporaryCounter;
        //_motor.gamePaused = false;
    }
    public void noButton()
    {
        gameObject.SetActive(false);
        gameCanvas.SetActive(true);
    }
    public void yesButton()
    {
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
                SceneManager.LoadScene("MainMenu");
        }
        else
        {
            
            interstialCounter = PlayerPrefs.GetInt("interstialCounter");
                
            if (interstialCounter < 1)
            {
                interstialCounter++;
                PlayerPrefs.SetInt("interstialCounter",interstialCounter);
                SceneManager.LoadScene("MainMenu");
            }
            else
            {
                       if (msc.isAudioPlaying)
            {
                msc.aud.Pause();
            }
                interstialCounter = 0;
                PlayerPrefs.SetInt("interstialCounter",interstialCounter);
                AdMobScript.instance.goToMainMenu = true;
                AdMobScript.instance.ShowInterstitialAd();                
                SceneManager.LoadScene("MainMenu");
                
            }
            
        }
    }

    public void tryAgain()
    {
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            GameObject.Find("player").GetComponent<player>().reklamIzlendi();
        }
        else
        {

            if (Internet())
            {
                if (msc.isAudioPlaying)
                {
                    msc.aud.Pause();
                }
                playerScript.timerIsRunning = false;
                AdMobScript.instance.ShowRewardBasedVideo();
            }
            else
            {
                SceneManager.LoadScene("MainMenu");
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

    private bool Internet()
    {
        
        switch (Application.internetReachability)
        {
            case NetworkReachability.ReachableViaLocalAreaNetwork:
                connection = true;
                break;
            case NetworkReachability.ReachableViaCarrierDataNetwork:
                //internetPossiblyAvailable = allowCarrierDataNetwork;
                connection = true;
                break;
            default:
                connection = false;
                break;
        }
        if (!connection)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void OnDisable()
    {
        timeCounter = 10f;
        temporaryCounter = timeCounter + 1f;
        timeCounter += 1;
        scoreText.text = timeCounter.ToString();

        //AdMobScript.instance.DestroyInterstitialAd();

        AdMobScript.instance.RequestInterstitial();
        AdMobScript.instance.RequestRewardBasedVideo();
    }

}
