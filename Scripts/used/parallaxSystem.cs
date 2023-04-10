using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class parallaxSystem : MonoBehaviour
{
    public GameObject[] backgrounds;
    public GameObject cam,black;

    private GameObject player;

    private Vector3 pivot;
    private Vector3[] bgFirstPositions;

    private openingAnim openingAnimScript;

    private void Awake()
    {
        openingAnimScript = black.GetComponent<openingAnim>();
    }
    void Start()
    {
        player = GameObject.Find("player");
        pivot = player.transform.position;
        bgFirstPositions = new Vector3[6];

        for(int a = 0; a <= 5; a++)
        {
            bgFirstPositions[a] = backgrounds[a].transform.position;
        }
    }

    void Update()
    {
        if (!openingAnimScript.getOpeningAnimIsFinished())
        {
            if (pivot.x - cam.transform.position.x > 0)
            {
                float distance = Mathf.Abs(pivot.x - cam.transform.position.x);
                backgrounds[0].transform.position += new Vector3(distance * 1.8f * Time.deltaTime, 0, 0);
                backgrounds[1].transform.position += new Vector3(distance * 1.2f * Time.deltaTime, 0, 0);
                backgrounds[2].transform.position += new Vector3(distance * 0.75f * Time.deltaTime, 0, 0);
                backgrounds[3].transform.position += new Vector3(distance * 0.4f * Time.deltaTime, 0, 0);
                backgrounds[4].transform.position += new Vector3(distance * 0.3f * Time.deltaTime, 0, 0);
                backgrounds[5].transform.position += new Vector3(distance * 0.2f * Time.deltaTime, 0, 0);
            }
            if (pivot.x - cam.transform.position.x < 0)
            {
                float distance = Mathf.Abs(pivot.x - cam.transform.position.x);
                backgrounds[0].transform.position -= new Vector3(distance * 1.8f * Time.deltaTime, 0, 0);
                backgrounds[1].transform.position -= new Vector3(distance * 1.2f * Time.deltaTime, 0, 0);
                backgrounds[2].transform.position -= new Vector3(distance * 0.75f * Time.deltaTime, 0, 0);
                backgrounds[3].transform.position -= new Vector3(distance * 0.4f * Time.deltaTime, 0, 0);
                backgrounds[4].transform.position -= new Vector3(distance * 0.3f * Time.deltaTime, 0, 0);
                backgrounds[5].transform.position -= new Vector3(distance * 0.2f * Time.deltaTime, 0, 0);
            }
        }
        else
        {
            if (PlayerPrefs.GetInt("istutorialbuttonpressed") == 1)
            {
                SceneManager.LoadScene("MainMenu");
            }
            else
            {
                SceneManager.LoadScene("Level1");
            }
            
        }
        
    }

    public void setPivot()
    {
        pivot = cam.transform.position;
    }

    public void startBlackAnim()
    {
        black.SetActive(true);
    }
}
