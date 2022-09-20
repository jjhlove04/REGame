using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningCoalProjectileMover : MonoBehaviour
{
    private Transform targetEnemy;
    private float damage;

    private float moveSpeed = 100;

    [SerializeField]
    private LayerMask m_Mask;

    [SerializeField]
    private GameObject explosionEffect;


    void FixedUpdate()
    {
        Vector3 moveDir;

        if (targetEnemy != null && !targetEnemy.GetComponent<Enemy>().isDying)
        {
            moveDir = (targetEnemy.transform.position - transform.position).normalized;

            // 이동
            transform.position += moveDir * moveSpeed * Time.deltaTime;

            //회전값 적용
            transform.LookAt(targetEnemy);
        }

        else
        {
            gameObject.SetActive(false);
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        ObjectPool.instacne.GetObject(explosionEffect).transform.position = transform.position;

        gameObject.SetActive(false);

        HealthSystem healthSystem = other.GetComponent<HealthSystem>();

        if (other.tag == "Enemy")
        {
            healthSystem.Damage(damage);

            //DamageText.Create(targetEnemy.position, damage,new Color(1,42/255,42/255));
        }
    }

    public BurningCoalProjectileMover Create(float damage, Transform targetEnemy)
    {
        this.damage = damage;

        this.targetEnemy = targetEnemy;

        return this;
    }
}
