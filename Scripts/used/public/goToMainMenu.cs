using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goToMainMenu : MonoBehaviour
{
    public void _goToMainMenu()
    {
        if(PlayerPrefs.GetInt("istutorialplayed") == 0)
        {
            SceneManager.LoadScene("Level1");
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
        
    }
}
