using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class music : MonoBehaviour
{
    public bool isSoundsDisabled,isAudioPlaying;
    public static music instance;
    public AudioSource aud;
    private bool isPaused = false;

    private void Awake()
    {
        aud = GetComponent<AudioSource>();
        if (instance !=  null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }

    
    private void Start()
    {

        if ((PlayerPrefs.GetInt("ismusicon") == 1))
        {
            gameObject.GetComponent<AudioSource>().Play();
            PlayerPrefs.SetInt("isitstart", 1);
        }
        else
        {
            gameObject.GetComponent<AudioSource>().Stop();
        }

        if (PlayerPrefs.GetInt("issoundon") == 1)
        {
            isSoundsDisabled = true;
        }
        else
        {
            isSoundsDisabled = false;
        }
    }

    private void Update()
    {
       if (aud.isPlaying)
        {
            isAudioPlaying = true;
        }
        else
        {
            isAudioPlaying = false;
        }
    }
}
