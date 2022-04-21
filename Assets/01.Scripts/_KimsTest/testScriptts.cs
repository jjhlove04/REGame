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
    InGameUI inGameUI;

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
        gameManager = GameManager.Instance;
        spawnMananger = SpawnMananger.Instance;
        inGameUI = InGameUI._instance;

        objectPool = FindObjectOfType<ObjectPool>();
        NextWaveBtn.onClick.AddListener(NextWave);

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
        NextWaveCoolBtn();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(speedBtnCount != 0)
            {
                if (SpawnMananger.Instance.curTime >= (SpawnMananger.Instance.roundCurTime * 0.3f))
                {
                    NextWave();
                }
            }
        }

        if(SpawnMananger.Instance.round > SpawnMananger.Instance.maxRound)
        {
            inGameUI.warningTxt.color = new Color(1, 0f, 0, 1);
            inGameUI.warningTxt.text = "Press \"p\"";
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeTur(0);
            inGameUI.NewSelect(0);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeTur(1);
            inGameUI.NewSelect(1);
        }

        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeTur(2);
            inGameUI.NewSelect(2);
        }

        else if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeTur(3);
            inGameUI.NewSelect(3);
        }

        else if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            ChangeTur(4);
            inGameUI.NewSelect(4);
        }

        else if (Input.GetKeyDown(KeyCode.P))
        {
            inGameUI.PresetBtn();
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
                NextWaveBtn.interactable = false;
                break;
            case 1:
                gameManager.gameSpeed = 1f;
                speedImg.sprite = speedList[1];
                NextWaveBtn.interactable = true;
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

            gameManager.goldAmount += 30;

        }
        else
        {
            spawnMananger.curTime = spawnMananger.roundCurTime;
            gameManager.goldAmount += (int)(spawnMananger.roundCurTime * 0.7f);
        }
    }
    public void NextWaveCoolBtn()
    {
        if (speedBtnCount != 0)
        {
            if (SpawnMananger.Instance.curTime >= (SpawnMananger.Instance.roundCurTime * 0.3f))
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
        nextWaveImg.fillAmount = SpawnMananger.Instance.curTime / SpawnMananger.Instance.roundCurTime;
        autoWaveImg.fillAmount = SpawnMananger.Instance.curTime / (SpawnMananger.Instance.roundCurTime * 0.3f);
    } 

    public void Create(GameObject turret)
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
        inGameUI.upGradePanelRect.DOAnchorPosX(-200, 1.5f).SetUpdate(true);
        SelectTurret();
    }
    public void Despawn()
    {
        turretData[turPos].SetActive(false);
    }

    public void ChageMakeTur(GameObject turret)
    {
        GameObject gameInst = objectPool.GetObject(turret);
        gameInst.GetComponent<Turret>().turCount = turPos;
        gameInst.GetComponent<Turret>().turType = InGameUI._instance.selectType;
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
        TurSelect();

        turType = num;
        inGameUI.selectType = turType;
        GameObject gameObject = turretType[num];
        turret = gameObject;

        inGameUI.OpenPresetBtn();
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

        NextUpgrade();
    }


    public void UnSelectTurret()
    {
        for (int i = 0; i < turretPoses.Count; i++)
        {
            turretPoses[i].GetChild(0).gameObject.SetActive(false);
        }

        TurCancle();
    }

    public void NextUpgrade()
    {
        int[] imageCount = TestTurretDataBase.Instance.GetTurretImageCount();

        if (imageCount[0] != 100)
        {
            image.sprite = imageType[imageCount[0]];

            if (imageCount[1] != 100)
            {
                if (imageCount[0] == 5 && imageCount[0] == 9 && imageCount[0] == 14)
                {
                    upGradeImage.sprite = null;
                }

                else
                {
                    upGradeImage.sprite = imageType[imageCount[1]];
                }
            }

            else
            {
                upGradeImage.sprite = null;
            }
        }

        else
        {
            upGradeImage.sprite = null;
        }
    }
}
