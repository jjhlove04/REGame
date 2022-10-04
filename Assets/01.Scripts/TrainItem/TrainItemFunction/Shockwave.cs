using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    [SerializeField]
    private float m_MaxDistance;

    public LayerMask m_Mask;

    private float damage;

    private int count;

    private ObjectPool objectPool;

    TestTurretDataBase testTurretDataBase;
    private void Awake()
    {
        testTurretDataBase = TestTurretDataBase.Instance;
        m_MaxDistance += m_MaxDistance * (testTurretDataBase.plusDistance / 100);
        transform.localScale = new Vector3(m_MaxDistance, 0.001f, m_MaxDistance);
    }

    private void Start()
    {
        objectPool = ObjectPool.instacne;
    }

    public void Create(float damage,int count)
    {
        this.damage = damage;

        this.count = count;

        StartCoroutine(ShockwaveAttackCoroutine());
    }

    private void ShockwaveAttack()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, m_MaxDistance, m_Mask);

        HealthSystem healthSystem = new HealthSystem();

        int count = collider.Length;

        if (count >= this.count)
        {
            count = this.count;
        }

        for (int i = 0; i < count; i++)
        {
            if (collider[i].gameObject.activeSelf)
            {
                healthSystem = collider[i].GetComponent<HealthSystem>();

                healthSystem.LaveLamp(damage);
            }
        }
    }

    IEnumerator ShockwaveAttackCoroutine()
    {
        for (int i = 0; i < 4; i++)
        {
            ShockwaveAttack();

            yield return new WaitForSeconds(1);
        }

        objectPool.ReturnGameObject(gameObject);
    }
}
