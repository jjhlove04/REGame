using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public EnemyData enemyStat;

    public Animator anim;

    protected int enemyType = 0;

    private float distance;

    public GameObject waist = null;

    public bool run;

    private float randomZ;

    public bool isDying = false;

    private float dieSpeed = 20;

    protected TrainManager trainManager;

    private float distanceX;

    private bool isGround = false;


    protected virtual void OnEnable()
    {
        trainManager = TrainManager.instance;

        distance = Random.Range(enemyStat.minDistance, enemyStat.maxDistance);
        distanceX = Random.Range(enemyStat.distanceX - enemyStat.distanceX * 0.2f, enemyStat.distanceX);
        randomZ = Random.Range(-5, 5);
        isDying = false;
        EnemyTagInit();
        run = true;
        AttackingTime();
    }

    protected virtual void Update()
    {
        if (!isDying)
        {
            Vector3 dir = trainManager.trainContainer[enemyType].transform.position - transform.position;

            Quaternion rot = Quaternion.LookRotation(new Vector3(dir.x, dir.y, dir.z+ randomZ + trainManager.trainContainer.Count * 25));

            if (!isGround)
            {
                Gravity();
            }

            if (run)
            {
                EnemyIsDistanceX();
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
                run = Vector3.Distance(transform.position, trainManager.trainContainer[enemyType].transform.position+ new Vector3(0,0, randomZ)) > distance;
                EnemyLimitMoveX();
            }
        }

        else run = true;
    }

    void EnemyLimitMoveX()
    {
        transform.position = new Vector3(distanceX, transform.position.y, transform.position.z);
    }

    void EnemyTargettingMove()
    {
        transform.position = Vector3.MoveTowards(transform.position, trainManager.trainContainer[enemyType].transform.position + new Vector3(0, 0, randomZ),
        enemyStat.enemySpeed * Time.deltaTime);
    }

    public void EnemyDied()
    {
        //GameObject scrap = ObjectPool.instacne.GetObject(Resources.Load<GameObject>("Scrap"));
        //scrap.transform.position = transform.position;
        GameManager.Instance.expAmount += enemyStat.dropExp;
        GameManager.Instance.goldAmount += enemyStat.dropGold;

        gameObject.SetActive(false);
    }

    private void Gravity()
    {
        if(transform.position.y < 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
        transform.position += new Vector3(0, -1.5f, 0) * Time.deltaTime;
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

    protected virtual void AttackingTime()
    {
        anim.SetFloat("AttackTime", (1/ enemyStat.sAttackTime));
    }

    protected virtual void Attack(Quaternion rot)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Ground"))
        {
            isGround = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Ground"))
        {
            isGround = false;
        }
    }
}