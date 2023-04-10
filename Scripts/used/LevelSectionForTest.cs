using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSectionForTest : MonoBehaviour
{
    public GameObject level, content;
    private int counter = 0;

    void Start()
    {
        for (int a = 0; a < 40; a++)
        {
            GameObject instance = Instantiate(level);
            instance.transform.parent = content.transform;
            instance.name = "Level" + (++counter);
            instance.transform.GetChild(0).GetComponent<Text>().text = counter.ToString();
        }

        int levelSize = 30;
        float contentHeight = levelSize * 50;

        content.GetComponent<RectTransform>().sizeDelta = new Vector2(995, contentHeight);
    }

    void Update()
    {
        
    }
}
