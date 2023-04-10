using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// main game canvas scripti
public class gameloopCanvas : MonoBehaviour
{
    public GameObject pauseCanvas;
    public static bool isPaused = false;

    private gameMotor _motor;
    private List<string> levelNames;
    private int _count = 0;
    private  loveBar _loveBar;

    private void Awake()
    {
       // hideBanners();
    
    }

    private void Start()
    {
        levelNames = new List<string>();
        _motor = GameObject.Find("GameMotor").GetComponent<gameMotor>();
        _loveBar = GameObject.Find("loveBar").GetComponent<loveBar>();

    }

    public void pause()
    {
        pauseCanvas.SetActive(true);
        if (!_motor.gamePaused)
        {
            _motor.gamePaused = true;
            GameObject.Find("player").GetComponent<player>().timerIsRunning = false;
        }
        else
        {
            _motor.gamePaused = false;

        }
             
         _loveBar.enabled = false;

    }

    private void hideBanners()
     {

         int sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
         string[] scenes = new string[sceneCount];

         for(int i = 0; i < sceneCount; i++)
         {
             scenes[i] = System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i));

             levelNames.Add(scenes[i]);
         }

         foreach(string str in levelNames)
         {
             if(str == "Level" + _count)
             {
                 AdMobScript.instance._HideBanner();
                 AdMobScript.instance._DestroyBanner();
                 AdMobScript.instance._RequestBanner();

                 AdMobScript.instance.RequestInterstitial();
                 AdMobScript.instance.RequestRewardBasedVideo();
                 _count++;
             }
             else if(str == "Tutorial")
             {
                 AdMobScript.instance._HideBanner();
                 AdMobScript.instance._DestroyBanner();
                 AdMobScript.instance._RequestBanner();
             }
         }

         
         if (SceneManager.GetActiveScene().name == "MainMenu")
         {
             AdMobScript.instance._ShowBanner(); // oyuncu menü içerisinde ise banneri aktif et
         }

     }
}
