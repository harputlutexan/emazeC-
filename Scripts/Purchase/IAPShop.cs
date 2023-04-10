using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;


public class IAPShop : MonoBehaviour
{
    private string altinPaket1 = "macofugames.emazeball.1stpackage";
    private string altinPaket2 = "macofugames.emazeball.2ndpackage";
    private string altinPaket3 = "macofugames.emazeball.3rdpackage";
    int currentCoinScore;
    private Text altinSayisiText;
    private IStoreController m_StoreController;
    private Text paket1CountText, paket2CountText, paket3CountText
    , coinPackage1Value, coinPackage2Value, coinPackage3Value;

    private void Start()
    {
        coinPackage1Value = GameObject.Find("coinValue1").gameObject.GetComponent<Text>();
        coinPackage2Value = GameObject.Find("coinValue2").gameObject.GetComponent<Text>();
        coinPackage3Value = GameObject.Find("coinValue3").gameObject.GetComponent<Text>();
        coinPackage1Value.text = GetPrice(altinPaket1);
        coinPackage2Value.text = GetPrice(altinPaket2);
        coinPackage3Value.text = GetPrice(altinPaket3);

    }


    public void OnPurchaseComplete(Product product)
    {
        altinSayisiText = GameObject.Find("altinsayisiText").GetComponent<Text>();
        currentCoinScore = PlayerPrefs.GetInt("coin_score");
        Debug.Log("1 adet " + product.definition.id + " satın aldınız !");

        if (product.definition.id == altinPaket1)
        {
            PlayerPrefs.SetInt("coin_score", currentCoinScore+1500);
            Debug.Log("1 adet " + product.definition.id+ " satın aldınız !");
            altinSayisiText.text = (currentCoinScore + 1500).ToString();

        }
        if (product.definition.id == altinPaket2)
        {
            PlayerPrefs.SetInt("coin_score", currentCoinScore+4500);
            Debug.Log("1 adet " + product.definition.id + " satın aldınız !");
            altinSayisiText.text = (currentCoinScore + 4500).ToString();

        }
        if (product.definition.id == altinPaket3)
        {
            PlayerPrefs.SetInt("coin_score", currentCoinScore+10000);
            Debug.Log("1 adet " + product.definition.id + " satın aldınız !");
            altinSayisiText.text = (currentCoinScore + 10000).ToString();

        }
    }

    

    private string GetPrice(string productID)
    {

        if (CodelessIAPStoreListener.Instance != null)
        return CodelessIAPStoreListener.Instance.GetProduct(productID).metadata.localizedPriceString;
        else
            return "null ";
    }


    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
    {
        Debug.Log("Purchase of " + product.definition.id + " failed due to " + reason);
    }
}
