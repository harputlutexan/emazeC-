using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class circleMotorScript : MonoBehaviour
{
    #region for Circle
    public GameObject circleObjectRoot;
    public int segments;
    public float xradius;
    public float yradius;

    private EdgeCollider2D col;
    #endregion

    public void circleOlustur()
    {
        List<Vector2> newVerticies = new List<Vector2>();
        float x;
        float y;
        float z = 0f;

        float angle = 1f;

        GameObject obj = circleObjectRoot;
        obj.GetComponent<LineRenderer>().SetVertexCount(segments + 1);
        obj.GetComponent<LineRenderer>().useWorldSpace = false;
        obj.GetComponent<LineRenderer>().sortingLayerName = "Foreground";

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;

            newVerticies.Add(new Vector2(x, y)); 
            obj.GetComponent<LineRenderer>().SetPosition(i, new Vector3(x, y, z));

            angle += (360f / segments);
        }

        obj.GetComponent<EdgeCollider2D>().points = newVerticies.ToArray();
        Instantiate(obj, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
