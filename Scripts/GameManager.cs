using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 60;
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            //AdMobScript.instance._ShowBanner(); // oyuncu menü içerisinde ise banneri aktif et
        }
        else if(SceneManager.GetActiveScene().name == "Tutorial")
        {

        }
        else
        {
            for(int a = 1; a <= 40; a++) // oyuncu her hangi bir levelin içerisinde ise banner reklamını gizle, interstitial ve rewardbased video için reklamları hafızaya yükle
            {
                if(SceneManager.GetActiveScene().name == "Level" + a)
                {
                    //AdMobScript.instance._HideBanner();
                    //AdMobScript.instance._DestroyBanner();
                    //AdMobScript.instance._RequestBanner();
                    /*if (!AdMobScript.instance){
                        AdMobScript.instance.RequestInterstitial();
                        AdMobScript.instance.RequestRewardedAd();
                    }*/
                    
                }
            }
        }
    }
}
