using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class logoSceneSound : MonoBehaviour
{
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        if (PlayerPrefs.GetInt("firstgamestart") == 0)//oyunun ilk acilisinda yapilan kod
        {
            PlayerPrefs.SetInt("freezeskillcount", 3); // bu değeri playerprefs içerisine kayıt et.
            PlayerPrefs.SetInt("teleportskillcount", 3);
            PlayerPrefs.SetInt("shieldskillcount", 3);
            PlayerPrefs.SetInt("firstgamestart", 1);
            PlayerPrefs.SetInt("ismusicon", 1);
            PlayerPrefs.SetInt("issoundon", 1);
            GetComponent<AudioSource>().Play();
             if (Application.systemLanguage != SystemLanguage.Turkish){
            PlayerPrefs.SetInt("currentlanguageindex", 1);// ingilicenin indeksi simdilik 1
        }
        }
        else
        {
            if (PlayerPrefs.GetInt("ismusicon") == 1)
            {
                GetComponent<AudioSource>().Play();
            }
            else
            {
                GetComponent<AudioSource>().Stop();
            }
        }
    }

}
