using System.Collections;
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
    private int damage;
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

    private int criticalHitProbability = 10;

    private bool onTaillessPlanaria = false;
    private float heal = 0.5f;

    private bool onTheSoleCandy;
    private float theSoleCandyDamage;

    private bool onFurryBracelet = false;
    private float onFurryBraceletTime = 1.5f;

    private bool onFMJ = false;
    private float additionalFMJDamage=0;

    private bool onPunchGun = false;
    private float punchGunPercentage;

    private GameManager gameManager;

    private void OnEnable()
    {
        OffDetection();

        if(Physics.OverlapSphere(transform.position, 4, rader).Length > 0)
        {
            OnDetection();
        }

        targetPos = transform.position;

        if (inGameUII != null)
        {
            damage = inGameUII.turretDamage;
        }
    }

    private void Start()
    {
        shootTimerMax = curShootTimeMax;
        shootTimeMax = curShootTimeMax;
        gameManager = GameManager.Instance;
        trainScript = TrainScript.instance;
        inGameUII = InGameUII._instance;
        bulAmount = maxBulletAmount;

        bulletBar.UpdateBar(bulAmount, maxBulletAmount);

        damage = inGameUII.turretDamage;

        TurretManager.Instance.SpawnTurret(this);
    }

    private void Update()
    {
        HandleTargeting();
        HandleShooting();
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
                if (shootTimer >= ((shootTimerMax / weapons.Length) * shootCount)*Random.Range(0.9f,1.1f))
                {
                    LookForTargets();

                    ShootSound();

                    GameObject gameObject = ObjectPool.instacne.GetObject(bullet);

                    gameObject.transform.position = weapons[shootCount - 1].transform.Find("BulletPoint").position;

                    SelectBullet(gameObject);

                    TestTurretDataBase.Instance.resultDamage += (damage+ additionalDamage);
                    bulAmount--;

                    bulletBar.UpdateBar(bulAmount, maxBulletAmount);

                    shootCount++;

                    if (shootCount == weapons.Length+1)
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

                    else if(detection)
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
            mos.z = Camera.main.farClipPlane;
            Vector3 ray = Camera.main.ScreenToWorldPoint(mos);
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.transform.position, ray, out hit, mos.z))
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
        ProjectileMover projectileMover = gameObject.GetComponent<ProjectileMover>();

        projectileMover.Create(targetEnemy, (damage + additionalDamage * WeakLens()))
            .ThisTurret(this)
            .SetRedNut(IsRedNut())
            .SetTaillessPlanaria(onTaillessPlanaria)
            .SetWeakLens(IsWeakLens())
            .SetFurryBracelet(FurryBaracelet(), onFurryBraceletTime)
            .SetFMJAdditionalDamage(onFMJ, additionalFMJDamage)
            .SetOnPunchGun(IsPuchGun())
            .SetOnTheSoleCandy(IsTheSoleCandy(), theSoleCandyDamage);
            

        TaillessPlanaria();
    }

    public void EnemyMissing()
    {
        shootTimer = 2;

        bulAmount++;

        bulletBar.UpdateBar(bulAmount, maxBulletAmount);
    }

    public void LevelUpDamage(int curDamage, float distance, float shootTime, int bullet, int rPrice)
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
        if(bulAmount < 1)
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
        }
        else if(bulAmount == maxBulletAmount)
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
        if(shootAudioSource.clip != null)
        {
            shootAudioSource.Play();

            shootAudioSource.pitch = (shootAudioSource.clip.length * (1 / shootAudioSource.clip.length)) * Time.timeScale;

            waiting = true;
        }
    }

    private void WaitingTimeSound()
    {
        if(waitingAudioSource.clip != null)
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
        if(reloadAudioSource.clip != null)
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
        shootTimerMax = (shootTimerMax / 100)*70;

        additionalDamage += (int)(damage * 0.3f);
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
        if (count > 1)
        {
            criticalHitProbability += 7 * (count-1);
        }

        onWeakLens = on;

        return this;
    }

    private bool IsWeakLens()
    {
        return onWeakLens && Random.Range(0, 100) <= gameManager.ActivationCoefficient(criticalHitProbability);
    }

    private int WeakLens()
    {
        if(IsWeakLens())
        {
            return 2;
        }

        else
        {
            return 1;
        }
    }

    public Turret OnTaillessPlanaria(bool on, int count)
    {
        if (count > 1)
        {
            heal += 0.1f * (count - 1);
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
            onFurryBraceletTime += 0.5f * (count-1);
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

    public int ReturnDamage()
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
        TurretManager.Instance.SelectTurret(this);
    }
}