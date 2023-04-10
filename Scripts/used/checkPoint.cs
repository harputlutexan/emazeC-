using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPoint : MonoBehaviour
{
    public AudioClip clip;
    private AudioSource sourc;
    private bool firstTime;

    private music msc;
    private bool isSoundOn;

    private void Awake()
    {
        sourc = GetComponent<AudioSource>();
        if (GameObject.Find("MUSIC") != null)
        {
            msc = GameObject.Find("MUSIC").GetComponent<music>();
        }
    }

    void Start()
    {
        firstTime = true;

        if (PlayerPrefs.GetInt("issoundon") == 1)
        {
            isSoundOn = true;
        }
        else
        {
            isSoundOn = false;
        }
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerPrefs.GetInt("issoundon") == 1)
        {
            if (collision.tag == "Player")
            {
                if (firstTime)
                {
                    sourc.PlayOneShot(clip);
                    firstTime = false;
                }

            }
        }   
    }
}
