using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class frostTimer : MonoBehaviour
{
    private float time;
    private float timeTemp;

    private void Awake()
    {
        time = GameObject.Find("player").GetComponent<player>().freezeTimer;
    }

    void Update()
    {
        if(GetComponent<Image>().fillAmount != 0)
        {
            GetComponent<Image>().fillAmount -= (Time.deltaTime / timeTemp);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        timeTemp = time;
        GetComponent<Image>().fillAmount = 1f;
        Debug.Log("disabled " + time);
    }
}
