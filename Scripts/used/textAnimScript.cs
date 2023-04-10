using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textAnimScript : MonoBehaviour
{
    public int coinCount;
    public int starCount;
    private Text leftText;
    selectLevelCanvas script;

    private void Awake()
    {
        script = GameObject.Find("SelectLevel").GetComponent<selectLevelCanvas>();
    }

    private void Start()
    {
        script.setScore();
        GetComponent<Text>().text = "+" + starCount*10;
        //GetComponent<Text>().text = "+" + starCount + " x" + coinCount;
    }

    public void animFinish()
    {
        Destroy(gameObject);
    }
}
