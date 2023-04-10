using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class watchVideoButton : MonoBehaviour, IPointerClickHandler
{
    private AdMobScript theAdMobScript;

    void Awake()
    {

    }
    void Start()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //AdMobScript.instance.ShowRewardBasedVideo();
    }
}
