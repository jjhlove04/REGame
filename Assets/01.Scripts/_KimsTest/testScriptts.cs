using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Button speedBtn;
    public Text speedTxt;
    private int speedBtnCount;

    public Button NextWaveBtn;
    [SerializeField]
    private float gameTime;

    //

    public Button spawnBtn;
    public Button gameEndBtn;

    public GameObject turret;
    private ObjectPool objectPool;

    public List<Transform> turretPoses = new List<Transform>();
    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        objectPool = FindObjectOfType<ObjectPool>();
        speedBtn.onClick.AddListener(ChangeSpeed);
        NextWaveBtn.onClick.AddListener(NextWave);

        spawnBtn.onClick.AddListener(Create);
        Debug.Log("asd");
        gameEndBtn.onClick.AddListener(GameEnd);
    }

    // Update is called once per frame
    void Update()
    {
        gameTime = Time.time;
        string result = string.Format("{0:0.0}", gameTime);
    }
    public void ChangeSpeed()
    {
        speedBtnCount++;
        switch (speedBtnCount)
        {
            case 0:
                GameManager.Instance.gameSpeed = 1f;
                speedTxt.text = GameManager.Instance.gameSpeed + "X";
                break;
            case 1:
                GameManager.Instance.gameSpeed = 1.5f;
                speedTxt.text = GameManager.Instance.gameSpeed + "X";
                break;
            case 2:
                GameManager.Instance.gameSpeed = 2f;
                speedTxt.text = GameManager.Instance.gameSpeed + "X";
                break;
            case 3:
                GameManager.Instance.gameSpeed = 4f;
                speedTxt.text = GameManager.Instance.gameSpeed + "X";
                speedBtnCount = -1;
                break;
            default:
                break;
        }
    }

    public void NextWave()
    {
        SpawnMananger.Instance.curTime = SpawnMananger.Instance.roundCurTime;
    }

    public void Create()
    {
        int count = UnityEngine.Random.Range(0, turretPoses.Count);

        GameObject gameInst = objectPool.GetObject(turret);
        gameInst.transform.position = turretPoses[count].position;
        gameInst.transform.SetParent(this.gameObject.transform);
        Debug.Log("asd");

    }

    public void GameEnd()
    {
        TrainManager.instance.curTrainCount -= 100000;
    }
}
