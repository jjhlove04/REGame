using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roketlancher : MonoBehaviour
{
    // 이동
    public float moveSpeed = 50;

    [SerializeField]
    private GameObject particle;
    // 적에게 부딪히면 데미지를 주고 나는 사라진다

    public static Roketlancher Create(Vector3 pos, Transform enemy, int damage)
    {
        Transform bulletPrefab = Resources.Load<Transform>("Missile");
        Transform bulletTrm = Instantiate(bulletPrefab, pos, Quaternion.identity);

        Roketlancher bulletProjectile = bulletTrm.GetComponent<Roketlancher>();

        bulletProjectile.SetTarget(enemy);
        bulletProjectile.Damage(damage);

        return bulletProjectile;

    }


    private Transform targetEnemy;
    private Vector3 lastMoveDir;
    private int damage;


    private void Update()
    {
        Vector3 moveDir;
        if (targetEnemy != null)
        {
            moveDir = (targetEnemy.transform.position - transform.position).normalized;
            lastMoveDir = moveDir;
        }

        else
        {
            moveDir = lastMoveDir;
        }

        // 이동
        transform.position += moveDir * moveSpeed * Time.deltaTime;


        //회전값 적용
        transform.LookAt(targetEnemy);
    }

    void SetTarget(Transform enemy)
    {
        this.targetEnemy = enemy;
    }

    public void Damage(int damage)
    {
        this.damage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Train")
        {
            TrainScript.instance.Damage(damage);
            other.GetComponent<TrainHit>()?.Hit();
            Instantiate(particle, transform.position, Quaternion.identity);
        }

        else if (other.tag == "Turret")
        {
            other.GetComponent<HealthSystem>()?.Damage(damage * Time.deltaTime);
            Instantiate(particle, transform.position, Quaternion.identity);
        }

        gameObject.SetActive(false);
    }
}
