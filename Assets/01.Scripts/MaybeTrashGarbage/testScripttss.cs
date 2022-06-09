using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class testScripttss : MonoBehaviour
{
    private static testScripttss _instance;
    public static testScripttss Instance
    {
        get
        {
            return _instance;
        }
    }

    private int speedBtnCount = 1;

    public Slider hpBar;

    [Header("NEXTBTN")]
    public Button NextWaveBtn;
    public Image nextWaveImg;
    public Image autoWaveImg;
    public Image textImg;

    [Header("")]
    [SerializeField]
    private float gameTime;

    public Button reloadBtn;
    public Slider bulletAmmo;

    public Image speedImg;
    public Sprite[] speedList;

    public GameObject turret;
    private ObjectPool objectPool;

    public List<Transform> turretPoses = new List<Transform>();
    public List<GameObject> turretData = new List<GameObject>();
    public List<GameObject> turretType = new List<GameObject>();

    GameManager gameManager;
    SpawnMananger spawnMananger;
    InGameUII inGameUI;
    TestTurretDataBase testTurretDataBase;

    public int turPos = 0;
    public int turType;

    private int floor;

    [SerializeField]
    private Image image;

    [SerializeField]
    private Image upGradeImage;

    [SerializeField]
    private Sprite[] imageType;

    [SerializeField]
    private Image[] images;

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        turType = -1;

        gameManager = GameManager.Instance;
        spawnMananger = SpawnMananger.Instance;
        inGameUI = InGameUII._instance;
        testTurretDataBase = TestTurretDataBase.Instance;

        objectPool = FindObjectOfType<ObjectPool>();
        NextWaveBtn.onClick.AddListener(NextWave);

        reloadBtn.onClick.AddListener(Reload);

        //hp바 세
        hpBar.value = (float)TrainScript.instance.curTrainHp / (float)TrainScript.instance.maxTrainHp;

        gameManager.goldAmount += 120;
        
    }

    // Update is called once per frame
    void Update()
    {
        ChangeSpeed();
        gameTime = Time.time;
        string result = string.Format("{0:0.0}", gameTime);
        BulletCheck();
        NextWaveCoolBtn();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (speedBtnCount != 0)
            {
                if (spawnMananger.curTime >= (spawnMananger.roundCurTime * 0.3f))
                {
                    NextWave();
                }
            }
        }

        if (spawnMananger.round > spawnMananger.maxRound)
        {
            inGameUI.warningtxt.color = new Color(1, 0f, 0, 1);
            inGameUI.warningIcon.color = new Color(1, 0f, 0, 1);
            inGameUI.warningtxt.GetComponent<Text>().text = "Press \"x\"";
        }

        if (Input.GetKeyDown(KeyCode.Period))
        {
            Speeeeeed();
        }

        else if (Input.GetKeyDown(KeyCode.Comma))
        {
            Minuspeed();
        }
    }

    public void Reload()
    {

        if (turPos != -1 && turretData[turPos].TryGetComponent(out Turret trshot))
        {
            trshot.Reload();
        }
    }

    public void ChangeSpeed()
    {
        switch (speedBtnCount)
        {
            case 0:
                gameManager.gameSpeed = 0f;
                speedImg.sprite = speedList[0];
                break;
            case 1:
                gameManager.gameSpeed = 1f;
                speedImg.sprite = speedList[1];
                break;
            case 2:
                gameManager.gameSpeed = 2f;
                speedImg.sprite = speedList[2];
                break;
            case 4:
                gameManager.gameSpeed = 4f;
                speedImg.sprite = speedList[3];
                break;
        }
    }

    public void NextWave()
    {
        if (spawnMananger.round == 1)
        {
            spawnMananger.stopSpawn = false;
        }
        else
        {
            spawnMananger.curTime = spawnMananger.roundCurTime;
            gameManager.goldAmount += (int)(spawnMananger.roundCurTime * 0.7f);
            inGameUI.CreateMonjeyTxt((int)(spawnMananger.roundCurTime * 0.7f));
        }
    }
    public void NextWaveCoolBtn()
    {
        if (speedBtnCount != 0)
        {
            if (spawnMananger.curTime >= (spawnMananger.roundCurTime * 0.3f))
            {
                NextWaveBtn.interactable = true;
                textImg.gameObject.SetActive(true);
            }
            else
            {
                NextWaveBtn.interactable = false;
                textImg.gameObject.SetActive(false);
            }
        }
        nextWaveImg.fillAmount = spawnMananger.curTime / spawnMananger.roundCurTime;
        autoWaveImg.fillAmount = spawnMananger.curTime / (spawnMananger.roundCurTime * 0.3f);
    }

    public void Create(GameObject turret)
    {
        if (turretPoses[turPos].TryGetComponent(out tesetTurret tT))
        {
            if (tT.onTurret != true)
            {
                GameObject gameInst = objectPool.GetObject(turret);
                //gameInst.GetComponent<Turret>().turCount = turPos;
                //gameInst.GetComponent<Turret>().turType = turType;
                gameInst.transform.position = turretPoses[turPos].position;
                gameInst.transform.SetParent(TrainScript.instance.transform.Find("Turrets"));
                tT.onTurret = true;
                turretData[turPos] = gameInst;
                gameManager.goldAmount -= turretData[turPos].GetComponent<Turret>().turretPrice;
                testTurretDataBase.createPrice += turretData[turPos].GetComponent<Turret>().turretPrice;
                inGameUI.ShowTurPrice();
            }
            else
            {
                inGameUI.upGradePanelRect.DOAnchorPosX(-200, 1.5f).SetUpdate(true);
                SelectTurret();
            }
        }
        inGameUI.upGradePanelRect.DOAnchorPosX(-200, 1.5f).SetUpdate(true);
        SelectTurret();
    }
    public void Despawn()
    {
        turretPoses[turPos].TryGetComponent(out tesetTurret tT);

        turretData[turPos].SetActive(false);

        turretData[turPos] = turretPoses[turPos].gameObject;
        tT.onTurret = false;

        UnSelectTurret();
    }

    public void ChageMakeTur(GameObject turret)
    {
        GameObject gameInst = objectPool.GetObject(turret);
        //gameInst.GetComponent<Turret>().turCount = turPos;
        gameInst.transform.position = turretPoses[turPos].position;
        gameInst.transform.SetParent(TrainScript.instance.transform.Find("Turrets"));
        turretData[turPos].SetActive(false);
        turretData[turPos] = gameInst;
        gameManager.goldAmount -= turretData[turPos].GetComponent<Turret>().turretPrice;
    }

    public void GameEnd()
    {
        TrainManager.instance.curTrainCount -= 100000;
    }

    public void ChangeTur(int num)
    {
        //TurSelect();

        turType = num;
        GameObject gameObject = turretType[num];
        turret = gameObject;

    }

    public void TurSelect()
    {
        for (int i = 0; i < images.Length; i++)
        {
            if (images[i].color.a == 0)
            {
                images[i].color = new Color(1, 1, 0, 0);
            }

            else
            {
                images[i].color = new Color(1, 1, 0, 1);
            }
        }
    }

    public void TurCancle()
    {
        for (int i = 0; i < images.Length; i++)
        {
            if (images[i].color.a == 0)
            {
                images[i].color = new Color(1, 1, 1, 0);
            }

            else
            {
                images[i].color = new Color(1, 1, 1, 1);
            }
        }
    }

    public void Speeeeeed()
    {
        if (speedBtnCount != 0)
        {
            speedBtnCount *= 2;
            if (speedBtnCount == 8)
                speedBtnCount = 0;
        }
        else
        {
            speedBtnCount++;
        }

        //gameSpeedText.text = ""+speedBtnCount;
    }
    public void Minuspeed()
    {
        if (speedBtnCount != 0)
        {
            speedBtnCount /= 2;
            if (speedBtnCount < 1)
            {
                speedBtnCount = 0;
            }
        }
        else
        {
            speedBtnCount = 4;
        }
    }

    public void TakeDamageHpBar()
    {
        //Time.deltaTime 옆에 * (TakeDamage) 만큼 곱해줘야함. 생략되어 있음.
        hpBar.value = Mathf.Lerp(hpBar.value, (float)TrainScript.instance.curTrainHp / (float)TrainScript.instance.maxTrainHp, Time.deltaTime);
    }

    public void BulletCheck()
    {
        if (turPos != -1 && turretData[turPos].GetComponent<TurretShooting>())
        {
            bulletAmmo.value = (float)turretData[turPos].GetComponent<TurretShooting>().bulAmount / (float)turretData[turPos].GetComponent<TurretShooting>().maxBulletAmount;
        }
    }

    public void SelectTurret()
    {
        for (int i = 0; i < turretPoses.Count; i++)
        {
            if (turretPoses[i] != turretPoses[turPos])
            {
                turretPoses[i].GetChild(0).gameObject.SetActive(false);
            }

            else
            {
                turretPoses[turPos].GetChild(0).gameObject.SetActive(true);
            }
        }
    }


    public void UnSelectTurret()
    {
        for (int i = 0; i < turretPoses.Count; i++)
        {
            turretPoses[i].GetChild(0).gameObject.SetActive(false);
        }

        TurCancle();
    }
}
