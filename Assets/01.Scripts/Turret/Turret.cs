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
    private float shootTimeMax;

    private float shootTimerMax;

    [SerializeField]
    private int damage = 10;
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


    private void OnEnable()
    {
        OffDetection();

        if(Physics.OverlapSphere(transform.position, 4, rader).Length > 0)
        {
            OnDetection();
        }

        targetPos = transform.position;

    }

    private void Start()
    {
        shootTimerMax = shootTimeMax;
        trainScript = TrainScript.instance;
        inGameUII = InGameUII._instance;
        bulAmount = maxBulletAmount;

        bulletBar.UpdateBar(bulAmount, maxBulletAmount);
    }

    private void Update()
    {
        HandleTargeting();
        HandleShooting();

        if (onNewsOfVictory)
        {
            NewsOfVictory();
        }
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
        if (targetEnemy != null && !targetEnemy.gameObject.GetComponent<Enemy>().isDying && Vector3.Distance(transform.position, targetEnemy.position) < 80)
        {
            shootTimer += Time.deltaTime;
            if (bulAmount > 0)
            {
                if (shootTimer >= (shootTimerMax / weapons.Length) * shootCount)
                {
                    ShootSound();

                    GameObject gameObject = ObjectPool.instacne.GetObject(bullet);
                    gameObject.transform.position = weapons[shootCount-1].transform.Find("BulletPoint").position;
                    gameObject.GetComponent<ProjectileMover>().Create(targetEnemy, (damage+ additionalDamage));
                    TestTurretDataBase.Instance.resultDamage += (damage+ additionalDamage);
                    bulAmount--;

                    bulletBar.UpdateBar(bulAmount, maxBulletAmount);

                    shootCount++;

                    if (shootCount == weapons.Length+1)
                    {
                        shootTimer = 0;
                        shootCount = 1;
                    }

                    if (redNut)
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
        if (GameManager.Instance.goldAmount >= reloadPrice && bulAmount != maxBulletAmount)
        {
            ReloadSound();

            GameManager.Instance.goldAmount -= reloadPrice;
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

    public void OnRedNut()
    {
        redNut = true;
    }

    private void RedNut()
    {
        if(Random.Range(0,100) < 8)
        {
            trainScript.curTrainHp += redNutHealHp;
            trainScript.curTrainHp += redNutHealHp;
        }
    }

    public void OnNewsOfVictory()
    {
        onNewsOfVictory = true;

        Invoke("OffNewsOfVictory", onNewsOfVictoryTime);
    }

    private void NewsOfVictory()
    {
        shootTimerMax = (shootTimerMax / 100)*80;

        additionalDamage = (damage / 100) * 20;
    }

    private void OffNewsOfVictory()
    {
        shootTimerMax = shootTimeMax;

        additionalDamage = 0;

        onNewsOfVictory = false;
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