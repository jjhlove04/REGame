using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
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

    protected bool isDying =false;

    private float dieSpeed=20;


    protected virtual void OnEnable()
    {
        distance = Random.Range(minDistance, maxDistance);
        distanceX = Random.Range(distanceX - distanceX * 0.2f, distanceX);
        randomZ = Random.Range(-5, 5);
        isDying = false;
    }

    protected virtual void Start()
    {
        enemyAttack = GetComponent<IEnemyAttack>();
    }
    protected virtual void Update()
    {
        if (!isDying)
        {
            NewTarget();

            Vector3 dir = TrainManager.instance.trainContainer[enemyType].transform.position - transform.position;

            Quaternion rot = Quaternion.LookRotation(new Vector3(dir.x, dir.y, dir.z + TrainManager.instance.trainContainer.Count * 25));

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

    void NewTarget()
    {
        if (enemyType >= TrainManager.instance.trainContainer.Count)
        {
            enemyType--;
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
        enemyType = Random.Range(0, TrainManager.instance.trainContainer.Count);
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    protected virtual void EnemyWaistInit()
    {
        waist.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    void EnemyIsDistanceX()
    {
        if (Mathf.Abs(transform.position.x) < distanceX)
        {
            if (run)
            {
                run = !(Vector3.Distance(transform.position, TrainManager.instance.trainContainer[enemyType].transform.position) < distance);
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
        if (TrainManager.instance.trainContainer[enemyType].transform.position != null)
        {

            transform.position = Vector3.MoveTowards(transform.position, TrainManager.instance.trainContainer[enemyType].transform.position + new Vector3(0,0, randomZ),
            enemySpeed * Time.deltaTime);
        }

        else
        {
            transform.position = Vector3.MoveTowards(transform.position, TrainManager.instance.trainContainer[enemyType-1].transform.position,
            enemySpeed * Time.deltaTime);
        }
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
    }

    private void Dying()
    {
        transform.position += Vector3.back * dieSpeed * Time.deltaTime;
    }
}