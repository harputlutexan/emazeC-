using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallaxSystemForMainGame : MonoBehaviour
{
    public GameObject[] backgrounds;
    public GameObject cam;

    public GameObject player;
    private float offsetX, offsetY;

    private Vector3 oldCampos, newCampos;

    void Start()
    {
        oldCampos = new Vector3(cam.transform.position.x, cam.transform.position.x, cam.transform.position.z);
    }

    void Update()
    {
        StartCoroutine(camPos());
        newCampos = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z);

        float distance = newCampos.x - oldCampos.x;
        backgrounds[0].transform.position += new Vector3(10f * distance * 1.8f * Time.deltaTime, 0, 0);
        backgrounds[1].transform.position += new Vector3(10f * distance * 1.2f * Time.deltaTime, 0, 0);
        backgrounds[2].transform.position += new Vector3(10f * distance * 0.75f * Time.deltaTime, 0, 0);
        backgrounds[3].transform.position += new Vector3(10f * distance * 0.4f * Time.deltaTime, 0, 0);
        backgrounds[4].transform.position += new Vector3(10f * distance * 0.3f * Time.deltaTime, 0, 0);
        backgrounds[5].transform.position += new Vector3(10f * distance * 0.2f * Time.deltaTime, 0, 0);
        
       
    }

    IEnumerator camPos()
    {
        yield return null;
        oldCampos = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z);
    }
}
