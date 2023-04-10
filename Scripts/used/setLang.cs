using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setLang : MonoBehaviour
{
    public GameObject MultiLanguageSystem;
    private Text text;
    private TextAsset TR, EN;
    private List<string> stringArrayTR, stringArrayEN;
    private bool oneTimeTR,oneTimeEN;
    public string firstLine;

    void Start()
    {
        oneTimeTR = true;
        oneTimeEN = true;

        text = GetComponent<Text>();

        TR = LangSys.instanceLang.TR;
        EN = LangSys.instanceLang.EN;
    }

    void Update()
    {

        if(LangSys.instanceLang.systemLanguage == "en")
        {
            setEN();
        }
        if(LangSys.instanceLang.systemLanguage == "tr")
        {
            setTR();
        }
    }

    private void setEN()
    {
        oneTimeTR = true;
        if (oneTimeEN)
        {
            string[] lines = EN.text.Split('\n');
            //Debug.Log(lines[0]);
            //Debug.Log(lines[1]);
            string key = "";
            for (int a = 0; a < lines.Length; a++)
            {
                key = lines[a].Substring(0,lines[a].IndexOf("="));
                if (firstLine == key)
                {
                    text.text = lines[a].Substring(lines[a].IndexOf("=")+1, lines[a].Length - lines[a].IndexOf("=") - 1);
                }
            }
            //oneTimeEN = false;
        }
        
    }
    private void setTR()
    {
        oneTimeEN = true;
        if (oneTimeTR)
        {
            string[] lines = TR.text.Split('\n');
            string key = "";
            for (int a = 0; a < lines.Length; a++)
            {
                key = lines[a].Substring(0, lines[a].IndexOf("="));
                if (firstLine == key)
                {
                    text.text = lines[a].Substring(lines[a].IndexOf("=") + 1, lines[a].Length - lines[a].IndexOf("=") - 1);
                }
            }
            //oneTimeTR = false;
        }
    }
}
