using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainScript : MonoBehaviour
{
    public static TrainScript instance { get; private set; }

    public float curTrainHp = 50000; //0���Ϸ� ����߸��� ����!
    public float maxTrainHp = 50000;
    private float hpCheck =70;

    private float initRoomHp;
    private float roomHp;

    private float smokeHp;

    private bool destroy = false;

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

    private void Awake()
    {
        instance = this;

        curTrainHp = maxTrainHp;

        TrainManager.instance.curTrainCount = TrainManager.instance.maxTrainCount;
        TrainManager.instance.CreateTrainPrefab();
        EnemyDataInit();
    }

    private void OnEnable()
    {
        roomHp = maxTrainHp / TrainManager.instance.curTrainCount;
        smokeHp = roomHp / 10;
        initRoomHp = roomHp;
    }

    private void Start()
    {
        trainhit = GetComponentInChildren<TrainHit>();
    }

    private void Update()
    {
        if (destroy)
        {
            TrainManager.instance.KeepOffTrain();
        }
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
        if (hpCheck * maxTrainHp / 100 >= curTrainHp)
        {
            switch (hpCheck)
            {
                case 70:
                    TrainManager.instance.OnSmoke();
                    hpCheck = 50;
                    break;
                case 50:
                    TrainManager.instance.OnSmoke();
                    hpCheck = 35;
                    break;
                case 35:
                    TrainManager.instance.OnSmoke();
                    hpCheck = 20;
                    break;
                case 20:
                    TrainManager.instance.OnBlackSmoke();
                    hpCheck = 15;
                    break;
                case 15:
                    TrainManager.instance.OnFire();
                    hpCheck = 5;
                    break;
                case 5:
                    TrainManager.instance.OnFire();
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
        curTrainHp = maxTrainHp;
    }

    public void Damage(float damage)
    {
        curTrainHp -= damage;
        testScriptts.Instance.TakeDamageHpBar();

        /*for (int i = 0; i < trainhit.Length; i++)
        {
            trainhit[i].Hit();
        }*/
        SmokeTrain();
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FireBullet"))
        {
            Damage(dicEnemydata[EnemyType.Fire].GetDamage() * Time.deltaTime);
        }

        else if (other.CompareTag("RoketBullet"))
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
        if (other.CompareTag("GuardianBullet"))
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
        if (TrainManager.instance.curTrainCount > 0)
        {
            TrainManager.instance.curTrainCount--;
            destroy = true;
            yield return new WaitForSeconds(0.5f);
            TrainManager.instance.Explotion();
            yield return new WaitForSeconds(4);
            GameManager.Instance.state = GameManager.State.End;
            destroy = false;
        }
    }
}