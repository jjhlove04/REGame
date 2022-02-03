using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [SerializeField]
    int enemyType = 0;
    [SerializeField]
    float enemySpeed = 10f;

    [SerializeField]
    float distanceX = 3;

    [SerializeField]
    bool isEnemyMove = true;
    Rigidbody rigid;

    HealthSystem healthSystem;

    public GameObject waist;

    public Animator anim;
    public float animTime;
    private float atime = 1f;

    private void OnEnable()
    {
        EnemyGetRandom();
    }

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        healthSystem = GetComponent<HealthSystem>();

        healthSystem.OnDied += EnemyDie;

    }
    private void Update()
    {
        if(Vector3.Distance(transform.position, TrainManager.instance.trainContainer[enemyType].transform.position) > 10f && isEnemyMove && Mathf.Abs(transform.position.x)-Mathf.Abs(TrainManager.instance.trainContainer[enemyType].transform.position.x) > distanceX)
        {
            EnemyTargettingMove();
        }
        else
        {
            Vector3 dir = TrainManager.instance.trainContainer[enemyType].transform.position - transform.position;

            Quaternion rot = Quaternion.LookRotation(dir);

            transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * 5);

            waist.transform.rotation = new Quaternion(0, -transform.rotation.y, 0, 0);

            anim.SetBool("IsAttack", true);

            animTime += Time.deltaTime;

            if (animTime >= atime)
            {
                anim.SetBool("IsAttack", false);
                animTime = 0f;
            }

            isEnemyMove = false;
            rigid.velocity = Vector3.zero;
        }
    }

    void EnemyGetRandom()
    {
        enemyType = Random.Range(0, TrainManager.instance.trainContainer.Count);
        transform.rotation = new Quaternion(0, 0, 0, 0);
        waist.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    void EnemyTargettingMove()
    {
        transform.position = Vector3.MoveTowards(transform.position, TrainManager.instance.trainContainer[enemyType].transform.position,
        enemySpeed * Time.deltaTime);
    }

    void EnemyDie()
    {
        isEnemyMove = true;
        GameObject scrap = ObjectPool.instacne.GetObject(Resources.Load<GameObject>("Scrap"));
        scrap.transform.position = this.transform.position;
        this.gameObject.SetActive(false);
    }


    private void OnMouseEnter()
    {
        Debug.Log("target");
        PlayerInput.Instance.isEnemy = true;
    }

    private void OnMouseExit()
    {
        PlayerInput.Instance.isEnemy = false;
    }
}
