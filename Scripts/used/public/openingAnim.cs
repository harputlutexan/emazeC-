using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class openingAnim : MonoBehaviour
{
    private bool isOpeningAnimFinished;

    void Start()
    {
        isOpeningAnimFinished = false;
    }

    public void openingAnimIsFinished()
    {
        isOpeningAnimFinished = true;
    }

    public bool getOpeningAnimIsFinished()
    {
        return isOpeningAnimFinished;
    }
}
