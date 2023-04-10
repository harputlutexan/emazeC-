using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class progressBar : MonoBehaviour
{
    private GameObject player;
    private Vector3 levelStartPos, levelEndPos;
    private float distanceBetWeenTwoPos;
    private float progress,progressBG;

    private Image thisImage;
    void Start()
    {
        thisImage = GetComponent<Image>();
        progress = 0;
        player = GameObject.Find("player");
        levelStartPos = player.transform.position;
        levelEndPos = GameObject.FindGameObjectWithTag("queen").transform.position; //  prensesin pozisyonu, level için son pozisyon olarak ayarlandı.
        distanceBetWeenTwoPos = Mathf.Abs(levelEndPos.x - player.transform.position.x); // levelin son pozisyonu ile başlangıç pozisyonu arasındaki mesafe
        progressBG = distanceBetWeenTwoPos; // progress barın sayacağı total mesafe
    }

    void Update()
    {

        if(Time.frameCount % 15 == 0) // 15 frame de bir çalışır ve progressbar için süreci ekranda gösterir.
        {
            thisImage.fillAmount = Mathf.Clamp01((player.transform.position.x-levelStartPos.x) / (progressBG));
        }
        
    }

}
