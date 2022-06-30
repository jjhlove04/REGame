using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get { return instance; }
    }

    public enum State
    {
        Ready,
        Play,
        Stop,
        End
    }
    EnemyData enemyStat;

    public State state;

    [SerializeField]
    private float gameTime = 0f;
    public float gameSpeed = 1f;

    public int turretPtice = 10;

    public int goldAmount = 0;
    private float expAmount = 0;
    public float ExpAmount
    {
        get { return expAmount; }
        set 
        {
            if (vamPireTeeth)
            {
                VamPireTeeth();
            }

            expAmount = value; 
        }
    }
    public float maxExp = 0;

    public int gameLevel = 1;
    public int trainLevel = 1;
    public int TrainLevel
    {
        get { return trainLevel; }
        set 
        {
            if (onNewsOfVictory)
            {
                NewsOfVictory();
            }

            trainLevel = value; 
        }
    }


    private bool annuity = false;
    private int annuityCoolTime = 8;
    private float annuityCurTime = 0;
    private int annuityGoldAmount = 1;

    private bool onNewsOfVictory = false;
    private GameObject turrets;
    private int turretAmount = 2;

    private TrainScript trainScript;
    private bool vamPireTeeth = false;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        trainScript = TrainScript.instance;
    }

    private void OnDestroy()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        gameTime = Time.unscaledTime;
        gameTime++;
        if (state == State.Play)
        {
            Time.timeScale = gameSpeed;
        }

        if (Input.GetKeyDown(KeyCode.X))
        { 
            state = State.End;
        }

        if(Input.GetKeyDown(KeyCode.M))
        {
            goldAmount += 100;
            expAmount += 4;
        }

        if (annuity)
        {
            Annuity();
        }

        if (onNewsOfVictory)
        {
            NewsOfVictory();
        }
    }

    public void OnAnnuity()
    {
        annuity = true;
    }

    private void Annuity()
    {
        annuityCurTime += Time.deltaTime;

        if(annuityCoolTime >= annuityCurTime)
        {
            goldAmount += annuityGoldAmount;
        }
    }

    public void OnNewsOfVictory()
    {
        onNewsOfVictory = true;

        turrets = TurretManager.Instance.turrets;
    }

    private void NewsOfVictory()
    {
        Turret[] turretArr = new Turret[turretAmount];

        for (int i = 0; i < turretAmount; i++)
        {
            turretArr[i] = RandomTurret(turretArr);
        }

        for (int j = 0; j < turretArr.Length; j++)
        {
            turretArr[j].OnNewsOfVictory();
        }
    }

    private Turret RandomTurret(Turret[] turretArr)
    {
        Turret turret = new Turret();

        turret = turrets.transform.GetChild(Random.Range(0, turrets.transform.childCount)).GetComponent<Turret>();

        for (int j = 0; j < turretArr.Length; j++)
        {
            if (turretArr[j] != null)
            {
                if (turretArr[j] != turret)
                {
                    return turret;
                }

                else
                {
                    turret=RandomTurret(turretArr);
                }
            }
        }

        return turret;
    }

    public void OnVamPireTeeth()
    {
        vamPireTeeth = true;
    }

    private void VamPireTeeth()
    {
        trainScript.CurTrainHp += 3;
    }
}