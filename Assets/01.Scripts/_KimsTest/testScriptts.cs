using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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

    private int speedBtnCount;

    public Slider hpBar;

    //public Button NextWaveBtn;
    [SerializeField]
    private float gameTime;

    //public Button gameEndBtn;
    public Button reloadBtn;
    public Slider bulletAmmo;

    public GameObject turret;
    private ObjectPool objectPool;

    public List<Transform> turretPoses = new List<Transform>();
    public List<GameObject> turretData = new List<GameObject>();
    public List<GameObject> turretType = new List<GameObject>();

    public int turPos = 0;
    public int turType;
    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        objectPool = FindObjectOfType<ObjectPool>();
        //NextWaveBtn.onClick.AddListener(NextWave);

        //gameEndBtn.onClick.AddListener(GameEnd);
        //reloadBtn.onClick.AddListener(()=>
        //{
        //    if (turretPoses[turPos].GetComponent<TurretShooting>())
        //    {
        //        turretPoses[turPos].GetComponent<TurretShooting>().Reload();
        //    }
        //});

        //hp바 세
        hpBar.value = (float)TrainScript.instance.curTrainHp / (float)TrainScript.instance.maxTrainHp;
    }

    // Update is called once per frame
    void Update()
    {
        gameTime = Time.time;
        string result = string.Format("{0:0.0}", gameTime);
        ChangeSpeed();

        if(Input.GetKeyDown(KeyCode.A))
        {
            NextWave();
        }
        
        if(turretPoses[turPos].GetComponent<TurretShooting>())
        {
            bulletAmmo.value = (float)turretPoses[turPos].GetComponent<TurretShooting>().bulAmount / (float)turretPoses[turPos].GetComponent<TurretShooting>().maxBulletAmount;
        }
    }
    public void ChangeSpeed()
    {
        switch (speedBtnCount)
        {
            case 0:
                GameManager.Instance.gameSpeed = 1f;
                break;
            case 1:
                GameManager.Instance.gameSpeed = 2f;
                break;
            case 2:
                GameManager.Instance.gameSpeed = 3f;
                break;
            case 3:
                GameManager.Instance.gameSpeed = 0f;
                break;
        }
    }

    public void NextWave()
    {
        if (SpawnMananger.Instance.round == 0)
        {
            SpawnMananger.Instance.stopSpawn = false;

            GameManager.Instance.goldAmount += 30;

        }
        else
        {
            SpawnMananger.Instance.curTime = SpawnMananger.Instance.roundCurTime;
            GameManager.Instance.goldAmount += (int)(SpawnMananger.Instance.roundCurTime * 0.7f);
        }
    }

    public void Create()
    {
        if (turretPoses[turPos].GetComponent<tesetTurret>().onTurret != true)
        {
            if (GameManager.Instance.goldAmount >= 10)
            {
                GameObject gameInst = objectPool.GetObject(turret);
                gameInst.transform.position = turretPoses[turPos].position;
                gameInst.transform.SetParent(this.gameObject.transform);
                turretPoses[turPos].GetComponent<tesetTurret>().onTurret = true;
                turretData[turPos] = gameInst;
                GameManager.Instance.goldAmount -= 10;
            }
        } 
        else
        {
            InGameUI._instance.upGradePanelRect.DOAnchorPosY(300, 1.5f);
            InGameUI._instance.selectType = turType;
        }

    }
    public void Despawn()
    {
        turretData[turPos].SetActive(false);
    }

    public void ChageMakeTur(GameObject turret)
    {
        GameObject gameInst = objectPool.GetObject(turret);
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
        InGameUI._instance.selectType = turType;
        GameObject gameObject = turretType[num];
        turret = gameObject;
    }
    public void Speeeeeed(int num)
    {
        speedBtnCount = num;
    }

    public void TakeDamageHpBar()
    {
        //Time.deltaTime 옆에 * (TakeDamage) 만큼 곱해줘야함. 생략되어 있음.
        hpBar.value = Mathf.Lerp(hpBar.value, (float)TrainScript.instance.curTrainHp / (float)TrainScript.instance.maxTrainHp, Time.deltaTime);
    }
}
