using UnityEngine;
using UnityEngine.SceneManagement;

public class Logo : MonoBehaviour
{
    private void Start()
    {
        //PlayerPrefs.DeleteAll();
    }
    public void loadNextScene()
    {
        SceneManager.LoadScene("LoadingScene"); // Logo animasyonu bitiminde LoadingScreen sahnesini yükle
    }
}
