using UnityEngine;


public class TestEnemy : MonoBehaviour
{
    public LayerMask layerMask;

    public Animator anim;

    protected int enemyType = 0;
    [SerializeField]
    float enemySpeed = 10f;

    [SerializeField]
    float distanceX = 3;

    [SerializeField]
    float minDistance, maxDistance;

    private float distance;

    public GameObject waist = null;


    private IEnemyAttack enemyAttack;

    protected bool run;

    [SerializeField]
    protected float damage;

    private float randomZ;

    protected bool isDying = false;

    private float dieSpeed = 20;
    protected Vector3 dir;
    protected Transform dir2;


    protected virtual void OnEnable()
    {
        distance = Random.Range(minDistance, maxDistance);
        distanceX = Random.Range(distanceX - distanceX * 0.2f, distanceX);
        randomZ = Random.Range(-5, 5);
        isDying = false;
        EnemyTagInit();
    }

    protected virtual void Start()
    {
        enemyAttack = GetComponent<IEnemyAttack>();
    }
    protected virtual void Update()
    {
        if (!isDying)
        {

            dir2 = Physics.OverlapSphere(transform.position, 500000, layerMask)[0].transform;

            dir = dir2.position;

            Quaternion rot = Quaternion.LookRotation(dir);

            EnemyIsDistanceX();

            if (run)
            {
                EnemyTargettingMove();
                transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * 5);
            }

            else
            {
                if (dir.magnitude > 0)
                    rot = Quaternion.LookRotation(dir);

                enemyAttack.Attack(rot);
            }
        }

        else
        {
            print(1);
            Dying();
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
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    protected virtual void EnemyWaistInit()
    {
        waist.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    private void EnemyTagInit()
    {
        gameObject.tag = "Enemy";
    }

    void EnemyIsDistanceX()
    {
        if (Mathf.Abs(transform.position.x) < distanceX)
        {
            if (run)
            {
                run = !(Vector3.Distance(transform.position, dir) < distance);
                EnemyLimitMoveX();
            }
        }

        else run = true;
    }

    void EnemyLimitMoveX()
    {
        if (transform.position.x < 0)
        {
            transform.position = new Vector3(-distanceX, transform.position.y, transform.position.z);
        }

        else if (transform.position.x > 0)
        {
            transform.position = new Vector3(distanceX, transform.position.y, transform.position.z);
        }
    }

    void EnemyTargettingMove()
    {
        transform.position = Vector3.MoveTowards(transform.position, dir + new Vector3(0, 0, randomZ),
        enemySpeed * Time.deltaTime);
    }

    public void EnemyDied()
    {
        //GameObject scrap = ObjectPool.instacne.GetObject(Resources.Load<GameObject>("Scrap"));
        //scrap.transform.position = transform.position;
        gameObject.SetActive(false);
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

    private void IsDying()
    {
        isDying = true;
        gameObject.tag = "EnemyDead";
    }

    private void Dying()
    {
        transform.position += Vector3.back * dieSpeed * Time.deltaTime;
    }
    public void ChangeDamage(float damage)
    {
        this.damage = damage;
    }
    public void ChangeSpeed(float enemySpeed)
    {
        this.enemySpeed = enemySpeed;
    }
    public void ChangeDistance(float distance)
    {
        this.distance = distance;
    }
}