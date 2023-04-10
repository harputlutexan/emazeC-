using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scriptForLevelButtons : MonoBehaviour , IPointerClickHandler
{
    private GameObject content;
    private int nextLevelIndis;
    private string nextSceneName;

    void Awake()
    {
        content = GameObject.Find("LevelSelection").transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).gameObject;
    }

    void Start()
    {
        nextLevelIndis = PlayerPrefs.GetInt("nextlevelindis");
        nextSceneName = System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(nextLevelIndis));

        if (nextLevelIndis > 0)
        {
            functionForIF();
        }
        else
        {
            functionForELSE();
        }

        canvasSizeFitter();
    }

    private void functionForIF()
    {
        string nextLevelName = GetSceneNameFromBuildIndex(nextLevelIndis);
        string currentLevelName = gameObject.name;

        string a = string.Empty;
        string b = string.Empty;

        int currentLevelNumber = 0;
        int nextLevelNumber = 0;

        for (int i = 0; i < currentLevelName.Length; i++)
        {
            if (char.IsDigit(currentLevelName[i]))
            {
                a += currentLevelName[i];
            }
        }
        currentLevelNumber = int.Parse(a);

        for (int i = 0; i < nextLevelName.Length; i++)
        {
            if (char.IsDigit(nextLevelName[i]))
            {
                b += nextLevelName[i];
            }
        }
        nextLevelNumber = int.Parse(b);

        //Debug.Log(currentLevelNumber);
        //Debug.Log(nextLevelNumber);

        //Debug.Log(SceneManager.GetSceneByBuildIndex(nextLevelIndis).name);

        if (gameObject.name == nextSceneName)
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
        if (gameObject.name == "Level1")
        {
            GetComponent<Button>().interactable = true;
            GetComponent<Image>().sprite = Resources.Load<Sprite>("images/MainMenu/LevelSelection/btn_stone_unlock");
            //transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(2).gameObject.SetActive(false);
        }

        else
        {
            if (nextLevelName == gameObject.name)
            {
                transform.GetChild(2).gameObject.SetActive(false);
                GetComponent<Button>().interactable = true;
                GetComponent<Image>().sprite = Resources.Load<Sprite>("images/MainMenu/LevelSelection/btn_stone_unlock");
            }
            else if (currentLevelNumber < nextLevelNumber)
            {
                GetComponent<Button>().interactable = true;
                GetComponent<Image>().sprite = Resources.Load<Sprite>("images/MainMenu/LevelSelection/btn_stone_unlock");
                transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(false);
            }
            else
            {
                GetComponent<Button>().interactable = false;
                GetComponent<Image>().sprite = Resources.Load<Sprite>("images/MainMenu/LevelSelection/btn_stone_lock");
                transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true);
            }
        }

        if (transform.GetChild(1).gameObject.activeSelf)
        {
            PlayerPrefs.SetString("selectedlevelname", gameObject.name);
        }
    }

    private void functionForELSE()
    {
        if(gameObject.name == "Level1")
        {
            GetComponent<Button>().interactable = true;
            GetComponent<Image>().sprite = Resources.Load<Sprite>("images/MainMenu/LevelSelection/btn_stone_unlock");
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(2).gameObject.SetActive(false);

            PlayerPrefs.SetString("selectedlevelname", gameObject.name);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!transform.GetChild(2).gameObject.activeSelf)
        {
            transform.GetChild(1).gameObject.SetActive(true);

            foreach (Transform child in content.transform)
            {
                if (child.transform.name != gameObject.name)
                {
                    child.transform.GetChild(1).gameObject.SetActive(false);
                }
            }

            PlayerPrefs.SetString("selectedlevelname", gameObject.name);
        }
    }

    string GetSceneNameFromBuildIndex(int index)
    {
        string scenePath = SceneUtility.GetScenePathByBuildIndex(index);
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

        return sceneName;
    }

    void canvasSizeFitter()
    {
        Vector2 deviceScreenResolution = new Vector2(Screen.width, Screen.height);

        float srcHeight = Screen.height;
        float srcWidth = Screen.width;

        float DEVICE_SCREEN_ASPECT = (srcWidth / srcHeight)/srcHeight;

        transform.GetComponent<RectTransform>().localScale = new Vector3(1+DEVICE_SCREEN_ASPECT, 1+DEVICE_SCREEN_ASPECT, 0);
    }
}
