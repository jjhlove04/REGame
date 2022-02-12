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
    private float disX;

    [SerializeField]
    float distance;

    [SerializeField]
    bool isEnemyMove = true;
    Rigidbody rigid;

    HealthSystem healthSystem;

    public GameObject waist;

    public Animator anim;
    public float animTime;
    private float atime = 5f;

    private bool isAttack =false;

    private void OnEnable()
    {
        EnemyGetRandom();
        distance = Random.Range(15, 20);
        disX = Random.Range(distanceX - 1, distanceX);
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
        Vector3 dir = TrainManager.instance.trainContainer[enemyType].transform.position - transform.position;
        Quaternion rot = Quaternion.LookRotation(new Vector3(dir.x, dir.y, dir.z+ TrainManager.instance.trainContainer.Count *25));
        if ((Vector3.Distance(transform.position, TrainManager.instance.trainContainer[enemyType].transform.position) > distance || Mathf.Abs(transform.position.x - TrainManager.instance.trainContainer[enemyType].transform.position.x) > disX) && isEnemyMove )
        {
            EnemyTargettingMove();
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * 5);
        }
        else
        {
            rot = Quaternion.LookRotation(dir);

            Quaternion quaternion = Quaternion.identity;
            quaternion.eulerAngles = new Vector3(0, 0, 0);
            waist.transform.rotation = quaternion;

            if (anim.GetBool("IsAttack"))
            {
                StartCoroutine(Attacking());
                transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * 5);
            }

            if (isAttack)
            {

                transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * 5);
            }

            else
            {
                rot = Quaternion.LookRotation(Vector3.zero);
                transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime *5);
            }

            anim.SetBool("IsAttack", false);

            animTime += Time.deltaTime;

            if (animTime >= atime)
            {
                anim.SetBool("IsAttack", true);
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
        transform.position =  Vector3.MoveTowards(transform.position, TrainManager.instance.trainContainer[enemyType].transform.position,
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

    private IEnumerator Attacking()
    {
        isAttack = true;
        yield return new WaitForSeconds(2f);
        isAttack = false;
    }
}
