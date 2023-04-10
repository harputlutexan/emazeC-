using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSection : MonoBehaviour
{
    public GameObject panel1, panel2, leftButton, mainMenuCanvas;
    public Image activated, locked;
    private List<GameObject> allLevels;


    /// for content section -start
    public GameObject level,content;
    private List<string> levels;
    private int _count = 1;
    int counter = 0;

    private Vector2 sizeDeltaTemp, anchoredPositionTemp;
    /// for content section -finish
    

    void Start()
    {
        sizeDeltaTemp = content.GetComponent<RectTransform>().sizeDelta;
        anchoredPositionTemp = content.GetComponent<RectTransform>().anchoredPosition;

        levels = new List<string>();
        /// for content section -start
        /// 
        int sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
        string[] scenes = new string[sceneCount];

        for (int i = 0; i < sceneCount; i++)
        {
            scenes[i] = System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i));

            levels.Add(scenes[i]);

        }

        foreach(string str in levels)
        {
            //Debug.Log(str);
            if (str == "Level" + _count)
            {
                GameObject instance = Instantiate(level); // Ana level prefabını klonla
                instance.transform.SetParent(content.transform); // klonlanan prefabı, content içerisine ata
                instance.name = "Level" + (++counter); // Level nesnesini Level1,Level2 ... diye adlandır
                instance.transform.GetChild(0).GetComponent<Text>().text = counter.ToString();
                _count++;
            }
        }

        allLevels = new List<GameObject>();
        foreach (Transform child in content.transform)
        {
            allLevels.Add(child.gameObject);
        }

        
        if (PlayerPrefs.GetInt("nextlevelindis") != 3)
        {
            allLevels[0].transform.GetChild(1).gameObject.SetActive(false);
            allLevels[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("images/MainMenu/LevelSelection/btn_stone_unlock");
        }

        derecelendir();
    }

    private void derecelendir()
    {
        int sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;

        for (int a = 1; a <= (sceneCount - 3); a++)
        {
            if(PlayerPrefs.GetInt("score_star_Level"+a) == 1)
            {
                GameObject.Find("Level" + a).transform.GetChild(3).GetChild(0).GetComponent<Image>().enabled = true;
            }else if(PlayerPrefs.GetInt("score_star_Level" + a) == 2)
            {
                GameObject.Find("Level" + a).transform.GetChild(3).GetChild(0).GetComponent<Image>().enabled = true;
                GameObject.Find("Level" + a).transform.GetChild(3).GetChild(1).GetComponent<Image>().enabled = true;
            }
            else if (PlayerPrefs.GetInt("score_star_Level" + a) == 3)
            {
                GameObject.Find("Level" + a).transform.GetChild(3).GetChild(0).GetComponent<Image>().enabled = true;
                GameObject.Find("Level" + a).transform.GetChild(3).GetChild(1).GetComponent<Image>().enabled = true;
                GameObject.Find("Level" + a).transform.GetChild(3).GetChild(2).GetComponent<Image>().enabled = true;
            }
        }
    }

    public void goBack()
    {
        mainMenuCanvas.SetActive(true);
        gameObject.SetActive(false);
    }


    public void saga()
    {
        panel1.SetActive(false);
        panel2.SetActive(true);
        leftButton.GetComponent<Image>().color = Color.white;
    }
    public void sola()
    {
        panel1.SetActive(true);
        panel2.SetActive(false);
        leftButton.GetComponent<Image>().color = Color.black;
    }


    private void OnDisable()
    {
        resetAnchoredPositionOfContent();
    }



    private void resetAnchoredPositionOfContent()
    {
        content.GetComponent<RectTransform>().sizeDelta = sizeDeltaTemp;
        content.GetComponent<RectTransform>().anchoredPosition = anchoredPositionTemp;
    }
}
