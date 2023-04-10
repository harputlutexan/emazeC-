using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class queen : MonoBehaviour
{
    public GameObject hearth,selectLevelCanvas;
    private Animator anim;

    public bool isLevelFinished;

    void Start()
    {
        isLevelFinished = false;
        anim = GetComponent<Animator>();
        GetComponent<CircleCollider2D>().radius = 3.28f;
        Vector2 newOffset = new Vector2(-0.1f,0f);
        GetComponent<CircleCollider2D>().offset = newOffset;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            hearth.SetActive(true);
            anim.enabled = true;
        }
    }

    public void nextLevel()
    {
        isLevelFinished = true;
        GameObject.Find("Cameras").transform.GetChild(1).GetComponent<Animator>().enabled = false;
    }
}
