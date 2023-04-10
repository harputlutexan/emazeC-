using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loveBar : MonoBehaviour
{
    public GameObject heartAttack;

    public float setTimeForLoveBar;
    public Image barIMG;
    public float timer;
    private player playerScript;
    private bool oneTime = true;

    void Start()
    {
        playerScript = GameObject.Find("player").GetComponent<player>();
        timer = setTimeForLoveBar;
        barIMG = GetComponent<Image>();
    }

    void Update()
    {
        if(barIMG.fillAmount != 0f) // aşk barı sıfırlanmadı ise zaman ile azalt
        {
            timer -= Time.deltaTime;
            if (Time.frameCount % 15 == 0)
            {
                barIMG.fillAmount = Mathf.Clamp01(timer / (setTimeForLoveBar));
            }
            if(barIMG.fillAmount < 0.1)
            {
                if (oneTime)
                {
                    playerScript.alertForPlayer(true);
                    oneTime = false;
                }
            }
            else
            {
                playerScript.alertForPlayer(false);
            }
        }
        else // aşk barı sıfırlandı, düşmanın canını yak ve barı tekrar doldur.
        {
            playerScript.setHurt(); 
            barIMG.fillAmount = 1f;
            timer = setTimeForLoveBar;
        }
    }

    public void _fillTheLoveBar()
    {
        barIMG.fillAmount = 1f;
        timer = setTimeForLoveBar;

        oneTime = true;
        heartAttack.SetActive(false);
    }
}
