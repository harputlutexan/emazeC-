using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Purchasing;


public class Market : MonoBehaviour
{
    private AsyncOperation asyncLoad;
    private bool isSceneLoaded;

    private float AltinPaket1Money, AltinPaket2Money, AltinPaket3Money;
    private int coinScore;
    private Text coinCountText;
    private Text paket1CountText, paket2CountText, paket3CountText
        , coinPackage1Value, coinPackage2Value, coinPackage3Value;
    private IStoreController m_StoreController;
    private string altinPaket1 = "macofugames.emazeball.1stpackage";
    private string altinPaket2 = "macofugames.emazeball.2ndpackage";
    private string altinPaket3 = "macofugames.emazeball.3rdpackage";


    private void Start()
    {
        coinScore = PlayerPrefs.GetInt("coin_score");
        asyncLoad = SceneManager.LoadSceneAsync("MainMenu");
        coinCountText = GameObject.Find("altinsayisiText").GetComponent<Text>();

        paket1CountText = GameObject.Find("paket1Count").transform.GetChild(1).gameObject.GetComponent<Text>();
        paket2CountText = GameObject.Find("paket2Count").transform.GetChild(1).gameObject.GetComponent<Text>();
        paket3CountText = GameObject.Find("paket3Count").transform.GetChild(1).gameObject.GetComponent<Text>();
        /*coinPackage1Value = GameObject.Find("coinValue1").gameObject.GetComponent<Text>();
        coinPackage2Value = GameObject.Find("coinValue2").gameObject.GetComponent<Text>();
        coinPackage3Value = GameObject.Find("coinValue3").gameObject.GetComponent<Text>();*/


        coinCountText.text = coinScore.ToString();

        paket1CountText.text = PlayerPrefs.GetInt("shieldskillcount").ToString();
        paket2CountText.text = PlayerPrefs.GetInt("freezeskillcount").ToString();
        paket3CountText.text = PlayerPrefs.GetInt("teleportskillcount").ToString();
        /*coinPackage1Value.text = GetPrice(altinPaket1);
        coinPackage2Value.text = GetPrice(altinPaket2);
        coinPackage3Value.text = GetPrice(altinPaket3);*/


        asyncLoad.allowSceneActivation = false;

        StartCoroutine(LoadTheScene());
    }
    public void go_MainMenu()
    {
        asyncLoad.allowSceneActivation = true;
    }

    IEnumerator LoadTheScene()
    {
        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                isSceneLoaded = true;
                break;
            }
            yield return null;
        }
    }

    /*private string GetPrice(string productID)
    {
        if (m_StoreController != null && m_StoreController.products != null)
            return m_StoreController.products.WithID(productID).metadata.localizedPriceString;
        else
            return "m_StoreController null";
    }*/

}
