using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameMotor : MonoBehaviour
{
    public bool gamePaused;

    private static gameMotor instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }


        if (PlayerPrefs.GetInt("firsttime") == 0)
        {
            PlayerPrefs.SetInt("issoundon", 1);
            PlayerPrefs.SetInt("ismusicon", 1);
            setSkillHakkı();

            PlayerPrefs.SetInt("firsttime", 1);
        }

        
    }
        void Start()
        {
            setSkillHakkı();
            gamePaused = false;
    }

        // Update is called once per frame
        void Update()
        {
        }

        void setSkillHakkı()
        {
            if(PlayerPrefs.GetInt("firsttime") == 0)
            {     
                PlayerPrefs.SetInt("shieldskillcount", 3);
                PlayerPrefs.SetInt("freezeskillcount", 3);
                PlayerPrefs.SetInt("teleportskillcount", 3);

                PlayerPrefs.SetInt("setskillcountsonetime", 1);
            }


        }
    }
