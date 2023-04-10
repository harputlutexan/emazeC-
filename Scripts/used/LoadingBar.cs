using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingBar : MonoBehaviour
{
    private Image _LoadingBar;
    public GameObject player, queen, heart;

    public float _count;
    private float timer;

    Vector3 firstPos;
    private bool skipButtonPressed;
    private bool isSceneLoaded;
    private bool goMainMenu;
    AsyncOperation asyncLoad;

    private void Awake()
    {
        _LoadingBar = GameObject.Find("Loadingbar").GetComponent<Image>();
        _count = 0f;

        firstPos = player.transform.localPosition;
        timer = 0.75f;
    }

    private void Start()
    {
        goMainMenu = false;
        isSceneLoaded = false;

        asyncLoad = SceneManager.LoadSceneAsync("MainMenu"); // Arkaplanda MainMenu sahnesini yüke
        asyncLoad.allowSceneActivation = false; // Sahne yüklenmesi bittiğinde direkt geçme, komut bekle.
        StartCoroutine(LoadTheScene()); // Arkaplanda sahne yüklenmesine başla.
    }

    private void Update()
    {
        if (isSceneLoaded)
        {
            if (goMainMenu)
            {
                asyncLoad.allowSceneActivation = true; // bu değer true olduğunda yüklenen sahneye geçilir
            }
        }
        loadingBarSlow();
    }

    private void loadingBarSlow() // 3 saniye boyunca loading bar animasyonunu oynat
    {   
        _count += Time.deltaTime;
        _LoadingBar.fillAmount = Mathf.Clamp01(_count / 3f);

        float posx = _LoadingBar.rectTransform.rect.width * _LoadingBar.fillAmount;

        if(player.transform.localPosition.x < (queen.transform.localPosition.x - 90f))
        {
            player.transform.localPosition = new Vector3(posx + (firstPos.x), player.transform.localPosition.y, player.transform.localPosition.z);
        }
        else
        {
            kalpAnimasyonuGosterVeSonraAnaMenüyeGit();
        }
    }

    private void goToTheMainMenu()
    {
        goMainMenu = true;
    }

    private void kalpAnimasyonuGosterVeSonraAnaMenüyeGit()
    {
        heart.SetActive(true);

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = 0;
            goToTheMainMenu();
        }
    }

    IEnumerator LoadTheScene()
    {
        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                isSceneLoaded = true;
                break;
            }
            yield return null;
        }
    }
}
