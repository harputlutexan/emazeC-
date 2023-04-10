using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forAndroid : MonoBehaviour
{
    public GameObject player;
    private bool _left, _right, _up, _down;
    private bool isFaceRight = true;
    void Update()
    {
        if (_up)
        {
            player.transform.position += new Vector3(0, 3f * Time.deltaTime, 0);
        }
        if (_down)
        {
            player.transform.position -= new Vector3(0, 3f * Time.deltaTime, 0);
        }
        if (_left)
        {
            player.transform.position -= new Vector3(3f * Time.deltaTime, 0, 0);
            if (isFaceRight)
            {
                isFaceRight = false;

                Vector3 newScale = player.transform.localScale;
                newScale.x *= -1;
                player.transform.localScale = newScale;
            }
        }
        if (_right)
        {
            player.transform.position += new Vector3(3f * Time.deltaTime, 0, 0);
            if (!isFaceRight)
            {
                isFaceRight = true;

                Vector3 newScale = player.transform.localScale;
                newScale.x *= -1;
                player.transform.localScale = newScale;
            }
        }
    }

    public void leftDown()
    {
        _left = true;
    }
    public void leftUp()
    {
        _left = false;
    }
    public void rightDown()
    {
        _right = true;
    }
    public void rightUp()
    {
        _right = false;
    }
    public void upDown()
    {
        _up = true;
    }
    public void upUp()
    {
        _up = false;
    }
    public void downDown()
    {
        _down = true;
    }
    public void downUp()
    {
        _down = false;
    }
}
