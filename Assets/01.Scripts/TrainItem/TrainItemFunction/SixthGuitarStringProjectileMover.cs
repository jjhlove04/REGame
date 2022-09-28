using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SixthGuitarStringProjectileMover : MonoBehaviour
{
    Transform targetEnemy;

    float damage;

    int count =4;

    public LayerMask enemy;

    [SerializeField]
    private GameObject start;
    [SerializeField]
    private GameObject end;

    private ObjectPool objectPool;

    private void OnEnable()
    {
        StartCoroutine(AttackElectric());
    }

    private void Start()
    {
        objectPool = ObjectPool.instacne;
    }

    private void ObjReturn()
    {
        objectPool.ReturnGameObject(gameObject);
    }

    IEnumerator AttackElectric()
    {
        for (int i = 0; i < 4; i++)
        {
            end.transform.position = targetEnemy.position;
            yield return new WaitForSeconds(0.1f);
            start.transform.position = targetEnemy.position;
            NewTarget(targetEnemy.position);
        }

        ObjReturn();
    }

    private void AttackDamage()
    {
        targetEnemy.GetComponent<HealthSystem>().Damage(damage);
    }

    public void Create(Transform targetEnemy, float damage)
    {
        this.targetEnemy = targetEnemy;
        this.damage = damage;

        start.transform.position = targetEnemy.position;
        end.transform.position = targetEnemy.position;
    }

    private void NewTarget(Vector3 targetPos)
    {
        Collider[] hit = Physics.OverlapSphere(targetPos, 50, enemy);

        Transform targetEnemy = null;

        for (int i = 0; i < count; i++)
        {
            Enemy enemy = hit[i].GetComponent<Enemy>();
            if (hit[i].CompareTag("Enemy"))
            {
                if (!enemy.isDying)
                {
                    if (targetEnemy != null)
                    {
                        if (Vector3.Distance(targetPos, hit[i].transform.position) < Vector3.Distance(transform.position, targetEnemy.transform.position))
                        {
                            targetEnemy = hit[i].transform;
                        }
                    }

                    else
                    {
                        targetEnemy = hit[i].transform;
                    }

                }
            }
        }

        this.targetEnemy = targetEnemy;

        AttackDamage();
    }
}
