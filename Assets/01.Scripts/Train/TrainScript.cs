using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainScript : MonoBehaviour
{
    public static TrainScript instance { get; private set; }

    public TrainInfo traininfo;

    public float curTrainHp = 50000; //0���Ϸ� ����߸��� ����!

    public float curTrainShield = 10000;
    public int dieEnemy = 0;

    private float hpCheck =70;

    private float initRoomHp;
    private float roomHp;

    private float smokeHp;

    [HideInInspector]
    public bool destroy = false;

    [SerializeField]
    private float fireDamage;

    [SerializeField]
    private float drillDamage;

    [SerializeField]
    private float guardianDamage;

    [SerializeField]
    private float humanoidDamage;

    [SerializeField]
    private float roketDamage;

    private TrainHit trainhit;

    Dictionary<EnemyType, EnemyData> dicEnemydata = new Dictionary<EnemyType, EnemyData>();

    private TrainManager trainManager;

    private void Awake()
    {
        instance = this;

        EnemyDataInit();
    }

    private void OnEnable()
    {
        trainManager = GetComponent<TrainManager>();

        trainManager.CreateTrainPrefab(traininfo.trainCount);

        curTrainHp = traininfo.trainMaxHp;
        curTrainShield = traininfo.trainMaxShield;
        roomHp = traininfo.trainMaxHp / trainManager.curTrainCount;
        smokeHp = roomHp / 10;
        initRoomHp = roomHp;


    }

    private void Start()
    {
        trainhit = GetComponentInChildren<TrainHit>();
    }

    private void Update()
    {
        //if (destroy)
        //{
        //    trainManager.KeepOffTrain();
        //}
        FixShield();
    }

    private void EnemyDataInit()
    {
        dicEnemydata.Add(EnemyType.Fire, Resources.Load<EnemyData>("Fire"));
        dicEnemydata.Add(EnemyType.Drill, Resources.Load<EnemyData>("Drill"));
        dicEnemydata.Add(EnemyType.Guardian, Resources.Load<EnemyData>("Guardian"));
        dicEnemydata.Add(EnemyType.HumanoidRig, Resources.Load<EnemyData>("HumanoidRig"));
        dicEnemydata.Add(EnemyType.Roket, Resources.Load<EnemyData>("Roket"));
    }

    public void DestroyTrain()
    {
        StartCoroutine(Destroy());
    }

    public void SmokeTrain()
    {
        if (hpCheck * traininfo.trainMaxHp / 100 >= curTrainHp)
        {
            switch (hpCheck)
            {
                case 70:
                    trainManager.OnSmoke();
                    hpCheck = 50;
                    break;
                case 50:
                    trainManager.OnSmoke();
                    hpCheck = 35;
                    break;
                case 35:
                    trainManager.OnSmoke();
                    hpCheck = 20;
                    break;
                case 20:
                    trainManager.OnBlackSmoke();
                    hpCheck = 15;
                    break;
                case 15:
                    trainManager.OnFire();
                    hpCheck = 5;
                    break;
                case 5:
                    trainManager.OnFire();
                    hpCheck = 0;
                    break;
                case 0:
                    hpCheck = 0;

                    if (curTrainHp <= 0)
                    {
                        DestroyTrain();
                    }
                    break;
            }
        }
    }

    public void FixHP()
    {
        curTrainHp = traininfo.trainMaxHp;
    }
    public void FixShield()
    {
        if (curTrainShield < traininfo.trainMaxShield)
        {
            if (dieEnemy >= 10)
            {
                curTrainShield++;
                dieEnemy = 0;

            }
        }
    }

    public void Damage(float damage)
    {
        curTrainShield -= damage;
        if (curTrainShield <= 0)
        {
            curTrainHp -= damage;
            testScripttss.Instance.TakeDamageHpBar();

            /*for (int i = 0; i < trainhit.Length; i++)
            {
                trainhit[i].Hit();
            }*/
            SmokeTrain();
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RoketBullet"))
        {
            Damage(dicEnemydata[EnemyType.Roket].GetDamage());
        }

        else if (other.CompareTag("HumanoidRigBullet"))
        {
            Damage(dicEnemydata[EnemyType.HumanoidRig].GetDamage() * Time.deltaTime);
        }

        else
        {
            return;
        }

        trainhit.Hit();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("FireBullet"))
        {
            Damage(dicEnemydata[EnemyType.Fire].GetDamage() * Time.deltaTime);
        }

        else if (other.CompareTag("GuardianBullet"))
        {
            Damage(dicEnemydata[EnemyType.Guardian].GetDamage() * Time.deltaTime);
        }

        else if (other.CompareTag("DrillBullet"))
        {
            Damage(dicEnemydata[EnemyType.Drill].GetDamage() * Time.deltaTime);
        }

        else
        {
            return;
        }

        trainhit.Hit();
    }

    IEnumerator Destroy()
    {
        roomHp += initRoomHp;
        if (trainManager.curTrainCount > 0)
        {
            trainManager.curTrainCount--;
            destroy = true;
            yield return new WaitForSeconds(0.5f);
            trainManager.Explotion();
            yield return new WaitForSeconds(4);
            GameManager.Instance.state = GameManager.State.End;
            destroy = false;
        }
    }
}