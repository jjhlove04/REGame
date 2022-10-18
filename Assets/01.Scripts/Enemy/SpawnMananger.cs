using System.Collections.Generic;
using UnityEngine;

public class SpawnMananger : MonoBehaviour
{
    public static SpawnMananger Instance;
    private static SpawnMananger _insstance
    {
        get { return Instance; }
    }

    public delegate void Spawn(int i);

    public SpawnInfo Info;
    
    private ObjectPool _objPool;

    public List<GameObject> enemyList = new List<GameObject>();

    public Spawn spawn;

    public float curTime; 

    public int round;
    public int maxRound;

    public bool stopSpawn = false;

    private GameObject playerSpanwer;


    private float levelTime = 60;
    private void Awake()
    {
        
        foreach(var item in enemyList)
        {
            Debug.Log(item.name);
        }
        
        Instance = this;

        round = 1;

        spawn = new Spawn((round)=> { });
    }
    private void Start()
    {
        _objPool = FindObjectOfType<ObjectPool>();
        curTime = Info.roundCurTime;
        stopSpawn = true;

        playerSpanwer = transform.Find("PlayerSpawn").gameObject;
    }

    private void Update()
    {
        if (!stopSpawn)
        {
            curTime += Time.deltaTime;

            if (curTime >= Info.roundCurTime)
            {
                
                spawn(round);

                curTime = 0;
                round++;
                //roundCurTime = roundCurTime * 1.01f;

                if (round > maxRound)
                {
                    stopSpawn = true;
                }

                TestTurretDataBase.Instance.round = round;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerSpanwer.SetActive(!playerSpanwer.activeSelf);
        }

        if (round % 22 == 0 && round != 0)
        {
            GameManager.Instance.gameLevel++;
        }

    }




}
