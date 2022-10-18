using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainScript : MonoBehaviour
{
    public static TrainScript instance { get; private set; }

    public TrainInfo traininfo;

    public int shapeMemoryAlloy;

    private int curTrainMax;

    public int CurTrainHpMax
    {
        get { return curTrainMax+ shapeMemoryAlloy; }
        set
        {


            if (value > curTrainMax)
            {
                curTrainHp += value - curTrainMax;

                curTrainMax = value;

                if (onMachineHeart)
                {
                    CheckMachineHeart();
                }
            }
        }
    }

    public float curTrainHp; //0���Ϸ� ����߸��� ����!

    [HideInInspector]
    public float CurTrainHp
    {
        get { return curTrainHp; }
        set
        {
            curTrainHp = Mathf.Clamp(value, 0, CurTrainHpMax);

            if (curTrainHp > value)
            {
                GameManager.Instance.BumperGrappler();
            }
        }
    }
    private float maxTrainShield = 10000;
    public float MaxTrainShield
    {
        get { return (int)(maxTrainShield + machineHeartMaxShield); }
        set
        {
            maxTrainShield = value;
        }
    }

    private float curTrainShield = 10000;
    public float CurTrainShield
    {
        get { return curTrainShield+ MachineHeartCurShield; }
        set 
        {
            if (MachineHeartCurShield > 0)
            {
                float a = value;
                if (a< curTrainShield + MachineHeartCurShield)
                {
                    if (a > MachineHeartCurShield)
                    {
                        curTrainShield -= curTrainShield + MachineHeartCurShield + MachineHeartCurShield - a;
                        MachineHeartCurShield = 0;
                    }

                    else
                    {
                        MachineHeartCurShield -= curTrainShield + MachineHeartCurShield - a;
                    }
                }

                else
                {
                    if (a - machineHeartCurShield > maxTrainShield)
                    {
                        MachineHeartCurShield +=a - curTrainMax;
                        curTrainShield = curTrainMax;
                    }

                    else
                    {
                        curTrainShield = a;
                    }
                }

            }

            else
            {
                curTrainShield=Mathf.Clamp(value, 0, maxTrainShield);
            }
        }
    }



    private float machineHeartCurShield = 0;
    public float MachineHeartCurShield
    {
        get { return machineHeartCurShield; }

        set { machineHeartCurShield = Mathf.Clamp(value, 0, machineHeartMaxShield); }
    }
    private float machineHeartMaxShield = 0;
    private float machineHeartReloadCurTime = 0;
    private float machineHeartReloadMaxTime = 4;
    private float machineHeartCurTime = 1;
    private float machineHeartMaxTime = 1;
    private bool onMachineHeart = false;
    private int countMachineHeart = 0;

    public float shapeMemoryAlloyHp;
    private float curTrainHpTime = 0;
    private float maxHpTime = 5;
    public float recoverAmount = 0.5f;

    public int trainDef;
    public int dieEnemy = 0;

    private float decreaseHpCheck = 70;
    private float addHpCheck = 70;

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
    private float explosiveShieldDamage = 2;

    private float lastTotalDamage;

    private bool maskOfCriminal = false;

    public LayerMask layerMask;

    private bool onCoolDown = false;
    private int coolDownHealHp = 20;

    private float curTime = 0;
    private float onCoolDownMaxTime = 3;

    private GameObject turrets;

    private float wireEntanglementCurRange = 0;
    private float wireEntanglementRange=20;
    private float wireEntanglementDamage = 0.5f;
    private bool onWireEntanglement = false;
    private int wireEntanglementCount = 0;
    private float curWireEntanglementTime;
    private float wireEntanglementTimeMax = 1;
    private int wireEntanglementObjecctCount = 5;
    public List<GameObject> wireEntanglementObjecct = new List<GameObject>();

    private float additionalRecoveryAmount;

    private InGameUII inGameUII;

    [SerializeField]
    private GameObject plasmaShiled;
    private TestTurretDataBase testDatabase;

    private void Awake()
    {
        instance = this;

        EnemyDataInit();
    }

    private void OnEnable()
    {
        trainManager = GetComponent<TrainManager>();

        trainManager.CreateTrainPrefab(traininfo.trainCount);

        CurTrainHp = CurTrainHpMax = traininfo.trainMaxHp;
        CurTrainShield = traininfo.trainMaxShield;
        MaxTrainShield = traininfo.trainMaxShield;

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
        testDatabase = TestTurretDataBase.Instance;
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

        SmokeTrain();

        ReChargeMachineHeart();
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
        if (addHpCheck * CurTrainHpMax / 100 < CurTrainHp)
        {
            switch (addHpCheck)
            {
                case 70:
                    trainManager.OffSmoke();
                    decreaseHpCheck = 70;
                    break;
                case 50:
                    trainManager.OffSmoke();
                    decreaseHpCheck = 50;
                    addHpCheck = 70;
                    break;
                case 35:
                    trainManager.OffSmoke();
                    decreaseHpCheck = 35;
                    addHpCheck = 50;
                    break;
                case 20:
                    trainManager.OffBlackSmoke();
                    decreaseHpCheck = 20;
                    addHpCheck = 35;
                    break;
                case 15:
                    trainManager.OffFire();
                    decreaseHpCheck = 15;
                    addHpCheck = 20;
                    break;
                case 5:
                    trainManager.OffFire();
                    decreaseHpCheck = 5;
                    addHpCheck = 15;
                    break;
            }
        }

        else if (decreaseHpCheck * CurTrainHpMax / 100 >= CurTrainHp)
        {


            switch (decreaseHpCheck)
            {
                case 70:
                    trainManager.OnSmoke();
                    decreaseHpCheck = 50;
                    addHpCheck = 70;
                    break;
                case 50:
                    trainManager.OnSmoke();
                    decreaseHpCheck = 35;
                    addHpCheck = 50;
                    break;
                case 35:
                    trainManager.OnSmoke();
                    decreaseHpCheck = 20;
                    addHpCheck = 35;
                    break;
                case 20:
                    trainManager.OnBlackSmoke();
                    decreaseHpCheck = 15;
                    addHpCheck = 20;
                    break;
                case 15:
                    trainManager.OnFire();
                    decreaseHpCheck = 5;
                    addHpCheck = 15;
                    break;
                case 5:
                    trainManager.OnFire();
                    decreaseHpCheck = 0;
                    addHpCheck = 5;
                    break;
                case 0:
                    decreaseHpCheck = 0;

                    if (CurTrainHp <= 0)
                    {
                        if (testDatabase.plusJesus != 1)
                        {
                            CurTrainHp -= 9999;
                            DestroyTrain();
                        }
                        else
                        {
                            curTrainHp = CurTrainHpMax * 0.5f;
                        }
                    }
                    break;
            }
        }


    }

    public void FixHP()
    {
        CurTrainHp = traininfo.trainMaxHp;
    }

    public void FinxTimeHp()
    {
        if (CurTrainHp < CurTrainHpMax)
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
        if (CurTrainShield < traininfo.trainMaxShield)
        {
            if (dieEnemy >= 10)
            {
                CurTrainShield++;
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
        return CurTrainHp == CurTrainHpMax;
    }

    public void Damage(float damage)
    {
        if (MaskOfCriminal())
        {
            if (!SpawnMananger.Instance.stopSpawn)
            {
                if (CurTrainShield <= 0)
                {
                    CurTrainHp -= damage * 100 / (100 + trainDef);
                    //testScripttss.Instance.TakeDamageHpBar();

                    /*for (int i = 0; i < trainhit.Length; i++)
                    {
                        trainhit[i].Hit();
                    }*/
                }

                else
                {
                    CurTrainShield -= damage * 100 / (100 + trainDef);
                }
            }

            if (explosiveShield && CurTrainHp > 0)
            {
                if (lastHitTime <= 0.7f)
                {
                    lastTotalDamage += damage;

                    if (lastTotalDamage > CurTrainHpMax * 0.15f)
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
        CurTrainHpMax += 22;
        recoverAmount += 0.2f;
        trainDef += 2;
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
            TestTurretDataBase.Instance.sceneIndex = 1;
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

        foreach (var item in Physics.OverlapBox(trainManager.center, new Vector3(50 + (50 * (testDatabase.plusDistance / 100)), trainManager.size.y, trainManager.size.z), Quaternion.identity, layerMask))
        {
            item.gameObject.GetComponent<HealthSystem>().Damage(inGameUII.TurretDamage * explosiveShieldDamage);
        }


        trainManager.Invoke("OffExplotion", 1);
    }

    public void OnWireEntanglement()
    {
        wireEntanglementCurRange = wireEntanglementCount;

        if (onWireEntanglement)
        {
            wireEntanglementDamage += 0.1f;

            wireEntanglementCurRange += wireEntanglementCount * 0.2f;

            for (int i = 0; i < wireEntanglementObjecct.Count; i++)
            {
                wireEntanglementObjecct[i].transform.GetChild(wireEntanglementObjecctCount).gameObject.SetActive(true);
            }

            wireEntanglementObjecctCount++;
        }


        for (int i = 0; i < wireEntanglementObjecct.Count; i++)
        {
            wireEntanglementObjecct[i].gameObject.SetActive(true);
        }


        onWireEntanglement = true;


        foreach (var item in Physics.OverlapBox(trainManager.center, new Vector3(wireEntanglementCurRange + (wireEntanglementCurRange * (testDatabase.plusDistance / 100)), trainManager.size.y, trainManager.size.z), Quaternion.identity, layerMask))
        {
            item.gameObject.GetComponent<HealthSystem>()?.Damage(inGameUII.TurretDamage * wireEntanglementDamage);
        }
    }

    private void WireEntanglement()
    {
        foreach (var item in Physics.OverlapBox(trainManager.center, new Vector3(wireEntanglementCurRange, trainManager.size.y, trainManager.size.z), Quaternion.identity, layerMask))
        {
            item.gameObject.GetComponent<HealthSystem>()?.Damage(inGameUII.TurretDamage * wireEntanglementDamage);
        }
    }

    public void AlloySteel(float rateOfRise)
    {
        float addHp = CurTrainHpMax * rateOfRise;

        CurTrainHpMax += (int)addHp;
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

    public void OnMachineHeart(bool on, int count)
    {
        plasmaShiled.SetActive(true);

        for (int i = 0; i < trainManager.curTrainCount + 1; i++)
        {
            plasmaShiled.transform.GetChild(i).gameObject.SetActive(true);
        }

        countMachineHeart = count;
        machineHeartMaxShield = CurTrainHpMax * 0.08f + CurTrainHpMax * (0.04f * countMachineHeart);

        MachineHeartCurShield = machineHeartMaxShield;

        onMachineHeart = on;
    }

    public void CheckMachineHeart()
    {
        int shield = (int)(CurTrainHpMax * 0.08f + CurTrainHpMax * (0.04f * countMachineHeart))-(int)machineHeartMaxShield;
        machineHeartCurShield += shield;
        machineHeartMaxShield+= shield;
    }

    public void ReChargeMachineHeart()
    {
        if (onMachineHeart)
        {
            machineHeartReloadCurTime += Time.deltaTime;

            if (machineHeartReloadCurTime >= machineHeartReloadMaxTime)
            {
                if (machineHeartCurTime > machineHeartMaxTime)
                {
                    plasmaShiled.SetActive(true);

                    machineHeartCurTime = 0;

                    MachineHeartCurShield += machineHeartMaxShield * 0.1f;
                }
            }
        }

        if (MachineHeartCurShield <= 0)
        {
            plasmaShiled.SetActive(false);
        }
    }

    public void ReloadMachineHeart()
    {
        machineHeartReloadCurTime = 0;
        machineHeartCurTime = 1;
    }
}