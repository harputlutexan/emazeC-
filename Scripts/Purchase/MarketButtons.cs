using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MarketButtons : MonoBehaviour, IPointerClickHandler
{
    private Market marketScript;
    private int coinScore;
    private int paket1Price, paket2Price, paket3Price;

    private Text altinSayisiText;

    private Text paket1CountText, paket2CountText, paket3CountText;
    private GameObject toaster;

    private void Start()
    {
        marketScript = GameObject.Find("Canvas").GetComponent<Market>();
        coinScore = PlayerPrefs.GetInt("coin_score");
        altinSayisiText = GameObject.Find("altinsayisiText").GetComponent<Text>();

        paket1CountText = GameObject.Find("paket1Count").transform.GetChild(1).gameObject.GetComponent<Text>();
        paket2CountText = GameObject.Find("paket2Count").transform.GetChild(1).gameObject.GetComponent<Text>();
        paket3CountText = GameObject.Find("paket3Count").transform.GetChild(1).gameObject.GetComponent<Text>();

        paket1Price = 750;
        paket2Price = 500;
        paket3Price = 1000;
        toaster= GameObject.Find("toaster");
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        switch (gameObject.name)
        {
            case "paket1":
                kalkanAl();
                break;
            case "paket2":
                buzAl();
                break;
            case "paket3":
                isinlanAl();
                break;
        }
    }

    private void kalkanAl()
    {
        coinScore = PlayerPrefs.GetInt("coin_score");
        if(coinScore >= paket1Price)
        {
            
            int shieldSkillCount = PlayerPrefs.GetInt("shieldskillcount");
            shieldSkillCount++;
            PlayerPrefs.SetInt("shieldskillcount", shieldSkillCount);
            coinScore -= paket1Price;

            PlayerPrefs.SetInt("coin_score", coinScore);

            altinSayisiText.text = coinScore.ToString();
            paket1CountText.text = PlayerPrefs.GetInt("shieldskillcount").ToString();
        }
        else
        {
            //Debug.Log("yeterli paranız yok !");
            toaster.GetComponent<Animator>().enabled = true;
        }
    }
    private void buzAl()
    {
        coinScore = PlayerPrefs.GetInt("coin_score");
        if (coinScore >= paket2Price)
        {         
            
            int freezeSkillCount = PlayerPrefs.GetInt("freezeskillcount");
            freezeSkillCount++;
            PlayerPrefs.SetInt("freezeskillcount", freezeSkillCount);
            coinScore -= paket2Price;

            PlayerPrefs.SetInt("coin_score", coinScore);

            altinSayisiText.text = coinScore.ToString();
            paket2CountText.text = PlayerPrefs.GetInt("freezeskillcount").ToString();
        }
        else
        {
            //Debug.Log("yeterli paranız yok !");
            toaster.GetComponent<Animator>().enabled = true;

        }
    }
    private void isinlanAl()
    {

        coinScore = PlayerPrefs.GetInt("coin_score");
        if (coinScore >= paket3Price)
        {

            int teleportSkillCount = PlayerPrefs.GetInt("teleportskillcount");
            teleportSkillCount++;
            PlayerPrefs.SetInt("teleportskillcount", teleportSkillCount);
            coinScore -= paket3Price;

            PlayerPrefs.SetInt("coin_score", coinScore);

            altinSayisiText.text = coinScore.ToString();
            paket3CountText.text = PlayerPrefs.GetInt("teleportskillcount").ToString();
        }
        else
        {
            //Debug.Log("yeterli paranız yok !");
            toaster.GetComponent<Animator>().enabled = true;

        }
    }
}