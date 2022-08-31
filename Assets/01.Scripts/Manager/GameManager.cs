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

    public delegate void getGoldEvent();

    public getGoldEvent getGold;

    public float gameSpeed = 1f;

    public int turretPtice = 10;

    private int goldAmount = 0;
    public int GoldAmount
    {
        get { return goldAmount; }

        set 
        {
            if (value > goldAmount)
            {
                goldAmount = (int)(value * goldIncrease);
            }

            else
            {
                goldAmount = value;
            }

            getGold();
        }
    }

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

            expAmount = value * expIncrease;
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
            trainLevel = value;

            activationCoefficient -= 0.01f;   
        }
    }


    private bool annuity = false;
    private int annuityCoolTime = 8;
    private float annuityCurTime = 0;
    private int annuityGoldAmount = 1;

    public bool onNewsOfVictory = false;
    private GameObject turrets; 
    private int turretAmount = 2;
    private int onNewsOfVictoryTime = 4;

    private TrainScript trainScript;
    private bool vamPireTeeth = false;

    private float expIncrease = 1;
    private float goldIncrease = 1;

    private float activationCoefficient = 0.5f;


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
        if (state == State.Play)
        {
            Time.timeScale = gameSpeed;
        }

        else if (state == State.End)
        {
            SceneManager.LoadScene("TitleScene");
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            state = State.End;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            GoldAmount += 50;
            expAmount += 6;
        }

        if (annuity)
        {
            Annuity();
        }
    }

    public void OnAnnuity()
    {
        annuity = true;
    }

    private void Annuity()
    {
        annuityCurTime += Time.deltaTime;

        if (annuityCoolTime <= annuityCurTime)
        {
            annuityCurTime = 0;

            GoldAmount += annuityGoldAmount;
        }
    }

    public void OnNewsOfVictory()
    {
        if (onNewsOfVictory)
        {
            turretAmount++;
            onNewsOfVictoryTime++;
        }

        onNewsOfVictory = true;
    }

    public void NewsOfVictory()
    {
        turrets = TurretManager.Instance.turrets;

        int turCount = 0;

        Turret[] turretArr = new Turret[0];

        if (turretAmount <= turrets.transform.childCount)
        {
            turretArr = new Turret[turretAmount];

            for (int i = 0; i < turretAmount; i++)
            {
                Turret turret = RandomTurret(turretArr);

                if (turret != null)
                {
                    turretArr[turCount] = turret;

                    turCount++;
                }
            }
        }

        else 
        {
            turretArr = new Turret[turrets.transform.childCount];

            for (int i = 0; i < turrets.transform.childCount; i++)
            {
                Turret turret = RandomTurret(turretArr);

                if (turret != null)
                {
                    turretArr[turCount] = turret;

                    turCount++;
                }
            }
        }

        for (int j = 0; j < turretArr.Length; j++)
        {
            turretArr[j].OnNewsOfVictory(onNewsOfVictoryTime);
        }
    }

    private Turret RandomTurret(Turret[] turretArr)
    {                                    
        if (turrets.transform.childCount >0)
        {
            Turret turret = turrets.transform.GetChild(Random.Range(0, turrets.transform.childCount))?.GetComponent<Turret>();

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
                        turret = RandomTurret(turretArr);
                    }
                }
            }

            return turret;
        }

        return null;
    }

    public void OnVamPireTeeth()
    {
        vamPireTeeth = true;
    }

    private void VamPireTeeth()
    {
        trainScript.CurTrainHp += 3;
    }

    public void LearningMagnifyingGlass(float expIncrease)
    {
        this.expIncrease += expIncrease;
    }

    public void MoreBigWallet(float goldIncrease)
    {
        this.goldIncrease += goldIncrease;
    }

    public float ActivationCoefficient(float percent)
    {
        return percent * activationCoefficient;
    }
}