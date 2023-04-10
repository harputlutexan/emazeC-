using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class selectLevelCanvas : MonoBehaviour
{
    public Text score;
    public GameObject star1, star2, star3;
    private int _score;
    private bool oneTime;
    private GameObject player;
    private int extraCoinScore = 0;
    private music msc;
    private player playerScript;



    void Start()
    {
        oneTime = true;
        //_score  = PlayerPrefs.GetInt("coin_score");
        string currentSceneName = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetInt("score_"+currentSceneName,_score);
        PlayerPrefs.SetInt("nextLevel",2);
        score.text = _score.ToString();
         msc = (music)FindObjectOfType(typeof(music));
        playerScript = GameObject.Find("player").GetComponent<player>();

    }

    private void Update()
    {
        if (oneTime)
        
        {
            int currentScoreboardCoinScore= PlayerPrefs.GetInt("scoreboard_coin_score_" + SceneManager.GetActiveScene().name);
            _score = PlayerPrefs.GetInt("coin_score") +  extraCoinScore;
            PlayerPrefs.SetInt("coin_score", _score);
            score.text = (PlayerPrefs.GetInt("coin_score_" + SceneManager.GetActiveScene().name)+extraCoinScore).ToString();
            int currentLevelScore = PlayerPrefs.GetInt("coin_score_" + SceneManager.GetActiveScene().name) + extraCoinScore;
            if (currentLevelScore>=currentScoreboardCoinScore){
                PlayerPrefs.SetInt("scoreboard_coin_score_" + SceneManager.GetActiveScene().name, currentLevelScore);
            }
            PlayerPrefs.SetInt("coin_score_" + SceneManager.GetActiveScene().name, currentLevelScore);
            oneTime = false;
            //Debug.Log("currentScoreboardCoinScore: " + currentScoreboardCoinScore);
            //Debug.Log("currentLevelScore: " + currentLevelScore);

        }
    }

    public void _exit()
    {
        gameObject.SetActive(false);
    }

    public void _restartLevel()
    {
        playerScript.timerIsRunning = false;

        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
        else
        {
            if (msc.isAudioPlaying)
            {
                msc.aud.Pause();
            }
            AdMobScript.instance.goToMainMenu = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            AdMobScript.instance.ShowInterstitialAd();
        }
        
    }
    public void _nextLevel()
    {
        int nextLevelIndis = PlayerPrefs.GetInt("nextlevelindis");
        playerScript.timerIsRunning = false;

        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            SceneManager.LoadScene(nextLevelIndis);

        }
        else
        {
            if (msc.isAudioPlaying)
            {
                msc.aud.Pause();
            }
            SceneManager.LoadScene(nextLevelIndis);
            AdMobScript.instance.goToMainMenu = false;
            AdMobScript.instance.ShowInterstitialAd();
        }

        
    }
    public void _mainMenu()
    {

        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            SceneManager.LoadScene("MainMenu");
        }
            if (msc.isAudioPlaying)
            {
                msc.aud.Pause();
            }
        AdMobScript.instance.goToMainMenu = true;
        AdMobScript.instance.ShowInterstitialAd();
        //SceneManager.LoadScene("MainMenu");
    }

    public void ShowTheScore()
    {
        score.text = _score.ToString();
    }

    public void setScore()
    {
         int currentLevelStar = PlayerPrefs.GetInt("score_star_" + SceneManager.GetActiveScene().name);
        player = GameObject.Find("player");
        switch (player.GetComponent<player>().health)
        {
            case 1:
                star1.SetActive(true);
                extraCoinScore = 10;
                GameObject.Find("textAnim").GetComponent<textAnimScript>().coinCount = extraCoinScore;
                GameObject.Find("textAnim").GetComponent<textAnimScript>().starCount = 1;
                if (currentLevelStar<1){
                    PlayerPrefs.SetInt("score_star_" + SceneManager.GetActiveScene().name, 1);
                }                            
                break;
            case 2:
                star1.SetActive(true);
                star2.SetActive(true);
                extraCoinScore = 20;
                GameObject.Find("textAnim").GetComponent<textAnimScript>().coinCount = extraCoinScore;
                GameObject.Find("textAnim").GetComponent<textAnimScript>().starCount = 2;
                if (currentLevelStar<2){
                    PlayerPrefs.SetInt("score_star_" + SceneManager.GetActiveScene().name, 2);
                }
                break;
            case 3:
                star1.SetActive(true);
                star2.SetActive(true);
                extraCoinScore = 20;
                GameObject.Find("textAnim").GetComponent<textAnimScript>().coinCount = extraCoinScore;
                GameObject.Find("textAnim").GetComponent<textAnimScript>().starCount = 2;
                if (currentLevelStar<2){
                    PlayerPrefs.SetInt("score_star_" + SceneManager.GetActiveScene().name, 2);
                }
                break;
            case 4:
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
                extraCoinScore = 30;
                GameObject.Find("textAnim").GetComponent<textAnimScript>().coinCount = extraCoinScore;
                GameObject.Find("textAnim").GetComponent<textAnimScript>().starCount = 3;
                if (currentLevelStar<3){
                    PlayerPrefs.SetInt("score_star_" + SceneManager.GetActiveScene().name, 3);
                }
                break;
            case 5:
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
                extraCoinScore = 30;
                GameObject.Find("textAnim").GetComponent<textAnimScript>().coinCount = extraCoinScore;
                GameObject.Find("textAnim").GetComponent<textAnimScript>().starCount = 3;
                if (currentLevelStar<3){
                    PlayerPrefs.SetInt("score_star_" + SceneManager.GetActiveScene().name, 3);
                }
                break;
        }
    }
}
