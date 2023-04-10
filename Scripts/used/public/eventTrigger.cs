using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class eventTrigger : MonoBehaviour ,IPointerDownHandler,IPointerExitHandler,IPointerEnterHandler
{
    private player player;

    private void Start()
    {
        player = GameObject.Find("player").GetComponent<player>();
    }
    public void OnPointerDown(PointerEventData eventData) // Touchpad işlemleri
    {
        if (gameObject.name == "shield")
        {
            player.setShieldOn();
        }
        else if (gameObject.name == "freeze")
        {
            player._freeze();
        }
        else if (gameObject.name == "teleport")
        {
            player._teleport();
        }
        else { }
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        if (gameObject.name == "leftButton")
        {
            player.leftButtonIsNotPressed();
        }
        else if (gameObject.name == "rightButton")
        {
            player.rightButtonIsNotPressed();
        }
        else if (gameObject.name == "upButton")
        {
            player.upButtonIsNotPressed();
        }
        else if (gameObject.name == "downButton")
        {
            player.downButtonIsNotPressed();
        }
        else { }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gameObject.name == "leftButton")
        {
            player.leftButtonPressed();
        }
        else if (gameObject.name == "rightButton")
        {
            player.rightButtonPressed();
        }
        else if (gameObject.name == "upButton")
        {
            player.upButtonPressed();
        }
        else if (gameObject.name == "downButton")
        {
            player.downButtonPressed();
        }
        else { }

    }
}
