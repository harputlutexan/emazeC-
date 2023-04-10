using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class forSkip : MonoBehaviour
{
    private bool skipButtonPressed;
    private bool isSceneLoaded;
    AsyncOperation asyncLoad;

    private void Start()
    {
        isSceneLoaded = false;

        if(PlayerPrefs.GetInt("istutorialbuttonpressed") == 0)
        {
            asyncLoad = SceneManager.LoadSceneAsync("Level1");
        }
        else
        {
            asyncLoad = SceneManager.LoadSceneAsync("MainMenu");
        }
        PlayerPrefs.SetInt("istutorialbuttonpressed",0);//buradan izleyip veya skp edip cikinca tutorial tusuna basmamis gibi olacak

        asyncLoad.allowSceneActivation = false;

        skipButtonPressed = false;
        StartCoroutine(LoadTheScene());
    }
    private void Update()
    {
        if(skipButtonPressed && isSceneLoaded)
        {
            asyncLoad.allowSceneActivation = true;
        }
    }


    public void skipTheTutorial()
    {
        skipButtonPressed = true;
    }


    IEnumerator LoadTheScene()
    {
        while (!asyncLoad.isDone)
        {
            if(asyncLoad.progress >= 0.9f)
            {
                isSceneLoaded = true;
                break;
            }
            yield return null;
        }
    }
}
