using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // �̵�
    public float moveSpeed=50;
    // ������ �ε����� �������� �ְ� ���� �������

    public static Bullet Create(Vector3 pos, Transform enemy, int damage)
    {
        Transform bulletPrefab = Resources.Load<Transform>("Ef_IceMagicGlowFree01");
        Transform bulletTrm = Instantiate(bulletPrefab, pos, Quaternion.identity);

        Bullet bulletProjectile = bulletTrm.GetComponent<Bullet>();

        bulletProjectile.SetTarget(enemy);
        bulletProjectile.Damage(damage);

        return bulletProjectile;

    }


    private Transform targetEnemy;
    private Vector3 lastMoveDir;
    private float timeToDie = 2f;
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

        // �̵�
        transform.position += moveDir * moveSpeed * Time.deltaTime;


        //ȸ���� ����
        transform.LookAt(targetEnemy);

        // Ÿ�̸� ó��
        timeToDie -= Time.deltaTime;
        if (timeToDie < 0f)
        {
            Destroy(gameObject);
        }
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
        if (other.tag == "Enemy")
        {
            other.GetComponent<HealthSystem>().Damage(damage);
            DamageText.Create(targetEnemy.position, damage,new Color(1,42/255,42/255));
            this.gameObject.SetActive(false);
        }
    }
}