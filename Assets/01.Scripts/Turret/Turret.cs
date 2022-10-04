﻿using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private int shootCount = 1;

    private float shootTimer;

    private Transform targetEnemy;

    [SerializeField]
    private GameObject bullet = null;

    [SerializeField]
    private GameObject[] weapons;

    [SerializeField]
    private GameObject neck;


    [SerializeField]
    private float maxDistance;

    [SerializeField]
    private LayerMask enemy;

    public int maxBulletAmount;

    public int bulAmount = 10;

    public int turretPrice = 0;

    public int reloadPrice = 2;

    [SerializeField]
    private BulletBar bulletBar;

    [SerializeField]
    private float curShootTimeMax;

    private float shootTimeMax;

    private float shootTimerMax;

    [SerializeField]
    private float damage;
    private int additionalDamage = 0;


    [HideInInspector]
    public bool detection = false;

    public int turCount;

    public int turType;

    public int turImageCount;

    public int upgradeCost;

    [SerializeField]
    private AudioSource shootAudioSource;

    [SerializeField]
    private AudioSource waitingAudioSource;

    [SerializeField]
    private AudioSource reloadAudioSource;

    public LayerMask rader;

    private bool waiting = false;

    public GameObject warning;

    private InGameUII inGameUII;

    private Vector3 targetPos;

    private bool redNut = false;

    [SerializeField]
    private int redNutHealHp = 3;

    private TrainScript trainScript;

    private bool onNewsOfVictory = false;
    private int onNewsOfVictoryTime = 4;

    private bool onWeakLens = false;
    private int critical;

    private int criticalHitProbabilityWeakLens = 10;

    private bool onTaillessPlanaria = false;
    private float heal = 0.5f;

    private bool onTheSoleCandy;
    private float theSoleCandyDamage;

    private bool onFurryBracelet = false;
    private float onFurryBraceletTime = 1.5f;

    private bool onFMJ = false;
    private float additionalFMJDamage = 0;

    private bool onPunchGun = false;
    private float punchGunPercentage;

    private bool onMortarTube = false;
    private float mortarTubeCount;
    private float mortarTubeDamage;

    private bool onHemostatic = false;
    private float hemostaticCount;
    private float hemostaticDamage = 0.35f;

    private bool onLowaMk2 = false;
    private float LowaMk2Count;
    private int LowaMk2Percentage = 10;

    private bool onSixthGuitarString = false;
    private float sixthGuitarStringCount;
    private float sixthGuitarStringDamage = 0;

    private bool onDryOil = false;
    private float dryOilSlow = 0.1f;

    private float criticalPercentCupsAndBool;

    private GameObject speedSeriesLaunchesObj;
    private float DryOilSlow
    {
        get { return this.dryOilSlow; }
        set
        {
            if (value >= 0.6f)
            {
                dryOilSlow = 0.6f;
            }
        }
    }

    private bool onMachineHeart = false;

    private bool onSpeedSeriesLaunches = false;

    private float dryOilTime = 0.5f;

    private bool onShockwaveGenerator = false;

    [SerializeField]
    private GameObject sixthGuitarStringObj;

    private GameManager gameManager;

    private TurretManager turretManager;



    Camera cam;
    private void OnEnable()
    {
        OffDetection();

        if (Physics.OverlapSphere(transform.position, 4, rader).Length > 0)
        {
            OnDetection();
        }

        targetPos = transform.position;

        if (inGameUII != null)
        {
            damage = inGameUII.TurretDamage;
        }

        maxDistance += maxDistance * (TestTurretDataBase.Instance.plustTurretDistance / 100);
        reloadPrice -= reloadPrice * (TestTurretDataBase.Instance.plusReload / 100);
    }

    private void Start()
    {
        shootTimerMax = curShootTimeMax;
        shootTimeMax = curShootTimeMax;
        gameManager = GameManager.Instance;
        trainScript = TrainScript.instance;
        inGameUII = InGameUII._instance;
        turretManager = TurretManager.Instance;

        cam = Camera.main;

        bulAmount = maxBulletAmount;

        bulletBar.UpdateBar(bulAmount, maxBulletAmount);

        damage = inGameUII.TurretDamage;

        turretManager.SpawnTurret(this);
    }

    private void Update()
    {
        HandleTargeting();
        HandleShooting();

        //BossRoundTarget();
    }

    private void OnDisable()
    {
        OffDetection();
    }

    private void HandleTargeting()
    {
        if (targetEnemy != null && !targetEnemy.gameObject.GetComponent<Enemy>().isDying)
        {
            Vector3 target = this.targetEnemy.position - transform.position;
            Quaternion rot = Quaternion.LookRotation(target);

            for (int i = 0; i < weapons.Length; i++)
            {
                Transform trm = weapons[i].transform;

                trm.rotation = Quaternion.Lerp(trm.rotation, rot, Time.deltaTime * 5);
                trm.localRotation = Quaternion.Euler(new Vector3(trm.rotation.eulerAngles.x, 0, 0));
            }

            Transform neckTrm = neck.transform;

            neckTrm.rotation = Quaternion.Lerp(neckTrm.rotation, rot, Time.deltaTime * 5);
            neckTrm.rotation = Quaternion.Euler(new Vector3(0, neckTrm.rotation.eulerAngles.y, 0));

            //targetEnemy = this.targetEnemy;
        }
    }


    private void HandleShooting()
    {
        if (targetEnemy != null && !targetEnemy.GetComponent<Enemy>().isDying && Vector3.Distance(transform.position, targetEnemy.position) < 80)
        {
            if (!SpawnMananger.Instance.stopSpawn)
            {
                shootTimer += Time.deltaTime;
            }
            if (bulAmount > 0)
            {
                if (shootTimer >= ((shootTimerMax / weapons.Length) * shootCount) * Random.Range(0.9f, 1.1f))
                {
                    LookForTargets();

                    ShootSound();

                    GameObject gameObject = ObjectPool.instacne.GetObject(bullet);

                    gameObject.transform.position = weapons[shootCount - 1].transform.Find("BulletPoint").position;

                    SelectBullet(gameObject);

                    TestTurretDataBase.Instance.resultDamage += (damage + additionalDamage);
                    bulAmount--;

                    bulletBar.UpdateBar(bulAmount, maxBulletAmount);

                    shootCount++;

                    if (shootCount == weapons.Length + 1)
                    {
                        shootTimer = 0;
                        shootCount = 1;
                    }

                    if (IsRedNut())
                    {
                        RedNut();
                    }
                }

                else
                {
                    WaitingTimeSound();
                }
            }
        }

        else
        {
            targetEnemy = null;
            LookForTargets();
        }
    }

    void LookForTargets()
    {
        Collider[] hit = Physics.OverlapSphere(transform.position, maxDistance, enemy);

        foreach (Collider hitEnemy in hit)
        {
            Enemy enemy = hitEnemy.GetComponent<Enemy>();
            if (hitEnemy.CompareTag("Enemy"))
            {
                if (!enemy.isDying)
                {
                    if (!enemy.IsStealth())
                    {
                        if (targetEnemy != null)
                        {
                            if (Vector3.Distance(targetPos, hitEnemy.transform.position) < Vector3.Distance(transform.position, targetEnemy.transform.position))
                            {
                                targetEnemy = hitEnemy.transform;
                            }
                        }

                        else
                        {
                            targetEnemy = hitEnemy.transform;
                        }
                    }

                    else if (detection)
                    {
                        if (targetEnemy != null)
                        {
                            if (Vector3.Distance(targetPos, hitEnemy.transform.position) < Vector3.Distance(transform.position, targetEnemy.transform.position))
                            {
                                targetEnemy = hitEnemy.transform;
                            }
                        }

                        else
                        {
                            targetEnemy = hitEnemy.transform;
                        }
                    }
                }
            }
        }
    }

    private void BossRoundTarget()
    {
        if (targetEnemy == null)
        {
            Vector3 mos = Input.mousePosition;
            mos.z = cam.farClipPlane;
            Vector3 ray = cam.ScreenToWorldPoint(mos);
            RaycastHit hit;

            if (Physics.Raycast(cam.transform.position, ray, out hit, mos.z))
            {
                Vector3 target = hit.point - transform.position;
                Quaternion rot = Quaternion.LookRotation(target);

                for (int i = 0; i < weapons.Length; i++)
                {
                    Transform trm = weapons[i].transform;

                    trm.rotation = Quaternion.Lerp(trm.rotation, rot, Time.deltaTime * 5);
                    trm.localRotation = Quaternion.Euler(new Vector3(trm.rotation.eulerAngles.x, 0, 0));
                }

                Transform neckTrm = neck.transform;

                neckTrm.rotation = Quaternion.Lerp(neckTrm.rotation, rot, Time.deltaTime * 5);
                neckTrm.rotation = Quaternion.Euler(new Vector3(0, neckTrm.rotation.eulerAngles.y, 0));
            }
        }

        if (targetEnemy == null)
        {
            shootTimer += Time.deltaTime;

            if (bulAmount > 0)
            {
                if (shootTimer >= ((shootTimerMax / weapons.Length) * shootCount) * Random.Range(0.9f, 1.1f))
                {
                    LookForTargets();

                    ShootSound();

                    GameObject gameObject = ObjectPool.instacne.GetObject(bullet);

                    gameObject.transform.position = weapons[shootCount - 1].transform.Find("BulletPoint").position;

                    SelectBullet(gameObject);

                    TestTurretDataBase.Instance.resultDamage += (damage + additionalDamage);
                    bulAmount--;

                    bulletBar.UpdateBar(bulAmount, maxBulletAmount);

                    shootCount++;

                    if (shootCount == weapons.Length + 1)
                    {
                        shootTimer = 0;
                        shootCount = 1;
                    }

                    if (IsRedNut())
                    {
                        RedNut();
                    }
                }

                else
                {
                    WaitingTimeSound();
                }
            }
        }
    }

    private void SelectBullet(GameObject gameObject)
    {
        IsCritical();

        ProjectileMover projectileMover = gameObject.GetComponent<ProjectileMover>();

        projectileMover.Create(targetEnemy, (damage * critical) + additionalDamage)
            .ThisTurret(this)
            .SetRedNut(IsRedNut())
            .SetTaillessPlanaria(onTaillessPlanaria)
            .SetWeakLens(IsCritical())
            .SetFurryBracelet(FurryBaracelet(), onFurryBraceletTime)
            .SetFMJAdditionalDamage(onFMJ, additionalFMJDamage)
            .SetOnPunchGun(IsPuchGun())
            .SetOnTheSoleCandy(IsTheSoleCandy(), theSoleCandyDamage)
            .SetOnHemostatic(IsHemostatic(), hemostaticDamage * damage)
            .SetOnSixthGuitarString(IsSixthGuitarString(), damage * sixthGuitarStringDamage)
            .SetOnDryOil(IsDryOil(), DryOilSlow, dryOilTime)
            .SetOnShockwaveGenerator(IsShockwaveGenerator())
            .ThisTurret(this);


        TaillessPlanaria();

        if (IsMortarTube())
        {
            mortarTubeDamage = damage * mortarTubeCount;

            TrainManager.instance.MortarTube(mortarTubeDamage);
        }

        if (IsLowaMk23())
        {
            LowaMk23();
        }
    }

    public void EnemyMissing()
    {
        shootTimer = 2;

        bulAmount++;

        bulletBar.UpdateBar(bulAmount, maxBulletAmount);
    }

    public void LevelUpDamage(float curDamage, float distance, float shootTime, int bullet, int rPrice)
    {
        damage = curDamage;
        maxDistance = distance;
        curShootTimeMax = shootTime;
        maxBulletAmount = bullet;
        reloadPrice = rPrice;

    }

    public void OnDetection()
    {
        detection = true;
    }

    public void OffDetection()
    {
        detection = false;
    }

    public bool IsNeedReload()
    {
        if (bulAmount < 1)
        {
            return true;
        }

        return false;
    }

    public void Reload()
    {
        if (gameManager.GoldAmount >= reloadPrice && bulAmount != maxBulletAmount)
        {
            ReloadSound();

            gameManager.GoldAmount -= reloadPrice;
            InGameUII._instance.CreateOutMoney(reloadPrice);
            bulAmount = maxBulletAmount;

            if (IsMachineHeart())
            {
                trainScript.ReloadMachineHeart();
            }

            if (IsSpeedSeriesLaunches())
            {
                inGameUII.ReloadSpeedSeriesLaunches();
            }
        }
        else if (bulAmount == maxBulletAmount)
        {
            inGameUII.GoldWarning.GetComponent<CanvasGroup>().alpha = 1;
            inGameUII.warningIcon.color = new Color(1, 0.8f, 0, 1);
            inGameUII.warningtxt.text = "It's Already Loaded";
        }
        else
        {
            inGameUII.GoldWarning.GetComponent<CanvasGroup>().alpha = 1;
            inGameUII.warningIcon.color = new Color(1, 0.8f, 0, 1);
            inGameUII.warningtxt.text = "Not Enough Gold";

        }
        bulletBar.UpdateBar(bulAmount, maxBulletAmount);
    }

    private void ShootSound()
    {
        if (shootAudioSource.clip != null)
        {
            shootAudioSource.Play();

            shootAudioSource.pitch = (shootAudioSource.clip.length * (1 / shootAudioSource.clip.length)) * Time.timeScale;

            waiting = true;
        }
    }

    private void WaitingTimeSound()
    {
        if (waitingAudioSource.clip != null)
        {
            waitingAudioSource.pitch = waitingAudioSource.clip.length / shootTimerMax * Time.timeScale;

            if (waiting)
            {
                waitingAudioSource.Play();

                waiting = false;
            }
        }
    }

    private void ReloadSound()
    {
        if (reloadAudioSource.clip != null)
        {
            reloadAudioSource.pitch = reloadAudioSource.clip.length / shootTimerMax;

            reloadAudioSource.Play();
        }
    }

    public void OnWarning()
    {
        if (bulAmount / maxBulletAmount <= 0.3)
        {
            warning.SetActive(true);
        }
        else
        {
            warning.SetActive(false);
        }
    }

    public void DesignateTarget(Vector3 target)
    {
        targetPos = target;

        LookForTargets();
    }

    public Turret OnRedNut(bool on, int count)
    {
        redNut = on;

        redNutHealHp = 5 * count;

        return this;
    }

    private bool IsRedNut()
    {
        return Random.Range(0, 100) < gameManager.ActivationCoefficient(8) && redNut;
    }

    private void RedNut()
    {
        trainScript.CurTrainHp += redNutHealHp;
        trainScript.CurTrainHp += redNutHealHp;
    }

    public void OnNewsOfVictory(int time)
    {
        onNewsOfVictory = true;

        onNewsOfVictoryTime = time;

        NewsOfVictory();

        StartCoroutine(OffNewsOfVictory());
    }

    IEnumerator OffNewsOfVictory()
    {
        yield return new WaitForSeconds(onNewsOfVictoryTime);

        shootTimerMax = curShootTimeMax;

        additionalDamage = 0;

        onNewsOfVictory = false;
    }

    private void NewsOfVictory()
    {
        shootTimerMax = (shootTimerMax / 100) * 70;

        additionalDamage = (int)(damage * 0.3f);
    }

    public Turret Overclokcing(bool on, float increase, int count)
    {
        if (on)
        {
            curShootTimeMax = shootTimeMax - curShootTimeMax * increase * count;
        }

        return this;
    }

    public Turret OnWeakLens(bool on, int count)
    {
        criticalHitProbabilityWeakLens = 3 + 7 * count;

        onWeakLens = on;

        return this;
    }

    private bool IsCritical()
    {
        if (Random.Range(0, 100) <= gameManager.ActivationCoefficient(criticalHitProbabilityWeakLens + criticalPercentCupsAndBool))
        {
            Critical(true);
            return true;
        }

        else
        {
            Critical(false);
            return false;
        }
    }

    private void Critical(bool onWeakLens)
    {
        if (onWeakLens)
        {
            critical = 2;
        }

        else
        {
            critical = 1;
        }
    }

    public Turret OnTaillessPlanaria(bool on, int count)
    {
        if (count > 1)
        {
            heal = 0.1f * (count - 1);
        }

        onTaillessPlanaria = on;



        return this;
    }

    private void TaillessPlanaria()
    {
        if (onTaillessPlanaria)
        {
            trainScript.CurTrainHp += heal;
        }
    }

    public Turret OnTheSoleCandy(bool on, int count)
    {
        onTheSoleCandy = on;

        if (onTheSoleCandy)
        {
            theSoleCandyDamage = 0.4f * count;
        }

        return this;
    }

    private bool IsTheSoleCandy()
    {
        return Random.Range(0, 100) < gameManager.ActivationCoefficient(8) && onTheSoleCandy;
    }

    public Turret OnFurryBracelet(bool on, int count)
    {
        if (count > 1)
        {
            onFurryBraceletTime = 0.5f * (count - 1);
        }

        onFurryBracelet = on;

        return this;
    }

    private bool FurryBaracelet()
    {
        return Random.Range(0, 100) <= gameManager.ActivationCoefficient(7) && onFurryBracelet;
    }

    public Turret OnFMJ(bool on, int count)
    {
        onFMJ = on;

        additionalFMJDamage = 0.25f * count;

        return this;
    }

    public Turret OnPunchGun(bool on, int count)
    {
        onPunchGun = on;

        punchGunPercentage = 6 * count;

        return this;
    }

    private bool IsPuchGun()
    {
        return Random.Range(0, 100) <= gameManager.ActivationCoefficient(punchGunPercentage) && onPunchGun;
    }

    public float ReturnDamage()
    {
        return damage;
    }
    public float ReturnDistance()
    {
        return maxDistance;
    }

    public float ReturnShootTimer()
    {
        return curShootTimeMax;
    }

    public Turret OnMortarTube(bool on, int count)
    {
        onMortarTube = on;

        mortarTubeCount = count;

        return this;
    }

    private bool IsMortarTube()
    {
        return Random.Range(0, 100) <= gameManager.ActivationCoefficient(9) && onMortarTube;
    }

    public Turret OnHemostatic(bool on, int count)
    {
        onHemostatic = on;

        hemostaticCount = count * 15;

        return this;
    }


    private bool IsHemostatic()
    {
        return Random.Range(0, 100) <= gameManager.ActivationCoefficient(hemostaticCount) && onHemostatic;
    }

    public Turret OnLowaMk23(bool on, int count)
    {
        onLowaMk2 = on;

        LowaMk2Percentage = count * 4;

        return this;
    }
    private bool IsLowaMk23()
    {
        return Random.Range(0, 100) <= gameManager.ActivationCoefficient(LowaMk2Percentage) && onLowaMk2;
    }

    private void LowaMk23()
    {
        turretManager.LowaMk23(damage, targetEnemy, transform.position);
    }

    public Turret OnSixthGuitarString(bool on, int count)
    {
        onSixthGuitarString = on;

        sixthGuitarStringCount = count;

        sixthGuitarStringDamage = sixthGuitarStringCount * 0.33f;

        return this;
    }
    private bool IsSixthGuitarString()
    {
        return Random.Range(0, 100) <= gameManager.ActivationCoefficient(20) && onSixthGuitarString;
    }

    public Turret OnDryOil(bool on, int count)
    {
        onDryOil = on;

        DryOilSlow = 0.1f + 0.1f * count;

        dryOilTime = 0.5f + 0.5f * count;

        return this;
    }
    private bool IsDryOil()
    {
        return onDryOil;
    }

    public Turret OnMachineHeart(bool on)
    {
        onMachineHeart = on;

        return this;
    }

    private bool IsMachineHeart()
    {
        return onMachineHeart;
    }

    public Turret OnSpeedSeriesLaunches(bool on, GameObject speedSeriesLaunchesObj)
    {
        onSpeedSeriesLaunches = on;

        if (on)
        {
            this.speedSeriesLaunchesObj = ObjectPool.instacne.GetObject(speedSeriesLaunchesObj);
            this.speedSeriesLaunchesObj.transform.position = transform.position;
        }

        return this;
    }

    private bool IsSpeedSeriesLaunches()
    {
        return onSpeedSeriesLaunches;
    }

    public void SpeedSeriesLaunches(bool on)
    {
        speedSeriesLaunchesObj.SetActive(on);
    }

    public Turret OnShockwaveGenerator(bool on)
    {
        onShockwaveGenerator = on;

        return this;
    }

    private bool IsShockwaveGenerator()
    {
        return onShockwaveGenerator;
    }

    public void ShockwaveGenerator(Vector3 pos)
    {
        if (Random.Range(0, 100) <= GameManager.Instance.ActivationCoefficient(80))
        {
            turretManager.ShockwaveGenerator(pos);
        }
    }

    public Turret CupsAndBool(float criticalPercent)
    {
        this.criticalPercentCupsAndBool = criticalPercent * 100;

        return this;
    }

    private void OnMouseEnter()
    {
        //gameObject.transform.GetChild(2).gameObject.SetActive(true);

        Transform attackRange = transform.Find("AttackRange");

        attackRange.transform.localScale = new Vector3(maxDistance * 2, maxDistance * 2, 1);
        attackRange.transform.localPosition = new Vector3(0, 0, 0);
        attackRange.gameObject.SetActive(true);


        if (detection)
        {
            attackRange.GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 0, 30);
        }

        else
        {
            attackRange.GetComponentInChildren<SpriteRenderer>().color = new Color32(0, 0, 255, 30);
        }
    }

    private void OnMouseExit()
    {
        //gameObject.transform.GetChild(2).gameObject.SetActive(false);


        Transform attackRange = transform.Find("AttackRange");

        attackRange.gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        turretManager.SelectTurret(this);
    }
}