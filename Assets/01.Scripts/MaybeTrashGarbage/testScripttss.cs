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

    //public Slider hpBar;

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
    TrainInfo trainInfo;


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

    public int startGold;

    private TrainScript trainScript;

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        turType = -1;

        trainScript = TrainScript.instance;
        gameManager = GameManager.Instance;
        spawnMananger = SpawnMananger.Instance;
        inGameUI = InGameUII._instance;

        objectPool = FindObjectOfType<ObjectPool>();
        NextWaveBtn.onClick.AddListener(NextWave);

        //reloadBtn.onClick.AddListener(Reload);

        //hp�� ��
        //hpBar.value = (float)TrainScript.instance.CurTrainHp / (float)TrainScript.instance.traininfo.trainMaxHp;

        if (trainScript.traininfo.trainCount == 1)
        {
            startGold = 120;
        }
        else if (trainScript.traininfo.trainCount == 2)
        {
            startGold = 240;
        }
        else
        {
            startGold = 360;
        }

        gameManager.GoldAmount += startGold;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeSpeed();
        gameTime = Time.time;
        string result = string.Format("{0:0.0}", gameTime);
        NextWaveCoolBtn();
        if (spawnMananger.round == 1)
        {
            if (speedBtnCount != 0)
            {
                if (spawnMananger.curTime >= (spawnMananger.Info.roundCurTime * 0.3f))
                {
                    NextWave();
                }
            }
        }

        if (spawnMananger.round > spawnMananger.maxRound)
        {

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
            spawnMananger.curTime = spawnMananger.Info.roundCurTime;
            gameManager.GoldAmount += (int)(spawnMananger.Info.roundCurTime * 0.7f);
            inGameUI.CreateMonjeyTxt((int)(spawnMananger.Info.roundCurTime * 0.7f));
        }
    }
    public void NextWaveCoolBtn()
    {
        if (speedBtnCount != 0)
        {
            if (spawnMananger.curTime >= (spawnMananger.Info.roundCurTime * 0.3f))
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
        nextWaveImg.fillAmount = spawnMananger.curTime / spawnMananger.Info.roundCurTime;
        autoWaveImg.fillAmount = spawnMananger.curTime / (spawnMananger.Info.roundCurTime * 0.3f);
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
                gameInst.transform.SetParent(trainScript.transform.Find("Turrets"));
                tT.onTurret = true;

                turretData[turPos] = gameInst;
                gameManager.GoldAmount -= turretData[turPos].GetComponent<Turret>().turretPrice;

                gameManager.pticelistNum++;
                gameManager.turretPtice = gameManager.pticeList[gameManager.pticelistNum];

                inGameUI.CreateOutMoney(turretData[turPos].GetComponent<Turret>().turretPrice);
                inGameUI.ShowTurPrice();
            }
            else
            {
                SelectTurret();
            }
        }

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
        gameInst.transform.SetParent(trainScript.transform.Find("Turrets"));
        turretData[turPos].SetActive(false);
        turretData[turPos] = gameInst;
        gameManager.GoldAmount -= turretData[turPos].GetComponent<Turret>().turretPrice;
        inGameUI.CreateOutMoney(turretData[turPos].GetComponent<Turret>().turretPrice);
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