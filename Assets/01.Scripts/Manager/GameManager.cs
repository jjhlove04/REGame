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

    private int goldAmount = 0;
    public int GoldAmount
    {
        get { return goldAmount; }

        set { goldAmount = (int)(value * goldIncrease); }
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

    private float expIncrease = 1;
    private float goldIncrease = 1;


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
            GoldAmount += 100;
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

        if (annuityCoolTime >= annuityCurTime)
        {
            GoldAmount += annuityGoldAmount;
        }
    }

    public void OnNewsOfVictory()
    {
        onNewsOfVictory = true;

        turrets = TurretManager.Instance.turrets;
    }

    private void NewsOfVictory()
    {
        Turret[] turretArr = new Turret[0];

        for (int i = 0; i < turretAmount; i++)
        {
            Turret turret = RandomTurret(turretArr);

            if (turret != null)
            {
                turretArr[i] = turret;
            }
        }

        for (int j = 0; j < turretArr.Length; j++)
        {
            turretArr[j].OnNewsOfVictory();
        }
    }

    private Turret RandomTurret(Turret[] turretArr)
    {
        Turret turret = new Turret();

        if (turrets.transform.childCount >0)
        {
            turret = turrets.transform.GetChild(Random.Range(0, turrets.transform.childCount))?.GetComponent<Turret>();

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

    public void MoreBigWallet(float expIncrease)
    {
        this.expIncrease += expIncrease;
    }

    public void LearningMagnifyingGlass(float goldIncrease)
    {
        this.goldIncrease += goldIncrease;
    }
}