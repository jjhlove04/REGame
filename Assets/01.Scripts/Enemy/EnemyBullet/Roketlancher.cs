using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roketlancher : MonoBehaviour
{
    // �̵�
    public float moveSpeed = 50;

    [SerializeField]
    private GameObject particle;
    // ������ �ε����� �������� �ְ� ���� �������

    public void Create(Vector3 pos,Transform enemy, int damage)
    {
        SpawnPos(pos);
        SetTarget(enemy);
        Damage(damage);
    }


    private Transform targetEnemy;
    private Vector3 lastMoveDir;
    private int damage;
    private ObjectPool objPool;

    private void Start()
    {
        objPool = FindObjectOfType<ObjectPool>();
    }

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
    }

    void SetTarget(Transform enemy)
    {
        this.targetEnemy = enemy;
    }

    public void Damage(int damage)
    {
        this.damage = damage;
    }

    private void SpawnPos(Vector3 pos)
    {
        gameObject.transform.position = pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Train")
        {
            TrainScript.instance.Damage(damage);
            other.GetComponent<TrainHit>()?.Hit();
            SpawnParticle();
            gameObject.SetActive(false);
        }

        else if (other.tag == "Turret")
        {
            other.GetComponent<HealthSystem>()?.Damage(damage);
            SpawnParticle();
            gameObject.SetActive(false);
        }
    }

    private void SpawnParticle()
    {
        GameObject particleObj = objPool.GetObject(particle);
        particleObj.transform.position = transform.position;
    }
}
