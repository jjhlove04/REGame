using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roketlancher : MonoBehaviour
{
    // 이동
    public float moveSpeed = 50;
    private float missileSpeed = 50;

    [SerializeField]
    private GameObject particle;

    private Rigidbody rigid;

    public float turnSpeed = 1f;
    public float rocketFlySpeed = 10f;

    // 적에게 부딪히면 데미지를 주고 나는 사라진다

    public void Create(Vector3 pos, Transform enemy, float damage)
    {
        SpawnPos(pos);
        SetTarget(enemy);
        Damage(damage);
    }


    private Transform targetEnemy;
    private float damage;

    //private Vector3 target;

    //private bool guided = false;

    //private bool tracking = false;

    /*private void OnEnable()
    {
        StopCoroutine(WaitGuided());
        StartCoroutine(WaitGuided());
    }*/

    private void OnDisable()
    {
        transform.rotation = Quaternion.identity;
        //tracking = false;
        //guided = false;
    }

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!rigid)
            return;

        // 전방을 바라보며 속도를 유지함
        rigid.velocity = transform.forward * rocketFlySpeed;

        // 방향을 계속하여 타겟팅을 주시
        var rocketTargetRot = Quaternion.LookRotation(targetEnemy.position - transform.position + new Vector3(0,10,0));

        // 움직이며 방향을 전환(회전 스피드까지 적용)
        rigid.MoveRotation(Quaternion.RotateTowards(transform.rotation, rocketTargetRot, turnSpeed));

        /*Quaternion rot = new Quaternion();

        //회전값 적용
        if (guided)
        {
            if (targetEnemy != null)
            {
                target = (targetEnemy.position - transform.position).normalized;
                rot = Quaternion.LookRotation(target);
            }

            transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * 20f);
        }

        if (Mathf.Abs(transform.rotation.eulerAngles.y - rot.eulerAngles.y) <= 1 && !tracking)
        {
            missileSpeed = moveSpeed;
            rigid.velocity = new Vector3(0, 0, rigid.velocity.z);
            tracking = true;
        } 


        else
        {
            missileSpeed = 25f;
        }

        // 이동
        rigid.velocity += transform.forward * missileSpeed * Time.deltaTime;

        if (!RocketRgb)
            return;

        // 전방을 바라보며 속도를 유지함
        RocketRgb.velocity = rocketLocalTrans.forward * rocketFlySpeed;

        // 방향을 계속하여 타겟팅을 주시
        var rocketTargetRot = Quaternion.LookRotation(RocketTarget.position - rocketLocalTrans.position);

        // 움직이며 방향을 전환(회전 스피드까지 적용)
        RocketRgb.MoveRotation(Quaternion.RotateTowards(rocketLocalTrans.rotation, rocketTargetRot, turnSpeed));*/
    }

    /*IEnumerator WaitGuided()
    {
        rigid.velocity = Vector3.zero;
        transform.rotation = Quaternion.identity;

        yield return new WaitForSeconds(0.5f);
        guided = true;
    }*/

    void SetTarget(Transform enemy)
    {
        this.targetEnemy = enemy;
    }

    public void Damage(float damage)
    {
        this.damage = damage;
    }

    private void SpawnPos(Vector3 pos)
    {
        gameObject.transform.position = pos;
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Train"))
        {
            TrainScript.instance.Damage(damage);
            SpawnParticle();
            gameObject.SetActive(false);
            CameraManager.Instance.Shake(0.5f, 2f);
        }

        else if (other.CompareTag("Turret"))
        {
            other.GetComponent<HealthSystem>()?.Damage(damage);
            SpawnParticle();
            gameObject.SetActive(false);
        }
    }*/

    private void SpawnParticle()
    {
        GameObject particleObj = ObjectPool.instacne.GetObject(particle);
        particleObj.transform.position = transform.position;
    }
}