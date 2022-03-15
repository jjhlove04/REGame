using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public int enemyType = 0;
    [SerializeField]
    float enemySpeed = 10f;

    [SerializeField]
    float distanceX = 3;

    [SerializeField]
    float minDistance, maxDistance;

    private float distance;

    HealthSystem healthSystem;

    public GameObject waist;


    private IEnemyAttack enemyAttack;

    public bool run;



    private void OnEnable()
    {
        EnemyGetRandom();
        distance = Random.Range(minDistance, maxDistance);
        distanceX = Random.Range(distanceX - distanceX * 0.2f, distanceX);
    }

    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        enemyAttack = GetComponent<IEnemyAttack>();

        healthSystem.OnDied += EnemyDie;

    }
    private void Update()
    {
        NewTarget();

        Vector3 dir = TrainManager.instance.trainContainer[enemyType].transform.position - transform.position;

        Quaternion rot = Quaternion.LookRotation(new Vector3(dir.x, dir.y, dir.z + TrainManager.instance.trainContainer.Count * 25));

        run = Vector3.Distance(transform.position, TrainManager.instance.trainContainer[enemyType].transform.position) > distance;

        if (run)
        {
            EnemyTargettingMove();
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * 5);
        }

        else
        {
            if (dir.magnitude > 0)
                rot = Quaternion.LookRotation(dir);

            Quaternion quaternion = Quaternion.identity;
            quaternion.eulerAngles = new Vector3(0, 0, 0);
            waist.transform.rotation = quaternion;

            enemyAttack.Attack(rot);
        }
    }

    void NewTarget()
    {
        if (enemyType >= TrainManager.instance.trainContainer.Count)
        {
            enemyType--;
        }
    }


    void EnemyGetRandom()
    {
        enemyType = Random.Range(0, TrainManager.instance.trainContainer.Count);
        transform.rotation = new Quaternion(0, 0, 0, 0);
        waist.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

     void EnemyLimitMoveX()
    {
        if (Mathf.Abs(transform.position.x) < distanceX)
        {
            if(transform.position.x < 0)
            {
                transform.position = new Vector3(-distanceX, transform.position.y,transform.position.z);
            }

            else if(transform.position.x > 0)
            {
                transform.position = new Vector3(distanceX, transform.position.y, transform.position.z);
            }
        }
    }

    void EnemyTargettingMove()
    {
        if (TrainManager.instance.trainContainer[enemyType].transform.position != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, TrainManager.instance.trainContainer[enemyType].transform.position,
            enemySpeed * Time.deltaTime);
        }

        else
        {
            transform.position = Vector3.MoveTowards(transform.position, TrainManager.instance.trainContainer[enemyType-1].transform.position,
            enemySpeed * Time.deltaTime);
        }

        EnemyLimitMoveX();
    }

    void EnemyDie()
    {
        GameObject scrap = ObjectPool.instacne.GetObject(Resources.Load<GameObject>("Scrap"));
        scrap.transform.position = transform.position;
        gameObject.SetActive(false);
    }


    private void OnMouseEnter()
    {
        PlayerInput.Instance.isEnemy = true;
    }

    private void OnMouseExit()
    {
        PlayerInput.Instance.isEnemy = false;
    }


}
