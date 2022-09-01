using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainScript : MonoBehaviour
{
    public static TrainScript instance { get; private set; }

    public TrainInfo traininfo;

    public int curTrainHpMax;


    public float curTrainHp; //0���Ϸ� ����߸��� ����!

    [HideInInspector]
    public float CurTrainHp
    {
        get { return curTrainHp; }
        set
        {
            curTrainHp = Mathf.Clamp(value, 0, curTrainHpMax);

            SmokeTrain();
        }
    }
    private float curTrainHpTime = 0;
    private float maxHpTime = 5;
    public float recoverAmount = 0.5f;

    public int trainDef;

    public float curTrainShield = 10000;
    public int dieEnemy = 0;

    private float hpCheck = 70;

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

    private float recoveryAmount = 1;

    private float lastHitTime;

    private bool explosiveShield = false;
    private float explosiveShieldDamage =2;

    private float lastTotalDamage;

    private bool maskOfCriminal = false;

    public LayerMask layerMask;

    private bool onCoolDown = false;
    private int coolDownHealHp = 20;

    private float curTime = 0;
    private float onCoolDownMaxTime = 3;

    private GameObject turrets;

    private float wireEntanglementRange = 20;
    private float wireEntanglementDamage =0.5f;
    private bool onWireEntanglement = false;
    private float curWireEntanglementTime;
    private float wireEntanglementTimeMax=1;

    private float additionalRecoveryAmount;

    private InGameUII inGameUII;

    private void Awake()
    {
        instance = this;

        EnemyDataInit();
    }

    private void OnEnable()
    {
        trainManager = GetComponent<TrainManager>();

        trainManager.CreateTrainPrefab(traininfo.trainCount);

        CurTrainHp = curTrainHpMax = traininfo.trainMaxHp;
        curTrainShield = traininfo.trainMaxShield;
        roomHp = traininfo.trainMaxHp / trainManager.curTrainCount;
        smokeHp = roomHp / 10;
        initRoomHp = roomHp;     
    }

    private void Start()
    {
        trainhit = GetComponentInChildren<TrainHit>();

        turrets = TurretManager.Instance.turrets;

        StartCoroutine(FixTimeHp());

        inGameUII = InGameUII._instance;
    }

    private void Update()
    {
        //if (destroy)
        //{
        //    trainManager.KeepOffTrain();
        //}
        FixShield();

        if (explosiveShield)
        {
            lastHitTime += Time.deltaTime;
        }

        FinxTimeHp();
        CoolDown();


        curWireEntanglementTime += Time.deltaTime;

        if (onWireEntanglement && curWireEntanglementTime > wireEntanglementTimeMax)
        {
            curWireEntanglementTime = 0;

            WireEntanglement();

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
        if ((CurTrainHp / curTrainHpMax) *100 <= 70)
        {
            if ((CurTrainHp / curTrainHpMax) * 100 >= 50)
            {
                trainManager.AllOffSmoke();

                trainManager.OnSmoke();
            }

            else if ((CurTrainHp / curTrainHpMax) * 100 >= 35)
            {
                trainManager.AllOffSmoke();

                trainManager.OnSmoke();
                trainManager.OnSmoke();
            }

            else if ((CurTrainHp / curTrainHpMax) * 100 >= 20)
            {
                trainManager.AllOffSmoke();
                trainManager.OffBlackSmoke();

                trainManager.OnSmoke();
                trainManager.OnSmoke();
                trainManager.OnSmoke();
            }

            else if ((CurTrainHp / curTrainHpMax) * 100 >= 15)
            {
                trainManager.OnBlackSmoke();
            }

            else if ((CurTrainHp / curTrainHpMax) * 100 >= 5)
            {
                trainManager.AllOffFire();

                trainManager.OnFire();
            }

            else if ((CurTrainHp / curTrainHpMax) * 100 > 0)
            {
                trainManager.AllOffFire();

                trainManager.OnFire();
                trainManager.OnFire();
            }

            else if ((CurTrainHp / curTrainHpMax) * 100 <= 0)
            {
                if (CurTrainHp <= 0)
                {
                    DestroyTrain();
                }
            }
        }

        else
        {
            trainManager.AllOffSmoke();
        }
    }

    public void FixHP()
    {
        CurTrainHp = traininfo.trainMaxHp;
    }

    public void FinxTimeHp()
    {
        if(CurTrainHp  < curTrainHpMax)
        {
            curTrainHpTime += Time.deltaTime;

            if(curTrainHpTime >= maxHpTime)
            {
                CurTrainHp += recoverAmount;
                curTrainHpTime = 0;
            }
        }
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

    private IEnumerator FixTimeHp()
    {
        if (!IsFullHp())
        {
            CurTrainHp += recoveryAmount + additionalRecoveryAmount;
        }

        yield return new WaitForSeconds(1);
    }

    private bool IsFullHp()
    {
        return CurTrainHp == curTrainHpMax;
    }

    public void Damage(float damage)
    {
        if (MaskOfCriminal())
        {
            if (!SpawnMananger.Instance.stopSpawn)
            {
                if (curTrainShield <= 0)
                {
                    CurTrainHp -= damage;
                    testScripttss.Instance.TakeDamageHpBar();

                    /*for (int i = 0; i < trainhit.Length; i++)
                    {
                        trainhit[i].Hit();
                    }*/
                }

                else
                {
                    curTrainShield -= damage;
                }
            }

            if (explosiveShield && CurTrainHp >0)
            {
                if (lastHitTime <= 0.7f)
                {
                    lastTotalDamage += damage;

                    if (lastTotalDamage > curTrainHpMax * 0.15f)
                    {
                        lastTotalDamage = 0;


                        ExplosiveShield();
                    }
                }

                else
                {
                    lastHitTime = 0;

                    lastTotalDamage = damage;
                }
            }
        }
    }
    public void LevelUp()
    {
        curTrainHpMax += 10;
        recoverAmount += 0.2f;
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
        transform.Find("Turrets")?.gameObject.SetActive(false);

        roomHp += initRoomHp;
        if (trainManager.curTrainCount > 0)
        {
            trainManager.curTrainCount--;
            destroy = true;
            yield return new WaitForSeconds(0.5f);
            trainManager.OnExplotion();
            yield return new WaitForSeconds(4);
            GameManager.Instance.state = GameManager.State.End;
            destroy = false;
        }
    }

    public void OnExplosiveShield()
    {
        if (explosiveShield)
        {
            explosiveShieldDamage += 0.2f;
        }

        explosiveShield = true;
    }

    private void ExplosiveShield()
    {
        lastTotalDamage = 0;
        lastHitTime = 0;

        trainManager.OnExplotion();

        foreach (var item in Physics.OverlapBox(trainManager.center, new Vector3(50, trainManager.size.y, trainManager.size.z), Quaternion.identity, layerMask))
        {
            item.gameObject.GetComponent<HealthSystem>().Damage(inGameUII.turretDamage*explosiveShieldDamage);
        }


        trainManager.Invoke("OffExplotion", 1);
    }

    public void OnWireEntanglement()
    {
        if (onWireEntanglement)
        {
            wireEntanglementDamage += 0.1f;
            wireEntanglementRange += wireEntanglementRange * 0.2f;
        }

        onWireEntanglement = true;
    }

    private void WireEntanglement()
    {
        foreach (var item in Physics.OverlapBox(trainManager.center, new Vector3(wireEntanglementRange, trainManager.size.y, trainManager.size.z), Quaternion.identity, layerMask))
        {
            item.gameObject.GetComponent<HealthSystem>()?.Damage(inGameUII.turretDamage * wireEntanglementDamage);
        }
    }

    public void AlloySteel(float rateOfRise)
    {
        float addHp = curTrainHpMax * rateOfRise;

        curTrainHpMax += (int)addHp;
        CurTrainHp += addHp;
    }

    public void OnMaskOfCriminal()
    {
        maskOfCriminal = true;
    }

    public void OnCoolDown()
    {
        if (onCoolDown)
        {
            coolDownHealHp += 5;
        }

        onCoolDown = true;
    }

    private void CoolDown()
    {
        curTime += Time.deltaTime;

        if (onCoolDownMaxTime >= curTime)
        {
            curTime = 0;

            int count = 0;

            if (onCoolDown)
            {
                for (int i = 0; i < turrets.transform.childCount; i++)
                {
                    if (turrets.transform.GetChild(i).GetComponent<Turret>().IsNeedReload())
                    {
                        count++;
                    }
                }

                if (count >= 2)
                {
                    CurTrainHp += coolDownHealHp;
                }
            }
        }
    }

    private bool MaskOfCriminal()
    {
        if (!maskOfCriminal)
        {
            return true;
        }

        else
        {
            int ran = Random.Range(0, 99);
            return ran <= 9;
        }
    }

    public void OnSpanner()
    {
        additionalRecoveryAmount += 1.2f;
    }
}