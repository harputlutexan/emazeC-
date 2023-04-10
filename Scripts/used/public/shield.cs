using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shield : MonoBehaviour
{
    Animator anima;

    void Start()
    {
        anima = GetComponent<Animator>();
    }

    public void setShieldAnimON()
    {
        anima.SetBool("fadeOn", true);
    }

    public void setShieldAnimOFF()
    {
        anima.SetBool("fadeOn", false);
        GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 1f);
        Debug.Log("aaaa");
    }
}
