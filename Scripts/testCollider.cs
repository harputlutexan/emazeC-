using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class testCollider : MonoBehaviour
{
    private Vector2 spriteWidth;
    private BoxCollider2D col;
    private SpriteRenderer spriteRenderer;

    #region for Circle
    public GameObject circleObjectRoot;
    public int segments;
    public float xradius;
    public float yradius;
    public Vector3 position;
    public float lineWidth;
    public float startAngle;
    public float endAngle;
    public Color _color;
    #endregion


    public void olustur()
    {
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("palm"))
        {
            spriteRenderer = obj.GetComponent<SpriteRenderer>();
            spriteWidth = spriteRenderer.size;

            if (obj.GetComponent<BoxCollider2D>() == false)
            {
                obj.AddComponent<BoxCollider2D>();
                col = obj.GetComponent<BoxCollider2D>();
                col.size = spriteWidth;
            }
            else
            {
                col = obj.GetComponent<BoxCollider2D>();
                col.size = spriteWidth;
            }
        }
    }

    public void circleOlustur()
    {
        List<Vector2> newVerticies = new List<Vector2>();
        float x;
        float y;
        float z = 0f;

        float angle = startAngle;

        GameObject obj = circleObjectRoot;
        obj.GetComponent<LineRenderer>().SetVertexCount(segments + 1);
        obj.GetComponent<LineRenderer>().useWorldSpace = false;
        obj.GetComponent<LineRenderer>().sortingLayerName = "Foreground";
        obj.GetComponent<LineRenderer>().material = new Material(Shader.Find("Unlit/Color"));
        obj.GetComponent<LineRenderer>().SetWidth(lineWidth,lineWidth);
        obj.GetComponent<LineRenderer>().sharedMaterial.color = _color;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;

            newVerticies.Add(new Vector2(x, y));
            obj.GetComponent<LineRenderer>().SetPosition(i, new Vector3(x, y, z));

            angle += (endAngle / segments);
        }

        obj.GetComponent<EdgeCollider2D>().points = newVerticies.ToArray();
        Instantiate(obj, position, Quaternion.identity);
    }

    public void sil()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("palm"))
        {
            if (obj.GetComponent<BoxCollider2D>())
            {
                DestroyImmediate(obj.GetComponent<BoxCollider2D>());
            }
        }
    }
    public void yansıma()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("yansima"))
        {
            float angleX = obj.transform.rotation.eulerAngles.x;
            float angleY = obj.transform.rotation.eulerAngles.y;
            float angleZ = obj.transform.rotation.eulerAngles.z;

            Quaternion quatr = Quaternion.Euler(angleX, angleY, angleZ*-1);
 
            GameObject instantiated = Instantiate(obj, new Vector3(obj.transform.localPosition.x, obj.transform.localPosition.y*-1f, obj.transform.localPosition.z), quatr);
            instantiated.transform.localScale = new Vector3(obj.transform.localScale.x, obj.transform.localScale.y * -1, obj.transform.localScale.z);
        }
    }
}
