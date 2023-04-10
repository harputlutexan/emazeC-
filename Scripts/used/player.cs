using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cinemachine;


public class player : MonoBehaviour
{
    public static player instance;
    private GameObject shieldUI, frostUI, teleportUI;
    public GameObject shield;
    public AudioClip clip, clip2, clip3, clip4;
    public Rigidbody2D body;



    public float damping;
    public float speed;
    public float freezeTimer, teleportTimer;
    public float shieldTimer;
    public float VELOCITY_INCREMENT;
    public float MAX_VELOCITY;
    public float MIN_VELOCITY;
    private float freezeTimerTemp, teleportTimerTemp;
    private float scoreCounterTimer = 0f;
    private float timerTemp;
    public float levelTimer = 0;
    private float playerVelocityAngle;

    private GameObject giveUp;
    private GameObject[] hearts;
    private GameObject coinScorePrefab, selectLevelCanvas, respawnParentObject;
    private loveBar _loveBar;
    private Text score, score_Levelici;
    private Text shieldText, freezeText, teleportText, currentLevelText, levelTimerText;
    private Animator shieldAnimator;
    private queen queenScript;

    private bool sektir = false;
    private bool isFaceRight = true;
    private bool animHasFinished;
    public bool hurted;
    private bool isLeftButtonPressed, isRightButtonPressed, isUpButtonPressed, isDownButtonPressed;
    private bool timerOn;
    private bool hittedEnemy;
    private bool movable;
    private bool isFreezePressed, isTeleportPressed;
    private bool isTeleportSkillLocked, isShieldSkillLocked, isFreezeSkillLocked;
    private bool rightLock, leftLock, upLock, downLock;
    private bool end;
    private bool shieldOneTime;
    private bool isItEnd;
    public bool patladi;
    public bool shieldActivated;
    public bool bombayaDokundu = false;
    public bool timerIsRunning = false;

    private string lastKeyPressed = string.Empty;
    private Vector3[] checkPoints;
    private int puan, level_Icı_Puan;

    private int sagsol = 1;
    private Vector3 checkpointPosition;
    public int health = 5;
    public Vector2 velocity;

    private Vector3 respawnPoint;
    private Animator anim;


    private AudioSource audSource;
    private gameMotor _motor;
    private music msc;


    private int shieldCount, freezeCount, teleportCount;

    public int coinValue;

    private int collisionCounter;

    //private List<GameObject> respawns;
    private bool hasaralabilir = true;
    private List<Collider2D> lastHit;

    private bool hasarGormemeTimerBaslat;
    private float hasarGormemeTimer;
    // private bool duvaraDokundu = false;

    private List<GameObject> objList;

    public float bombShieldImgSize;
    public Vector3 imgSize;
    private GameObject frostTimerIMG;

    private int extraCoinScore = 0;
    private Vector2 newPosition = new Vector2();
    public Sprite playerF, queenF;
    private GameObject canvas;
    public bool doNotHitToGreenCanGetDamage = true;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        frostTimerIMG = GameObject.Find("frostTimer");
        frostTimerIMG.SetActive(false);
        shieldUI = GameObject.Find("Skills").transform.GetChild(0).gameObject;
        frostUI = GameObject.Find("freeze");
        teleportUI = GameObject.Find("teleport");
        shield = GameObject.Find("Shield");
        shield.SetActive(false);

        selectLevelCanvas = GameObject.Find("MainGameCanvas").transform.GetChild(10).gameObject;
        giveUp = GameObject.Find("GiveUp");
        coinScorePrefab = GameObject.Find("coinScore");

        //respawns = new List<GameObject>();
        shieldAnimator = shield.GetComponent<Animator>();

        respawnParentObject = GameObject.Find("checkPoints");
        queenScript = GameObject.FindGameObjectWithTag("queen").GetComponent<queen>();
        hearts = new GameObject[6];
        shieldText = GameObject.Find("_shieldText").GetComponent<Text>();
        freezeText = GameObject.Find("_freezeText").GetComponent<Text>();
        teleportText = GameObject.Find("_teleportText").GetComponent<Text>();
        GameObject.Find("Cameras").transform.GetChild(1).GetComponent<Animator>().enabled = false;

        audSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();

        _loveBar = GameObject.Find("loveBar").GetComponent<loveBar>();

        for (int a = 0; a < hearts.Length; a++)
        {
            hearts[a] = GameObject.Find("hearth" + a);
        }

        timerIsRunning = true;
        canvas = GameObject.Find("MainGameCanvas");
    }

    void Start()
    {
        settingBombShieldArea(imgSize, bombShieldImgSize);
        objList = new List<GameObject>();
        lastHit = new List<Collider2D>();
        hasarGormemeTimer = 2f;
        hasarGormemeTimerBaslat = false;
        selectLevelCanvas.SetActive(false);
        giveUp.SetActive(false);
        coinScorePrefab.SetActive(false);
        msc = (music)FindObjectOfType(typeof(music));//reklamlar sonrasi muzike mudahale icin
        body = GetComponent<Rigidbody2D>();
        //score = GameObject.Find("score").GetComponent<Text>();
        score_Levelici = GameObject.Find("score_Levelici").GetComponent<Text>();
        currentLevelText = GameObject.Find("currentLevelText").GetComponent<Text>();
        levelTimerText = GameObject.Find("timerText").GetComponent<Text>();

        float minutes = Mathf.FloorToInt(levelTimer / 60);
        float seconds = Mathf.FloorToInt(levelTimer % 60);
        levelTimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        gameObject.transform.position = new Vector3(5, 0, 0);
        int currentLevelIndisForText = SceneManager.GetActiveScene().buildIndex - 2;
        currentLevelText.text = "L " + currentLevelIndisForText.ToString();
        if (GameObject.Find("MUSIC") != null)
        {
            if (PlayerPrefs.GetInt("ismusicon") == 1)
            {
                if (!msc.isAudioPlaying)
                {
                    msc.aud.Play();
                }

            }
        }

        end = false;
        isItEnd = false;
        collisionCounter = 0;

        rightLock = false;
        leftLock = false;
        upLock = false;
        downLock = false;
        isTeleportSkillLocked = false;
        isShieldSkillLocked = false;
        isFreezePressed = false;
        isTeleportPressed = false;

        shieldCount = PlayerPrefs.GetInt("shieldskillcount");
        //freezeCount = 3;
        teleportCount = PlayerPrefs.GetInt("teleportskillcount");
        freezeCount = PlayerPrefs.GetInt("freezeskillcount");
        //shieldCount = 10;
        puan = PlayerPrefs.GetInt("coin_score");
        //score.text = "" + puan;
        level_Icı_Puan = 0;

        isLeftButtonPressed = false;
        isRightButtonPressed = false;
        isUpButtonPressed = false;
        isDownButtonPressed = false;
        timerOn = false;

        patladi = false;
        animHasFinished = false;
        hittedEnemy = false;
        movable = true;
        shieldOneTime = true;
        hurted = false;
        shieldActivated = false;

        shieldText.text = shieldCount.ToString();
        freezeText.text = freezeCount.ToString();
        teleportText.text = freezeCount.ToString();

        freezeTimerTemp = freezeTimer;
        teleportTimerTemp = teleportTimer;

        teleportText.text = teleportCount.ToString();
        timerTemp = shieldTimer;
        _motor = GameObject.Find("GameMotor").GetComponent<gameMotor>();

        if (PlayerPrefs.GetInt("starscore") == 0)
        {
            PlayerPrefs.SetInt("starscore", 0);
        }
        checkpointPosition = gameObject.transform.position;
        if (health > 0)
        {
            _motor.gamePaused = false;
        }

        checkPoints = new Vector3[respawnParentObject.transform.childCount];
        for (int i = 0; i < respawnParentObject.transform.childCount; i++)
        {
            checkPoints[i] = respawnParentObject.transform.GetChild(i).transform.position;
        }

    }

    void Update()
    {
        if (!end)
        {
            if (!isItEnd)
            {
                gameProcessON(); // eğer oyun bitmemiş ise proseslere devam et
            }
            else
            {
                letTheGameFinish(); // oyunu sonlandır
            }
        }

        //if (giveUp.activeSelf)
        /*    if (AdMobScript.instance.adRewarded)
        {
            if (PlayerPrefs.GetInt("rewardbasedvideo") == 1)
            {
                if (PlayerPrefs.GetInt("ismusicon") == 1){
                    msc.aud.Play();
                }
                resetTheGameFromLastCheckpoint();
                anim.SetBool("hurt",false);
                anim.SetBool("alert", false);
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
        }*/
        if (timerIsRunning)
        {
            levelTimer += Time.deltaTime;
            float minutes = Mathf.FloorToInt(levelTimer / 60);
            float seconds = Mathf.FloorToInt(levelTimer % 60);
            levelTimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    private void gameProcessON()
    {
        animationSystem();
        bombSystem();
        timerHasarGormeme();


        if (movable)
        {
            _healthSystem(); // hasar alma ve can azaltma işlemleri

            if (SystemInfo.deviceType == DeviceType.Desktop)
            {
                _movingForKeyboard();
            }
            else
            {
                _moving();
            }


            // PC tarafı için controller kısımı
            _shieldSystem(); // kalkan ve hasar almama özelliği için fonksiyon
            _skills(); // karakter yetenekleri
        }
    }

    public void reklamIzlendi()
    {
        if (PlayerPrefs.GetInt("ismusicon") == 1)
        {
            if (msc!=null)
            msc.aud.Play();
        }
        resetTheGameFromLastCheckpoint();
        anim.SetBool("hurt", false);
        anim.SetBool("alert", false);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    void _movingForKeyboard() // klavye için controller
    {

        if (Input.GetKey(KeyCode.W) && !upLock)
        {

            velocity.y += VELOCITY_INCREMENT;
            lastKeyPressed = "W";
        }
        if (Input.GetKey(KeyCode.S) && !downLock)
        {
            velocity.y -= VELOCITY_INCREMENT;
            lastKeyPressed = "S";
        }
        if (Input.GetKey(KeyCode.A) && !leftLock)
        {
            if (isFaceRight)
            {
                isFaceRight = false;
                Vector3 newScale = transform.localScale;
                newScale.x *= -1;
                transform.localScale = newScale;
            }
            velocity.x -= VELOCITY_INCREMENT;
            lastKeyPressed = "A";
        }
        if (Input.GetKey(KeyCode.D) && !rightLock)
        {
            if (!isFaceRight)
            {
                isFaceRight = true;
                Vector3 newScale = transform.localScale;
                newScale.x *= -1;
                transform.localScale = newScale;
            }

            velocity.x += VELOCITY_INCREMENT;
            lastKeyPressed = "D";
        }

        velocity.x = velocity.x * damping;
        velocity.y = velocity.y * damping;

        if (velocity.x > MAX_VELOCITY)
        {
            velocity.x = MAX_VELOCITY;
        }
        else if (velocity.x < MIN_VELOCITY)
        {
            velocity.x = MIN_VELOCITY;
        }
        if (velocity.y > MAX_VELOCITY)
        {
            velocity.y = MAX_VELOCITY;
        }
        else if (velocity.y < MIN_VELOCITY)
        {
            velocity.y = MIN_VELOCITY;
        }

    }

    void FixedUpdate()
    {

        //body.velocity = velocity;
        if (!end)
        {
            if (!isItEnd)
            {
                body.velocity = velocity;
            }
            else
            {
                body.velocity = new Vector2(0, 0);

            }

        }
        if (hurted)
        {
            body.velocity = new Vector2(0, 0);

        }

    }

    private void letTheGameFinish()
    {
        if (Time.frameCount % 30 == 0)
        {
            if (queenScript.isLevelFinished) // queen'in kalp animasyonu bittiğinde çalışır ve level başarı ile bitirilir
            {
                selectLevelCanvas.SetActive(true);
                end = true;

            }
        }
    }

    private void timerHasarGormeme()
    {
        if (hasarGormemeTimerBaslat)
        {

            if (hasarGormemeTimer >= 0)
            {
                hasarGormemeTimer -= Time.deltaTime;
                doNotHitToGreenCanGetDamage = false;
            }
            else
            {
                hasarGormemeTimer = 2f;
                hasarGormemeTimerBaslat = false;
                doNotHitToGreenCanGetDamage = true;
            }

        }
    }

    void _skills()
    {
        freeze();
        teleport();
    }

    public void _freeze() // freeze skilli için fonksiyon, freeze buttn ile cagriliyor
    {
        if (!isFreezeSkillLocked) // freeze skili kilitli mi ?
        {
            if (freezeCount > 0) // peki yeteri kadar skill var mı ?
            {
                if (PlayerPrefs.GetInt("issoundon") == 1)
                {
                    audSource.PlayOneShot(clip4); // buz tutma efekti için sesi oynat
                }
                isFreezePressed = true; // skill tuşuna basıldı
                _motor.gamePaused = true; // düşmanları dondur
                freezeCount--; // skill sayısını 1 azalt.
                PlayerPrefs.SetInt("freezeskillcount", freezeCount); // bu değeri playerprefs içerisine kayıt et.
            }
            frostUI.GetComponent<Button>().interactable = false; // butonu tekrar aktif et
        }
        freezeText.text = (freezeCount).ToString(); // yeni skill sayısını arayüzde göster
    }

    void freeze() // freeze skill sistemi ile her update de game end olmadigi surece calisir
    {
        if (isFreezePressed) // freeze skill butonuna basıldı mı ?
        {
            isFreezeSkillLocked = true; // oyuncunun ard arda skill basmasını engellemek için skill tuşunu belli bir süre kitle.
            if (!frostTimerIMG.activeSelf)
            {
                frostTimerIMG.SetActive(true);
            }
            if (freezeTimer > 0) // freezeTimer değeri boyunca düşmanlar donacak
            {
                freezeTimer -= Time.deltaTime;
            }
            else
            {
                frostTimerIMG.SetActive(false);
                _motor.gamePaused = false;
                isFreezeSkillLocked = false;
                freezeTimer = freezeTimerTemp;
                isFreezePressed = false;
                frostUI.GetComponent<Button>().interactable = true;
            }
        }
    }

    void teleport() // teleport skill sistemi
    {
        if (isTeleportPressed) // teleport skill tuşuna basıldı mı ? 
        {
            if (teleportCount != -1) // eğer basıldı ise oyuncunun yeteri kadar skill puanı var mı ?
            {
                teleportUI.GetComponent<Button>().interactable = false; // ard arda skill butona basılmaması için belli bir süre butonu disable et
                isTeleportSkillLocked = true;

                if (teleportTimer > 0) // bu if else blogunun tek amacı ard arda teleport tusuna basilmasini engellemek
                {
                    teleportTimer -= Time.deltaTime;
                }
                else
                {
                    isTeleportSkillLocked = false;
                    teleportTimer = teleportTimerTemp;
                    isTeleportPressed = false;
                    teleportUI.GetComponent<Button>().interactable = true;
                }
            }
        }
    }

    void _shieldSystem() // shield skill sistemi
    {
        if (shieldCount != -1) // shield skill puanı var mı ?
        {
            if (shield.activeSelf) // peki shield su an calisiyor mu ?
            {
                if (shieldOneTime) // o halde shield aktif edilmiştir, zamanlayıcıyı başlat ve ard arda shield kullanılmasını önle
                {
                    shieldActivated = true;
                    timerOn = true;

                    shieldOneTime = false;
                    isShieldSkillLocked = true;
                }
            }
            else
            {
                if (objList.Count > 0)
                {
                    foreach (GameObject x in objList)
                    {
                        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), x.GetComponent<Collider2D>(), false);
                    }
                    objList.Clear();
                }
            }

            if (timerOn) // bu if blogunun amacı ard arda shield kullanılmasını önlemek
            {
                if (shieldTimer > 0f)
                {
                    shieldTimer -= Time.deltaTime;
                }
                else
                {
                    shield.SetActive(false);
                    shieldTimer = timerTemp;
                    timerOn = false;
                    shieldActivated = false;
                    shieldOneTime = true;
                    isShieldSkillLocked = false;
                    shieldUI.GetComponent<Button>().interactable = true;
                }
            }
        }
    }

    public void _teleport() // freeze ile aynı sistem teleport için geçerli
    {
        if (!isTeleportSkillLocked)
        {
            if (teleportCount > 0)
            {
                isTeleportPressed = true;

                for (int i = 0; i < respawnParentObject.transform.childCount; i++)
                {
                    checkPoints[i] = respawnParentObject.transform.GetChild(i).transform.position;
                    //Debug.Log("checkPoints[i]" + checkPoints[i]);
                    if (teleportCount != 0)
                    {
                        if (checkPoints[i].x - 1 > transform.position.x)
                        {
                            teleportPlayerToNextCheckPoint(i);
                            teleportCount--;
                            PlayerPrefs.SetInt("teleportskillcount", teleportCount);
                            break;
                        }
                    }
                }
            }
        }

        teleportText.text = (teleportCount).ToString();
    }

    void teleportPlayerToNextCheckPoint(int i) // playeri bir sonraki checkpointe ışınla
    {
        body.position = checkPoints[i];

    }

    void _moving() // mobil için controller
    {
        if (isUpButtonPressed && !upLock)
        {
            velocity.y += VELOCITY_INCREMENT;
            lastKeyPressed = "UP";
        }
        if (isDownButtonPressed && !downLock)
        {
            velocity.y -= VELOCITY_INCREMENT;
            lastKeyPressed = "DOWN";
        }
        if (isLeftButtonPressed && !leftLock)
        {
            if (isFaceRight)
            {
                isFaceRight = false;
                Vector3 newScale = transform.localScale;
                newScale.x *= -1;
                transform.localScale = newScale;
            }
            velocity.x -= VELOCITY_INCREMENT;
            lastKeyPressed = "LEFT";
        }
        if (isRightButtonPressed && !rightLock)
        {
            if (!isFaceRight)
            {
                isFaceRight = true;
                Vector3 newScale = transform.localScale;
                newScale.x *= -1;
                transform.localScale = newScale;
            }
            velocity.x += VELOCITY_INCREMENT;
            lastKeyPressed = "RIGHT";
        }
        velocity.x = velocity.x * damping;
        velocity.y = velocity.y * damping;

        if (velocity.x > MAX_VELOCITY)
        {
            velocity.x = MAX_VELOCITY;
        }
        else if (velocity.x < MIN_VELOCITY)
        {
            velocity.x = MIN_VELOCITY;
        }
        if (velocity.y > MAX_VELOCITY)
        {
            velocity.y = MAX_VELOCITY;
        }
        else if (velocity.y < MIN_VELOCITY)
        {
            velocity.y = MIN_VELOCITY;
        }

        //transform.position += new Vector3(velocity.x * Time.deltaTime, velocity.y * Time.deltaTime, 0);
    }

    void bombSystem()
    {
        if (patladi)
        {
            GetComponent<CircleCollider2D>().isTrigger = true; // player olumsuz oluyor?
            hearts[health].SetActive(false);
            if (!hurted)
            {
                health -= 1;
            }

            hurted = true;
            patladi = false;
            velocity = Vector2.zero;


        }
    }

    void animationSystem() // can yanma animasyonu için sistem
    {
        if (hurted) // olme sonrasi sadece burasi var??
        {
            velocity = Vector2.zero;

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("playerAnim"))
            {
                anim.SetBool("hurt", true);
                movable = false;
            }
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("hurtAnim"))
            {
                GameObject.Find("Cameras").transform.GetChild(1).GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 3;
                anim.SetBool("hurt", false);
                hurted = false;
                disable_enable_Canvas(false);
            }
        }

        // Alttaki sistem shield için, bitimine yakın yanıp sönme animasyonu ile oyuncuya bilgi veriyor.
        if (shieldTimer < 2.75f)
        {
            shieldAnimator.SetBool("fadeOn", true);
        }
        if (shieldTimer <= 0f)
        {
            shieldAnimator.SetBool("fadeOn", false);
            shield.GetComponent<SpriteRenderer>().color = Color.white;
        }

    }

    public void goToTheCheckpoint()
    {
        body.velocity = Vector2.zero;

        CinemachineFramingTransposer transposer = GameObject.Find("Cameras").transform.GetChild(1).GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
        GameObject.Find("Cameras").transform.GetChild(1).GetComponent<Transform>().position = body.position;
        GameObject.Find("Cameras").transform.GetChild(1).GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 7;
        GetComponent<CircleCollider2D>().isTrigger = false;
        anim.SetBool("alert", false);
        int currentScoreboardCoinScore = PlayerPrefs.GetInt("scoreboard_coin_score_" + SceneManager.GetActiveScene().name);

        if (health == 0)
        {
            PlayerPrefs.SetInt("coin_score_" + SceneManager.GetActiveScene().name, level_Icı_Puan);
            if (level_Icı_Puan >= currentScoreboardCoinScore)
            {
                PlayerPrefs.SetInt("scoreboard_coin_score_" + SceneManager.GetActiveScene().name, level_Icı_Puan);
            }
            //GetComponent<CircleCollider2D>().isTrigger = true;
            giveUp.SetActive(true);
            body.velocity = Vector2.zero;

        }

        if (patladi)
        {
            patladi = false;
            velocity = Vector2.zero;
        }


        for (int i = 0; i < respawnParentObject.transform.childCount; i++)
        {
            if (checkPoints[i].x < transform.position.x + 1f)
            {
                //  Debug.Log("checkPoints[i].x " + checkPoints[i].x);
                hurted = false;//tekrar yanma problemi icin
                transform.position = respawnPoint;
                hurtedSoMakeBombInactive = false;
                break;
            }
            else
            {
                transform.position = new Vector3(5, 0, 0);

            }
        }

        velocity = Vector3.zero;
        movable = true;
        body.velocity = Vector2.zero;
        //_loveBar._fillTheLoveBar();
        GameObject.Find("Cameras").transform.GetChild(1).GetComponent<CinemachineVirtualCamera>().m_Follow = transform;
        disable_enable_Canvas(true);


    }

    public void setShieldOn()
    {
        if (shieldCount > 0)
        {
            if (movable)
            {
                if (!isShieldSkillLocked)
                {
                    shieldUI.GetComponent<Button>().interactable = false;
                    shield.SetActive(true);
                    shieldCount--;
                    PlayerPrefs.SetInt("shieldskillcount", shieldCount);
                }
            }
        }
        shieldText.text = (shieldCount).ToString();
    }

    void _healthSystem()
    {
        if (health == 0)//oldugu zaman cagriliyor
        {
            anim.SetBool("hurt", true);
            movable = false;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    public void setHurt() // lovebar icerisinde cagiriliyor
    {
        hearts[health].SetActive(false);
        health -= 1;
        hurted = true;
        movable = false;
    }

    public void alertForPlayer(bool x)
    {
        anim.SetBool("alert", x);
    }


    private void resetTheGameFromLastCheckpoint() // rewardedvideo başarıyla izlenildiğinde oyuncuyu son checkpointten tekrar başlatır
    {
        //hurted = false;
        giveUp.SetActive(false);
        loveBar LB = GameObject.Find("loveBar").GetComponent<loveBar>();
        LB.timer = LB.setTimeForLoveBar;
        LB.barIMG.fillAmount = 1f;
        for (int a = 1; a <= 5; a++)
        {
            health = a;
            hearts[health].SetActive(true);
        }
        PlayerPrefs.SetInt("rewardbasedvideo", 0);// rewarded ad izleyince 1 olarak kaydediyor

        transform.position = respawnPoint;
        timerIsRunning = true;


        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        GetComponent<Rigidbody2D>().isKinematic = false;
    }


    // Tüm collision işlemleri alt kısımda kotarılır
    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.tag == "palm")
        {
            Vector2 contactVectorNormal = collision.contacts[0].normal;
            float magnt = velocity.magnitude;
            Vector2 newDirection = Vector2.Reflect(velocity.normalized, contactVectorNormal);
            //durma sadece gidemeyecegi yonunde olacak
            velocity.x = magnt * newDirection.x * 0.5f;
            velocity.y = magnt * newDirection.y * 0.5f;

        }

        if (collision.collider.tag == "laser")
        {

            if (shield.activeSelf)
            {

                Vector2 contactVectorNormal = collision.contacts[0].normal;
                float magnt = velocity.magnitude;
                Vector2 newDirection = Vector2.Reflect(velocity.normalized, contactVectorNormal);
                //durma sadece gidemeyecegi yonunde olacak
                velocity.x = magnt * newDirection.x * 0.5f;
                velocity.y = magnt * newDirection.y * 0.5f;
            }           //duvaraDokundu = true;
            else
            {

                if (hasaralabilir)
                {
                    velocity = Vector2.zero;
                    GetComponent<CircleCollider2D>().isTrigger = true; // player olumsuz oluyor?
                    hearts[health].SetActive(false);
                    health -= 1;
                    hurted = true;
                    hurtedSoMakeBombInactive = true;
                    if (PlayerPrefs.GetInt("issoundon") == 1)
                    {
                        audSource.PlayOneShot(clip2);
                    }
                }
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("alertForPlayer"))
                {
                    anim.SetBool("alert", false);
                    anim.SetBool("hurt", true);
                }

            }
        }

        if (collision.collider.tag == "greenBall")
        {

            if (shield.activeSelf)
            {
                objList.Add(collision.gameObject);
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
                sektir = false;
                doNotHitToGreenCanGetDamage = false;

            }
            else
            {
                Vector2 contactVectorNormal = collision.contacts[0].normal;
                Rigidbody2D ballBody = collision.gameObject.GetComponent<Rigidbody2D>();
                float ballBodyRotationSpeed = collision.gameObject.GetComponent<BallMotor>().rotationRadius;
                ballBody.isKinematic = true;
                Vector2 ballSpeed = ballBody.velocity;
                float ballScale = collision.gameObject.GetComponent<Transform>().localScale.x;
                float radiusDifference = ballBody.position.x - body.position.x;
                float ballRadius = ballScale * 350 / 64 / 2;
                float playerRadius = gameObject.GetComponent<Transform>().localScale.x * 175 / 64 / 2;
                float infiltration = ballRadius + playerRadius - Mathf.Abs(radiusDifference);

                //ilk durum player soldan geliyor ball y yönünde hareket ediyor
                if (ballSpeed.y != 0 || ballBodyRotationSpeed != 0)
                {
                    //    Debug.Log("infiltration " + infiltration + "radiusDifference" + radiusDifference);


                    if (infiltration >= 0 && radiusDifference >= 0)
                    {
                        Vector2 rayDirection = new Vector2(-1, 0);
                        Vector2 newPosition = new Vector2((ballBody.position.x - ballRadius - infiltration - playerRadius), body.position.y);
                        /*if (Physics.Raycast(transform.position, rayDirection, 10))
                {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, 2f);
                //Debug.Log("hit " + hit.point + "hit tag" + hit.collider.tag);
                //Debug.Log("newPosition.y " + newPosition.y + "newPosition.x " + newPosition.x);
                if (hit.collider.tag == "palm")
                {
                if (hit.point.y >= newPosition.y || hit.point.x >= newPosition.x)
                {
               //Debug.Log("newPosition2 " + newPosition);
               Vector2 newCorrectedPosition = new Vector2(hit.point.x + 0.2f + playerRadius, newPosition.y);
               body.MovePosition(newCorrectedPosition) ;
               //Debug.Log("newCorrectedPosition " + newCorrectedPosition);

           }
           else
           {
               //Debug.Log("newPosition3 " + newPosition);

               //body.MovePosition(newPosition);
           }
       }
   }*/
                        body.MovePosition(newPosition);



                        //player sagdan geldigi zaman
                    }
                    else if (infiltration >= 0 && radiusDifference < 0)
                    {
                        Vector2 newPosition = new Vector2((ballBody.position.x + ballRadius + infiltration) + playerRadius, transform.position.y);
                        Vector2 rayDirection = new Vector2(1, 0);
                        //Debug.Log("hit " + hit.point + "hit tag" + hit.collider.tag);
                        //Debug.Log("newPosition.y " + newPosition.y + "newPosition.x " + newPosition.x);
                        /*if (Physics.Raycast(transform.position, rayDirection, 10))
                        {
                            RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, 20f);

                            if (hit.collider.tag == "palm")
                            {
                                if (hit.point.y >= newPosition.y || hit.point.x <= newPosition.x)
                                {
                                    Vector2 newCorrectedPosition = new Vector2(newPosition.x - infiltration - playerRadius, newPosition.y);
                                    body.MovePosition(newCorrectedPosition);
                                    //Debug.Log("newCorrectedPosition " + newCorrectedPosition);

                                }

                            }
                        }*/

                        //Debug.Log("newPosition " + newPosition);
                        body.MovePosition(newPosition);



                    }
                }
                if (ballBodyRotationSpeed == 0)
                {
                    velocity.x = contactVectorNormal.x * (Mathf.Abs(ballSpeed.x) + 1) * ballScale * 15;
                    velocity.y = contactVectorNormal.y * (Mathf.Abs(ballSpeed.y) + 1) * ballScale * 15;
                }
                else
                {
                    velocity.x = contactVectorNormal.x * (Mathf.Abs(ballBodyRotationSpeed)) * ballScale * 15;
                    velocity.y = contactVectorNormal.y * (Mathf.Abs(ballBodyRotationSpeed)) * ballScale * 15;
                }


                hasarGormemeTimerBaslat = true;
                if (doNotHitToGreenCanGetDamage)
                {
                    hearts[health].SetActive(false);
                    health -= 1;


                    if (PlayerPrefs.GetInt("issoundon") == 1)
                    {
                        audSource.PlayOneShot(clip2);
                    }

                }
                if (health == 0)
                {
                    hurted = true;
                    movable = false;
                    velocity = Vector2.zero;
                    //Debug.Log("velocity " + velocity);

                }
            }
        }

        if (collision.collider.tag == "yellowBall" || collision.collider.tag == "redEnemy")
        {
            if (shield.activeSelf)
            {
                objList.Add(collision.gameObject);
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());

            }
            else
            {
                if (hasaralabilir)
                {
                    if (PlayerPrefs.GetInt("issoundon") == 1)
                    {
                        audSource.PlayOneShot(clip2);
                    }
                    velocity = Vector2.zero;
                    GetComponent<CircleCollider2D>().isTrigger = true; // player olumsuz oluyor?
                    hearts[health].SetActive(false);
                    health -= 1;
                    hurted = true;
                    hurtedSoMakeBombInactive = true;

                }
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("alertForPlayer"))
                {
                    anim.SetBool("alert", false);
                    anim.SetBool("hurt", true);
                }
            }

        }

    }

    public bool hurtedSoMakeBombInactive;

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "greenBall")
        {
            Rigidbody2D ballBody = collision.gameObject.GetComponent<Rigidbody2D>();
            ballBody.isKinematic = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Respawn")
        {
            collision.gameObject.GetComponent<Animator>().SetBool("startable", true);
            respawnPoint = collision.gameObject.transform.position;
        }



        if (collision.tag == "geçit")

        {

            //Debug.Log("velocity " + velocity);
            if (velocity.x == 0)
            {
                playerVelocityAngle = 0;
            }
            else
            {
                playerVelocityAngle = Mathf.Atan2(velocity.y, velocity.x);
            }
            //Debug.Log("angle " + playerVelocityAngle);
            float speedIcrement = MAX_VELOCITY * 1.3f;

            if (velocity.x > 0)
            {
                velocity.x = speedIcrement * Mathf.Abs(Mathf.Cos(playerVelocityAngle));
            }
            else if (velocity.x < 0)
            {
                velocity.x = -speedIcrement * Mathf.Abs(Mathf.Cos(playerVelocityAngle));
            }
            else
            {
                if (isFaceRight)
                {
                    velocity.x = speedIcrement * Mathf.Abs(Mathf.Cos(playerVelocityAngle));
                }
                else
                {
                    velocity.x = -speedIcrement * Mathf.Abs(Mathf.Cos(playerVelocityAngle));
                }
            }
            if (velocity.y > 0)
            {
                velocity.y = speedIcrement * Mathf.Abs(Mathf.Sin(playerVelocityAngle));
            }
            else if (velocity.y < 0)
            {
                velocity.y = -speedIcrement * Mathf.Abs(Mathf.Sin(playerVelocityAngle));
            }
            else
            {
                velocity.y = speedIcrement * Mathf.Abs(Mathf.Sin(playerVelocityAngle));

            }
            //Debug.Log("velocity " + velocity);
        }


        if (collision.gameObject.name == "QueenYan64")
        {
            hasaralabilir = false;
            GameObject.Find("Right").SetActive(false);
            GameObject.Find("Left").SetActive(false);
            GameObject.Find("Skills").SetActive(false);
            GameObject.Find("pause").SetActive(false);
            GameObject.Find("loveBar").SetActive(false);

            movable = false;
            int _score = int.Parse("" + puan);
            isItEnd = true;
            //_loveBar._fillTheLoveBar();
            _loveBar.GetComponent<loveBar>().enabled = false;
            int currentLevelIndis = SceneManager.GetActiveScene().buildIndex;
            changeSpriteAfterQueen();
            GameObject.Find("Cameras").transform.GetChild(1).GetComponent<Animator>().enabled = true;
            GameObject.Find("Cameras").transform.GetChild(0).GetComponent<parallaxSystemForMainGame>().enabled = false;
            timerIsRunning = false;
            PlayerPrefs.SetFloat("levelTimer_" + SceneManager.GetActiveScene().name, levelTimer);
            int sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
            //Debug.Log("currentLevelIndis " + currentLevelIndis + "sceneCount " + sceneCount);

            //levelindislerde oncesinde 3 scene var bir de 0 dan basladigi icin
            if (currentLevelIndis == (sceneCount - 3))
            {
                PlayerPrefs.SetInt("nextlevelindis", sceneCount - 3);
            }
            else if (currentLevelIndis >= PlayerPrefs.GetInt("nextlevelindis"))
            {
                PlayerPrefs.SetInt("nextlevelindis", currentLevelIndis + 1);
            }
            PlayerPrefs.SetInt("coin_score_" + SceneManager.GetActiveScene().name, level_Icı_Puan);//skor tahtasi icin level ici puan
        }



        if (collision.tag == "coin")
        {
            puan += coinValue; // toplam puan
            level_Icı_Puan += coinValue;
            PlayerPrefs.SetInt("coin_score", puan);//toplam puan hep artiyor
            score_Levelici.text = "" + level_Icı_Puan;
            Vector3 position = new Vector3(Random.Range(-75.0f, 75.0f), Random.Range(-75.0f, 75.0f), 0);
            float scale = Random.Range(0.5f, 1f);
            GameObject clone = (GameObject)Instantiate(coinScorePrefab, coinScorePrefab.transform.position, Quaternion.identity);
            clone.transform.GetChild(0).GetComponent<Text>().text = "+" + coinValue;
            clone.transform.SetParent(GameObject.Find("MainGameCanvas").transform);
            clone.transform.localPosition = position;
            clone.transform.localScale = new Vector3(scale, scale, 0);
            clone.SetActive(true);
            Destroy(clone, 1.2f);

            if (PlayerPrefs.GetInt("issoundon") == 1)
            {
                audSource.PlayOneShot(clip3);
            }

            Destroy(collision.gameObject);
        }


        if (collision.tag == "heart")
        {
            _loveBar._fillTheLoveBar();
            Destroy(collision.gameObject);
        }
    }


    private void changeSpriteAfterQueen()
    {
        GetComponent<Animator>().enabled = false;
        GameObject queenFObj = GameObject.Find("QueenYan64");
        GetComponent<Transform>().localScale = queenFObj.GetComponent<Transform>().localScale;
        GetComponent<SpriteRenderer>().sprite = playerF;
        GetComponent<SpriteRenderer>().flipX = true;
        queenFObj.GetComponent<SpriteRenderer>().sprite = queenF;
        Vector2 newPLayerPositionF = new Vector2(queenFObj.GetComponent<Transform>().position.x - 1, queenFObj.GetComponent<Transform>().position.y + 0.1f);
        body.MovePosition(newPLayerPositionF);

    }


    private void settingBombShieldArea(Vector3 imgSize, float colliderRadius)
    {
        GameObject shield = GameObject.Find("Bombs");

        if (shield != null)
        {

            List<GameObject> child_shield_objects;
            child_shield_objects = new List<GameObject>();
            int child_obj_count = shield.transform.childCount;

            for (int i = 0; i < child_obj_count; i++)
            {
                child_shield_objects.Add(shield.transform.GetChild(i).gameObject);
            }

            foreach (GameObject obj in child_shield_objects)
            {
                obj.transform.GetComponent<CircleCollider2D>().radius = colliderRadius;
                obj.transform.GetChild(0).localScale = imgSize;
            }
        }
    }

    public void cikart()
    {
        GameObject.Find("Cameras").transform.GetChild(1).GetComponent<CinemachineVirtualCamera>().m_Follow = null;
    }

    private void disable_enable_Canvas(bool isCanvasActive)
    {
        if (isCanvasActive)
        {
            canvas.SetActive(true);

        }
        else
        {
            isLeftButtonPressed = false;
            isRightButtonPressed = false;
            isUpButtonPressed = false;
            isDownButtonPressed = false;
            canvas.SetActive(false);

        }
    }

    public void leftButtonPressed()
    {
        isLeftButtonPressed = true;
    }
    public void leftButtonIsNotPressed()
    {
        isLeftButtonPressed = false;
    }
    public void rightButtonPressed()
    {
        isRightButtonPressed = true;
    }
    public void rightButtonIsNotPressed()
    {
        isRightButtonPressed = false;
    }
    public void upButtonPressed()
    {
        isUpButtonPressed = true;
    }
    public void upButtonIsNotPressed()
    {
        isUpButtonPressed = false;
    }
    public void downButtonPressed()
    {
        isDownButtonPressed = true;
    }
    public void downButtonIsNotPressed()
    {
        isDownButtonPressed = false;
    }

}