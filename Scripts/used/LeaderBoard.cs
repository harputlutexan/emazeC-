using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{
    public List<Text> text, stars;
    public GameObject MainMenu;
    public GameObject levelPrefab;
    public GameObject content;


    private Object[] ListOfLevels;
    private int counter = 1;
    private List<GameObject> allLevels;

    void Start()
    {

        
        int sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
        allLevels = new List<GameObject>();

        //ListOfLevels = new Object[sceneCount];
        //ListOfLevels = (Object[])Resources.LoadAll("Scenes/LevelScenes");
        //Debug.Log("sceneCount " + ListOfLevels.Length);

        
        for (int a = 0; a < sceneCount-5; a++)// 5 de ddiger sceneler var
        {

            //Debug.Log("currentScoreboardCoinScore: " + PlayerPrefs.GetInt("scoreboard_coin_score_Level" + counter));
            GameObject instance = Instantiate(levelPrefab);
            instance.transform.SetParent(content.transform);
            //instance.name = "Level" + (++counter);
            instance.transform.GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetInt("scoreboard_coin_score_Level" + counter).ToString();
            instance.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetInt("score_star_Level" + counter).ToString();
            float levelTimer = PlayerPrefs.GetFloat("levelTimer_Level" + counter);
            float minutes = Mathf.FloorToInt(levelTimer / 60);
            float seconds = Mathf.FloorToInt(levelTimer % 60);
            string levelTimerText = string.Format("{0:00}:{1:00}", minutes, seconds);
            instance.transform.GetChild(2).GetComponent<Text>().text = levelTimerText;

            instance.transform.GetComponent<Text>().text = "L " + counter;
            //GameObject starInstance = Instantiate(starPrefab);
            //starInstance.transform.SetParent(contentStar.transform);
            //starInstance.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetInt("score_star_Level" + counter).ToString();
            counter = counter +1;
    
        }

        //int levelSize = 30;
        //float contentHeight = levelSize * 100;
        //content.GetComponent<RectTransform>().sizeDelta = new Vector2(996.7633f, contentHeight);
        //content.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -1255);

        foreach (Transform child in content.transform)
        {
            allLevels.Add(child.gameObject);
        }


    }

    public void goBack()
    {
        MainMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
