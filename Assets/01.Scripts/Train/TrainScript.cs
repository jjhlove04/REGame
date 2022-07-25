using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainScript : MonoBehaviour
{
    public static TrainScript instance { get; private set; }

    public TrainInfo traininfo;

    private float curTrainHpMax;


    public float curTrainHp; //0���Ϸ� ����߸��� ����!

    [HideInInspector]
    public float CurTrainHp
    {
        get { return curTrainHp; }
        set
        {
            curTrainHp = value;
            if (curTrainHp > curTrainHpMax)
            {
                curTrainHp = curTrainHpMax;
            }
        }
    }

    private float curTrainHpTime = 0;
    private float maxHpTime = 5;
    public float recoverAmount = 0.5f;

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

    private float lastHitTime;

    private bool explosiveShield = false;
    private int explosiveShieldDamage;

    private float lastTotalDamage;

    private bool maskOfCriminal = false;

    public LayerMask layerMask;

    private bool onCoolDown = false;
    private int coolDownHealHp = 20;

    private float curTime = 0;
    private float onCoolDownMaxTime = 3;

    private GameObject turrets;


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

        explosiveShieldDamage = 50;
    }

    private void Start()
    {
        trainhit = GetComponentInChildren<TrainHit>();

        turrets = TurretManager.Instance.turrets;
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

        CoolDown();
        FixTimeHP();
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
        if (hpCheck * curTrainHpMax / 100 >= CurTrainHp)
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

                    if (CurTrainHp <= 0)
                    {
                        DestroyTrain();
                    }
                    break;
            }
        }
    }

    public void FixHP()
    {
        CurTrainHp = traininfo.trainMaxHp;
    }

    public void FixTimeHP()
    {
        if (CurTrainHp < curTrainHpMax)
        {
            curTrainHpTime += Time.deltaTime;

            if (curTrainHpTime >= maxHpTime)
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

    public void Damage(float damage)
    {
        if (MaskOfCriminal())
        {
            if (curTrainShield <= 0)
            {
                CurTrainHp -= damage;
                testScripttss.Instance.TakeDamageHpBar();

                /*for (int i = 0; i < trainhit.Length; i++)
                {
                    trainhit[i].Hit();
                }*/
                SmokeTrain();
            }

            else
            {
                curTrainShield -= damage;
            }

            if (explosiveShield)
            {
                if (lastHitTime <= 0.7f)
                {
                    lastTotalDamage += damage;

                    if (lastTotalDamage > curTrainHpMax * 0.15f)
                    {
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
            explosiveShieldDamage += (int)(explosiveShieldDamage * 0.2f);
        }

        explosiveShield = true;
    }

    private void ExplosiveShield()
    {
        lastTotalDamage = 0;
        lastHitTime = 0;

        trainManager.OnExplotion();

        foreach (var item in Physics.OverlapBox(trainManager.center, new Vector3(25, trainManager.size.y, trainManager.size.z), Quaternion.identity, layerMask))
        {
            item.gameObject.GetComponent<HealthSystem>().Damage(explosiveShieldDamage);
        }

        

        trainManager.Invoke("OffExplotion",1);
    }

    public void AlloySteel(float rateOfRise)
    {
        float addHp = curTrainHpMax * rateOfRise;

        curTrainHpMax += addHp;
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
}