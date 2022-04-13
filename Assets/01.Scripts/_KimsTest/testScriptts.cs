using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class testScriptts : MonoBehaviour
{
    private static testScriptts _instance;
    public static testScriptts Instance
    {
        get
        {
            return _instance;
        }
    }

    private int speedBtnCount = 1;

    public Slider hpBar;

    public Button NextWaveBtn;
    [SerializeField]
    private float gameTime;

    //public Button gameEndBtn;
    public Button reloadBtn;
    public Slider bulletAmmo;

    //public TextMeshProUGUI gameSpeedText;
    public Image speedImg;
    public Sprite[] speedList;

    public GameObject turret;
    private ObjectPool objectPool;

    public List<Transform> turretPoses = new List<Transform>();
    public List<GameObject> turretData = new List<GameObject>();
    public List<GameObject> turretType = new List<GameObject>();

    GameManager gameManager;
    SpawnMananger spawnMananger;
    InGameUI inGameUI;

    public int turPos = 0;
    public int turType;
    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        spawnMananger = SpawnMananger.Instance;
        inGameUI = InGameUI._instance;

        objectPool = FindObjectOfType<ObjectPool>();
        NextWaveBtn.onClick.AddListener(NextWave);

        //gameEndBtn.onClick.AddListener(GameEnd);
        reloadBtn.onClick.AddListener(Reload);

        //hp바 세
        hpBar.value = (float)TrainScript.instance.curTrainHp / (float)TrainScript.instance.maxTrainHp;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeSpeed();
        gameTime = Time.time;
        string result = string.Format("{0:0.0}", gameTime);
        BulletCheck();

        if (Input.GetKeyDown(KeyCode.A))
        {
            NextWave();
        }
    }

    public void Reload()
    {
        if (turPos != -1 && turretData[turPos].TryGetComponent(out TurretShooting trshot))
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
        if (spawnMananger.round == 0)
        {
            spawnMananger.stopSpawn = false;

            gameManager.goldAmount += 30;

        }
        else
        {
            spawnMananger.curTime = spawnMananger.roundCurTime;
            gameManager.goldAmount += (int)(spawnMananger.roundCurTime * 0.7f);
        }
    }

    public void Create()
    {
        if (turretPoses[turPos].TryGetComponent(out tesetTurret tT))
        {
            if (tT.onTurret != true)
            {
                if (gameManager.goldAmount >= 10)
                {
                    GameObject gameInst = objectPool.GetObject(turret);
                    gameInst.GetComponent<Turret>().turCount = turPos;
                    gameInst.GetComponent<Turret>().turType = turType;
                    gameInst.transform.position = turretPoses[turPos].position;
                    gameInst.transform.SetParent(this.gameObject.transform);
                    tT.onTurret = true;
                    turretData[turPos] = gameInst;
                    gameManager.goldAmount -= 10;
                }
                else
                {
                    inGameUI.warningTxt.color = new Color(1, 0.8f, 0, 1);
                    inGameUI.warningTxt.text = "Not Enough Gold";
                }
            }
            else
            {
                inGameUI.upGradePanelRect.DOAnchorPosX(-200, 1.5f).SetUpdate(true);
                SelectTurret();
                inGameUI.selectType = turType;
            }
        } 
    }
    public void Despawn()
    {
        turretData[turPos].SetActive(false);
    }

    public void ChageMakeTur(GameObject turret)
    {
        GameObject gameInst = objectPool.GetObject(turret);
        gameInst.GetComponent<Turret>().turCount = turPos;
        gameInst.transform.position = turretPoses[turPos].position;
        gameInst.transform.SetParent(this.gameObject.transform);
        Despawn();
        turretData[turPos] = gameInst;
    }

    public void GameEnd()
    {
        TrainManager.instance.curTrainCount -= 100000;
    }

    public void ChangeTur(int num)
    {
        turType = num;
        inGameUI.selectType = turType;
        GameObject gameObject = turretType[num];
        turret = gameObject;
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

    public void TakeDamageHpBar()
    {
        //Time.deltaTime 옆에 * (TakeDamage) 만큼 곱해줘야함. 생략되어 있음.
        hpBar.value = Mathf.Lerp(hpBar.value, (float)TrainScript.instance.curTrainHp / (float)TrainScript.instance.maxTrainHp, Time.deltaTime);
    }

    public void BulletCheck()
    {
        if (turPos != -1 && turretData[turPos].GetComponent<TurretShooting>() )
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
    }
}
