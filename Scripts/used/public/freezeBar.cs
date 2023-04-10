using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class freezeBar : MonoBehaviour
{
    private Image img;
    private float counter;
    // Start is called before the first frame update
    void Start()
    {
        counter = 3f;
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(img.fillAmount > 0 && img.fillAmount != 0)
        {
            counter -= Time.deltaTime;
            img.fillAmount = Mathf.Clamp01(counter/5f);
        }
        else
        {
            counter = 3f;
            gameObject.SetActive(false);
        }
    }
}
