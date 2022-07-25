using System.Collections;
using UnityEngine;

public enum OnlyDamage
{
    singular,
    wideArea,
    Null
}
public class Enemy : MonoBehaviour
{
    public EnemyData enemyStat;

    protected int enemyType = 0;

    public Animator anim;
    public GameObject waist = null;

    private float distance;
    private float randomZ;
    private float distanceX;

    protected float speed;

    public bool run;

    public bool isDying = false;
    private float dieSpeed = 20;
    private float animSpeed;

    protected TrainManager trainManager;
    protected TrainScript trainScript;
    protected GameManager gameManager;

    [SerializeField]
    private bool stealth = false;

    public OnlyDamage onlyDamage = OnlyDamage.Null;

    private bool emp = false;

    protected Transform target;
    private Transform saveTarget;

    private bool blackhole;
    private float balckholeDamage;
    private float balckgoleDuration;

    public bool onFurryBracelet= false;


    protected virtual void OnEnable()
    {
        blackhole = false;

        SpawnMananger.Instance.enemyList.Add(this.gameObject);
        trainManager = TrainManager.instance;
        trainScript = TrainScript.instance;
        gameManager = GameManager.Instance;

        distance = Random.Range(enemyStat.minDistance, enemyStat.maxDistance);
        distanceX = Random.Range(enemyStat.distanceX - enemyStat.distanceX * 0.2f, enemyStat.distanceX);
        randomZ = Random.Range(-5, 5);
        isDying = false;
        EnemyTagInit();
        run = true;
        AttackingTime();
        RandomAnimSpeed();
    }
    
    private void Start()
    {
        SpawnMananger.Instance.enemyList.Clear();
    }

    protected virtual void Update()
    {

        if (!isDying)
        {
            if (blackhole)
            {
                GetComponent<HealthSystem>().Damage(balckholeDamage/ balckgoleDuration*Time.deltaTime);
            }

            if (!emp&&!onFurryBracelet)
            {
                EnemyIsDistanceX();
                anim.speed = animSpeed;

                Vector3 dir = target.position - transform.position;

                Quaternion rot = LookTarget(dir);

                if (run)
                {
                    EnemyTargettingMove();


                    transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * 5);
                }

                else
                {
                    if (dir != Vector3.zero)
                    {
                        if (dir.magnitude > 0)
                        {
                            rot = Quaternion.LookRotation(dir);

                            Attack(rot);
                        }
                    }
                }
            }

            else
            {
                Dying();
                anim.speed = 0;
            }
        }

        else
        {
            Dying();
        }

        if (transform.position.y < 0)
        {
            transform.position += new Vector3(0, 1, 0);
        }
    }

    protected virtual void EnemyWaistLookForward()
    {
        Quaternion quaternion = Quaternion.identity;
        quaternion.eulerAngles = new Vector3(0, 0, 0);
        waist.transform.rotation = quaternion;
    }

    protected virtual void EnemyGetRandom()
    {
        enemyType = Random.Range(0, trainManager.trainContainer.Count);
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    protected virtual void EnemyWaistInit()
    {
        waist.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    private Quaternion LookTarget(Vector3 dir)
    {
        Quaternion rot = Quaternion.LookRotation(new Vector3(dir.x, dir.y, dir.z + randomZ + trainManager.trainContainer.Count * 25));

        if (target != trainManager.trainContainer[enemyType].transform)
        {
            rot = Quaternion.LookRotation(dir);
        }

        return rot;
    }

    private void EnemyTagInit()
    {
        gameObject.tag = "Enemy";
    }

    void EnemyIsDistanceX()
    {
        if (target == null)
        {
            target = trainManager.trainContainer[enemyType].transform;
            saveTarget = null;
        }

        else if(target != trainManager.trainContainer[enemyType].transform)
        {
            if(Vector3.Distance(transform.position, target.position) > 30 || target.GetComponent<Bait>().isDie)
            {
                target = trainManager.trainContainer[enemyType].transform;
            }
        }

        if (Mathf.Abs(transform.position.x) <= target.position.x+distanceX)
        {
            run = Vector3.Distance(transform.position, target.position + new Vector3(0, 0, randomZ)) > distance;
            //EnemyLimitMoveX();
        }

        else run = true;
    }

    void EnemyLimitMoveX()
    {
        transform.position = new Vector3(target.position.x+distanceX, transform.position.y, transform.position.z);
    }

    void EnemyTargettingMove()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position + new Vector3(distanceX, 0, randomZ),
        speed * Time.deltaTime);
    }

    public void EnemyDied()
    {
        //GameObject scrap = ObjectPool.instacne.GetObject(Resources.Load<GameObject>("Scrap"));
        //scrap.transform.position = transform.position;
        gameManager.ExpAmount += enemyStat.dropExp;
        gameManager.GoldAmount += enemyStat.dropGold;
        InGameUII._instance.CreateMonjeyTxt(enemyStat.dropGold);

        trainScript.dieEnemy++;
        TestTurretDataBase.Instance.killEnemy++;

        gameObject.SetActive(false);
    }

    private void Gravity()
    {
        if(transform.position.y < 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }

        transform.position += new Vector3(0, -9.8f, 0) * Time.deltaTime;
    }

    public virtual void PlayDieAnimationTrue()
    {
        anim.SetBool("IsDie", true);
        IsDying();
    }

    protected virtual void PlayDieAnimationFalse()
    {
        anim.SetBool("IsDie", false);
    }

    public void OnEmp(float duration)
    {
        if (emp)
        {
            CancelInvoke("OffEmp");

            Invoke("OffEmp", duration);
        }

        else
        {
            emp = true;

            Invoke("OffEmp", duration);
        }
    }

    public void OffEmp()
    {
        emp = false;
    }

    public void Aggro(Transform bait)
    {
        saveTarget = target;

        if(bait.gameObject.activeSelf && bait != null && Vector3.Distance(transform.position, target.position)> Vector3.Distance(transform.position, bait.position))
        {
            target = bait;
        }
    }

    private void IsDying()
    {
        isDying = true;
        gameObject.tag = "EnemyDead";
    }

    private void Dying()
    {
        transform.position += Vector3.back * dieSpeed * Time.deltaTime;
    }

    protected virtual void AttackingTime()
    {
        anim.SetFloat("AttackTime", (1/ enemyStat.sAttackTime));
    }

    protected virtual void Attack(Quaternion rot)
    {

    }

    public bool IsStealth()
    {
        return stealth;
    }

    public void RandomAnimSpeed()
    {
        animSpeed = Random.Range(0.9f, 1.1f);
        anim.speed = animSpeed;

        RandomMoveSpeed();
    }

    public void RandomMoveSpeed()
    {
        speed = enemyStat.enemySpeed * anim.speed;
    }

    public void BlackHole(float damage, float duration ,bool isBlackhole)
    {
        blackhole = isBlackhole;
        balckholeDamage = damage;
        balckgoleDuration = duration;
    }

    public void OnFurryBracelet(float time)
    {
        StartCoroutine(FurryBracelet(time));
    }

    private IEnumerator FurryBracelet(float time)
    {
        onFurryBracelet = true;

        yield return new WaitForSeconds(time);

        onFurryBracelet = false;
    }
}