using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public GameObject settings,musicSlider,soundSlider,LeaderBoardObject,levelSelection, MultiLanguageSystem,testLevelSelection;
    public GameObject levelSelectionForTestCanvas;
    public GameObject ShopCanvas;
    private music musicScript;
    private Text coinCountText;

    public Sprite musicButtonOn,musicButtonOff,sliderOn,sliderOff;
    public Text languageText;

    private GameObject soundButton;

    private string[] languages = new string[] { "TURKCE", "ENGLISH" };
    private int currentLanguageIndex ;
    private AudioSource music;

    private AsyncOperation op;

    private bool musicOn, soundOn;
    private bool levelinstallingdone = false;

    public GameObject openingAnim;
    private openingAnim openingAnimScript;

    private bool playButtonPressed,tutorialButtonPressed;

    private void Awake()
    {
        openingAnimScript = openingAnim.GetComponent<openingAnim>();

        musicScript = GameObject.Find("MUSIC").GetComponent<music>();

        music = GameObject.Find("MUSIC").GetComponent<AudioSource>();
        soundButton = GameObject.Find("sound");
        PlayerPrefs.SetInt("rewardbasedvideo", 0);//olur da reklam izlerken oyundan atarsa

    }
    private void Start()
    {

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            //AdMobScript.instance._ShowBanner(); // oyuncu menü içerisinde ise banneri aktif et
        }
        int puan = PlayerPrefs.GetInt("coin_score");
        //Debug.Log("currentLanguageIndex: " + currentLanguageIndex);
        currentLanguageIndex = PlayerPrefs.GetInt("currentlanguageindex");
        //Debug.Log("currentLanguageIndex: " + currentLanguageIndex);

        languageText.text = languages[currentLanguageIndex];
        coinCountText = GameObject.Find("coinCount").GetComponent<Text>();

        coinCountText.text = puan.ToString();
        if(PlayerPrefs.GetInt("ismusicon")==1){
             if (!music.isPlaying){
                music.Play();
             }
            
        }
        
        playButtonPressed = false;
        tutorialButtonPressed = false;
        if (PlayerPrefs.GetInt("selectedLevel") == 0)
        {
            PlayerPrefs.SetInt("selectedLevel", 1);
        }
        //StartCoroutine(LoadScene());  
    }

    private void Update()
    {
        checkLanguageAndUpdate();
        if (playButtonPressed)
        {
            if (openingAnimScript.getOpeningAnimIsFinished())
            {
                if (PlayerPrefs.GetInt("istutorialplayed") == 1)
                {
                    string selectedLevel = PlayerPrefs.GetString("selectedlevelname");

                    SceneManager.LoadScene(selectedLevel);
                }
                else
                {
                    SceneManager.LoadScene("Tutorial");
                    PlayerPrefs.SetInt("istutorialplayed", 1);
                }
            }
        }
        if (tutorialButtonPressed)
        {
            if (openingAnimScript.getOpeningAnimIsFinished())
            {

                    SceneManager.LoadScene("Tutorial");
            }
        }

    }

    public void OpenSettings()
    {
        settings.SetActive(true);

        if (PlayerPrefs.GetInt("ismusicon") == 0)
        {
            musicSlider.GetComponent<Image>().sprite = sliderOff;
        }
        else
        {
            musicSlider.GetComponent<Image>().sprite = sliderOn;
        }

        if (PlayerPrefs.GetInt("issoundon") == 0)
        {
            soundSlider.GetComponent<Image>().sprite = sliderOff;
        }
        else
        {
            soundSlider.GetComponent<Image>().sprite = sliderOn;
        }
    }
    public void Exit()
    {
        settings.SetActive(false);
    }
    public void levelSelectionExit()
    {
        levelSelection.SetActive(false);
    }
    public void levelSelectionOpen()
    {
        levelSelection.SetActive(true);
    }
    public void twitter()
    {
        Application.OpenURL("https://twitter.com/EmazeBall");
    }
    public void facebook()
    {
        Application.OpenURL("https://www.facebook.com/emaze.ball.5");
    }
    public void instagram()
    {
        Application.OpenURL("https://www.instagram.com/emazeballglobal/");
    }

    public void _soundSlider() // Ayarlar menüsündeki ses açma kapamam slideri için.
    {
        if (soundSlider.GetComponent<Image>().sprite == sliderOn)
        {
            PlayerPrefs.SetInt("issoundon", 0);
            soundSlider.GetComponent<Image>().sprite = sliderOff;
        }
        else
        {
            PlayerPrefs.SetInt("issoundon", 1);
            soundSlider.GetComponent<Image>().sprite = sliderOn;
        }

    }

    public void _musicSlider() // Ayarlar menüsündeki müzik açma kapamam slideri için.
    {
        if(PlayerPrefs.GetInt("ismusicon")==1)
        {
            PlayerPrefs.SetInt("ismusicon", 0);
            musicSlider.GetComponent<Image>().sprite = sliderOff;
        }
        else
        {
            PlayerPrefs.SetInt("ismusicon", 1);
            musicSlider.GetComponent<Image>().sprite = sliderOn;
        }
        //////////
        if (music.isPlaying)
        {
            music.Pause();
        }
        else
        {
            music.Play();
        }
    }

    public void LeaderBoard()
    {
        LeaderBoardObject.SetActive(true);
    }
    public void Shop()
    {
        //ShopCanvas.SetActive(true);
        //ShopCanvas.GetComponent<Shop>()._isOpened();
        SceneManager.LoadScene("Market");
    }
    public void ShopExit()
    {
        ShopCanvas.SetActive(false);
    }
    public void ExitFromShop()
    {
        ShopCanvas.SetActive(false);
    }
    public void LeaderBoardExit()
    {
        LeaderBoardObject.SetActive(false);
    }

    public void level1()
    {
        if (!levelinstallingdone)
        {
            StartCoroutine(LoadScene());
        }
        
    }

    private void openLevelSelectionScreen()
    {
        levelSelectionOpen();
    }

    public void openTestLevelSelectionScreen()
    {
        testLevelSelection.SetActive(true);
    }

    public void playtheGame()
    {
        openingAnim.SetActive(true);

        playButtonPressed = true;
        
    }


    
    public void startTutorialLevel() // Tutorial butonuna basıldığında tutorial scenesine gider ve oradan tekrar main menu ekranına döner
    {
        openingAnim.SetActive(true);
        tutorialButtonPressed = true;
        PlayerPrefs.SetInt("istutorialbuttonpressed",1);
    }





    public void languageSolSec() // Dil seçme kısımı, dil seçimini sola kaydırır.
    {
        if (currentLanguageIndex >0)
        {
            currentLanguageIndex--;
            PlayerPrefs.SetInt("currentlanguageindex", currentLanguageIndex);
        }
    }
        public void languageSagSec() // Dil seçme kısımı, dil seçimini saga kaydırır.
    {
        if (currentLanguageIndex < languages.Length - 1)
        {
            //PlayerPrefs.SetInt("currentlanguageindex", currentLanguageIndex);
            currentLanguageIndex++;
            //languageText.text = languages[currentLanguageIndex];
            //LangSys.instanceLang.systemLanguage = "en";
            PlayerPrefs.SetInt("currentlanguageindex", currentLanguageIndex);
            //PlayerPrefs.SetString("currentlanguage", LangSys.instanceLang.systemLanguage);
        }
    }

    private void checkLanguageAndUpdate(){
            languageText.text = languages[currentLanguageIndex];
            //PlayerPrefs.SetInt("currentlanguageindex", currentLanguageIndex);
        if (currentLanguageIndex == 0){
            LangSys.instanceLang.systemLanguage = "tr";
            PlayerPrefs.SetString("currentlanguage", LangSys.instanceLang.systemLanguage);
        }else if (currentLanguageIndex == 1){
            LangSys.instanceLang.systemLanguage = "en";
            PlayerPrefs.SetString("currentlanguage", LangSys.instanceLang.systemLanguage);
        }

    }


    /*public void levelSelectionForTest() // Levelleri test etmek için, bu test versiyonunda tüm leveller açık
    {
        levelSelectionForTestCanvas.SetActive(true);
    }*/

    IEnumerator LoadScene()
    {
        op = SceneManager.LoadSceneAsync("Level"+PlayerPrefs.GetInt("selectedLevel"));
        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            if (op.progress >= 0.9f)
            {

                levelinstallingdone = true;

            }
            else
            {
                levelinstallingdone = false;
            }

            yield return null;
        }
    }
}

/*    public void _soundButton() // Ana menüdeki ses butonu için
    {
        if (PlayerPrefs.GetInt("ismusicon") == 1)
        {
            PlayerPrefs.SetInt("ismusicon", 0);
            music.Pause();
            soundButton.GetComponent<Image>().sprite = musicButtonOff;
            musicSlider.GetComponent<Image>().sprite = sliderOff;
        }
        else
        {
            PlayerPrefs.SetInt("ismusicon", 1);
            music.Play();
            soundButton.GetComponent<Image>().sprite = musicButtonOn;
            musicSlider.GetComponent<Image>().sprite = sliderOn;
        }
    }

    public void SaveButton() // Ayarlar menüsünde önceden save butonu vardı ama sonradan iptal ettik, bu metod kullanılmıyor.
    {
        if (music.isPlaying)
        {
            PlayerPrefs.SetInt("ismusicon", 1);
        }
        else
        {
            PlayerPrefs.SetInt("ismusicon", 0);
        }
    }

    private void setSliderOn_Off(GameObject slider, int stage)
    {
        if (stage == 0)
        {
            slider.GetComponent<Image>().sprite = sliderOff;
        }
        else
        {
            slider.GetComponent<Image>().sprite = sliderOn;
        }

    }

    */

    /*
        if (levelinstallingdone)
        {
            op.allowSceneActivation = true;
        }
        else
        {

        }
        */
