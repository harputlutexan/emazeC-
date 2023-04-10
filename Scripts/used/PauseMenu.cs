using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject soundButton,musicObject;
    public Sprite musicButtonOn, musicButtonOff;

    private AudioSource music;
    private music msc;
    private gameMotor mtr;
    private  loveBar _loveBar;

    void Start()
    {
        msc = (music)FindObjectOfType(typeof(music));
        mtr = (gameMotor)FindObjectOfType(typeof(gameMotor));
        _loveBar = GameObject.Find("loveBar").GetComponent<loveBar>();


        if (PlayerPrefs.GetInt("ismusicon") == 1)
        {
            soundButton.GetComponent<Image>().sprite = musicButtonOn;
        }
        else
        {
            soundButton.GetComponent<Image>().sprite = musicButtonOff;
        }

    }

    void Update()
    {
        
    }

    public void _soundButton()
    {
        if (msc.isAudioPlaying)
        {
            msc.aud.Pause();
            soundButton.GetComponent<Image>().sprite = musicButtonOff;
            msc.isSoundsDisabled = true;
            PlayerPrefs.SetInt("issoundon", 0);
            PlayerPrefs.SetInt("ismusicon", 0);
        }
        else
        {
            msc.aud.Play();
            soundButton.GetComponent<Image>().sprite = musicButtonOn;
            msc.isSoundsDisabled = false;
            PlayerPrefs.SetInt("issoundon", 1);
            PlayerPrefs.SetInt("ismusicon", 1);
        }
    }
    public void _exitButton()
    {
        gameObject.SetActive(false);
    }
    public void _devamEt()
    {
        gameObject.SetActive(false);
        mtr.gamePaused = false;
        GameObject.Find("player").GetComponent<player>().timerIsRunning = true;
        _loveBar.enabled = true;


    }
    public void _çık()
    {
        gameObject.SetActive(false);
        mtr.gamePaused = false;
    }
    public void _exit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
