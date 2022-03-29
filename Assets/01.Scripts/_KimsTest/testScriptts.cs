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
   // public Button reloadBtn;

    public GameObject turret;
    private ObjectPool objectPool;

    public List<Transform> turretPoses = new List<Transform>();
    public List<GameObject> turretType = new List<GameObject>();

    public int turPos = 0;
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
        //reloadBtn.onClick.AddListener(turretPoses[turPos].GetComponent<tesetTurret>().Reload);

        //hp바 세
        hpBar.value = (float)TrainScript.instance.curTrainHp / (float)TrainScript.instance.maxTrainHp;
        //speedBtn.onClick.AddListener(ChangeSpeed);
        hpBar.value = (float)TrainScript.instance.curTrainHp / (float)TrainScript.instance.maxTrainHp;
    }

    // Update is called once per frame
    void Update()
    {
        gameTime = Time.time;
        string result = string.Format("{0:0.0}", gameTime);
        ChangeSpeed();
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
        SpawnMananger.Instance.curTime = SpawnMananger.Instance.roundCurTime;
    }

    public void Create()
    {
        if (turretPoses[turPos].GetComponent<tesetTurret>().onTurret != true)
        {
            Debug.Log(turPos);
            GameObject gameInst = objectPool.GetObject(turret);
            gameInst.transform.position = turretPoses[turPos].position;
            gameInst.transform.SetParent(this.gameObject.transform);
            turretPoses[turPos].GetComponent<tesetTurret>().onTurret = true;
        } 
        else
        {
            InGameUI._instance.upGradePanelRect.DOAnchorPosY(300, 1.5f);
            
        }

    }

    public void GameEnd()
    {
        TrainManager.instance.curTrainCount -= 100000;
    }

    public void ChangeTur(int num)
    {
        GameObject gameObject = turretType[num];
        turret = gameObject;
    }
    public void Speeeeeed(int num)
    {
        speedBtnCount = num;
    }

    public void Despawn()
    {

    }
    public void TakeDamageHpBar()
    {
        //Time.deltaTime 옆에 * (TakeDamage) 만큼 곱해줘야함. 생략되어 있음.
        hpBar.value = Mathf.Lerp(hpBar.value, (float)TrainScript.instance.curTrainHp / (float)TrainScript.instance.maxTrainHp, Time.deltaTime);
    }
}
