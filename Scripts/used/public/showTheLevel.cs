using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class showTheLevel : MonoBehaviour
{
    private Text _text;

    private void Awake()
    {
        _text = GetComponent<Text>();
    }
    void Start()
    {
        //_text.text = PlayerPrefs.GetInt("selectedLevel").ToString();
    }
}
