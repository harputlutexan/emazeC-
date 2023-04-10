using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scriptForLevelButtonsTest : MonoBehaviour , IPointerClickHandler
{
    private GameObject content;
    private int nextLevelIndis;

    void Awake()
    {
        content = GameObject.Find("LevelSelectionForTest").transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).gameObject;
    }

    void Start()
    {

        GetComponent<Button>().interactable = true;
        GetComponent<Image>().sprite = Resources.Load<Sprite>("images/MainMenu/LevelSelection/btn_stone_unlock");
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);

        PlayerPrefs.SetString("selectedlevelname", gameObject.name);

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
}
