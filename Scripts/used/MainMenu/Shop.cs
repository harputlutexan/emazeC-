using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Text shieldItemCountText, freezeItemCountText, teleportItemCountText,totalScoreCountText;

    private int shieldItemCounter, freezeItemCounter, teleportItemCounter;
    private int scoreToBuyShieldItem, scoreToBuyFreezeItem, scoreToBuyTeleportItem;

    private int currentScore, currentScoreTemp;

    private int newShieldItemCount, newFreezeItemCount, newTeleportItemCount;

    void Start()
    {
        currentScore = PlayerPrefs.GetInt("score");
        currentScoreTemp = currentScore;

        shieldItemCounter = 0;
        freezeItemCounter = 0;
        teleportItemCounter = 0;

        newShieldItemCount = PlayerPrefs.GetInt("shieldskillcount");
        newFreezeItemCount = PlayerPrefs.GetInt("freezeskillcount");
        newTeleportItemCount = PlayerPrefs.GetInt("teleportskillcount");

        scoreToBuyShieldItem = 5;
        scoreToBuyFreezeItem = 5;
        scoreToBuyTeleportItem = 5;
    }

    public void _isOpened()
    {
        currentScore = PlayerPrefs.GetInt("score");
        currentScoreTemp = currentScore;
        totalScoreCountText.text = currentScore.ToString();
    }

    public void applyAll()
    {
        PlayerPrefs.SetInt("score", currentScore);

        PlayerPrefs.SetInt("shieldskillcount", newShieldItemCount);
        PlayerPrefs.SetInt("freezeskillcount", newFreezeItemCount);
        PlayerPrefs.SetInt("teleportskillcount", newTeleportItemCount);

        currentScore = PlayerPrefs.GetInt("score");
    }

    public void addShieldItem()
    {
        if (isItEnoughForPurchase("shieldItem",shieldItemCounter))
        {
            shieldItemCounter++;
            showTheCounter(shieldItemCountText, shieldItemCounter);

            int nextScore = currentScore - scoreToBuyShieldItem;
            currentScore = nextScore;
            totalScoreCountText.text = currentScore.ToString();

            int getShieldItemCount = PlayerPrefs.GetInt("shieldskillcount");
            newShieldItemCount++;
        }
        else
        {
            Debug.Log("Puan yeterli değil !");
        } 
    }
    public void addFreezeItem()
    {
        if (isItEnoughForPurchase("freezeItem", freezeItemCounter))
        {
            freezeItemCounter++;
            showTheCounter(freezeItemCountText, freezeItemCounter);

            int nextScore = currentScore - scoreToBuyFreezeItem;
            currentScore = nextScore;
            totalScoreCountText.text = currentScore.ToString();

            int getFreezeItemCount = PlayerPrefs.GetInt("freezeskillcount");
            newFreezeItemCount++;
        }
        else
        {
            Debug.Log("Puan yeterli değil !");
        }
    }
    public void addTeleportItem()
    {
        if (isItEnoughForPurchase("teleportItem", teleportItemCounter))
        {
            teleportItemCounter++;
            showTheCounter(teleportItemCountText, teleportItemCounter);

            int nextScore = currentScore - scoreToBuyTeleportItem;
            currentScore = nextScore;
            totalScoreCountText.text = currentScore.ToString();

            int getTeleportItemCount = PlayerPrefs.GetInt("teleportskillcount");
            newTeleportItemCount++;
        }
        else
        {
            Debug.Log("Puan yeterli değil !");
        }
    }

    public void cancelAll()
    {
        deleteAll();
    }

    public void exitFromShop()
    {
        deleteAll();
        gameObject.SetActive(false);
    }

    private void deleteAll()
    {
        shieldItemCounter = 0;
        freezeItemCounter = 0;
        teleportItemCounter = 0;

        showTheCounter(shieldItemCountText, shieldItemCounter);
        showTheCounter(freezeItemCountText, freezeItemCounter);
        showTheCounter(teleportItemCountText, teleportItemCounter);

        currentScore = currentScoreTemp;
        totalScoreCountText.text = currentScore.ToString();
    }
    private void exit()
    {
        deleteAll();
        exit();

        gameObject.SetActive(false);
    }

    private void showTheCounter(Text text, int count)
    {
        text.text ="+" +count.ToString();
    }


    private bool isItEnoughForPurchase(string itemName,int itemCount)
    {
        if (itemName.Equals("shieldItem"))
        {

            if (currentScore >= scoreToBuyShieldItem)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        else if (itemName.Equals("freezeItem"))
        {

            if (currentScore >= scoreToBuyFreezeItem)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        else if (itemName.Equals("teleportItem"))
        {

            if (currentScore >= scoreToBuyTeleportItem)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        else return false;
    }

}
