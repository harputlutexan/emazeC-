using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LangSys : MonoBehaviour
{
    public TextAsset TR, EN;
    public string systemLanguage;
    public static LangSys instanceLang;

    void Awake()
    {
        if (!instanceLang)
        {
            systemLanguage = "tr";
            instanceLang = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

}
