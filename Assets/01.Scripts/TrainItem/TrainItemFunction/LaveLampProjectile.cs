using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaveLampProjectile : MonoBehaviour
{
    [SerializeField]
    private float m_MaxDistance;

    public LayerMask m_Mask;

    private float damage;
    TestTurretDataBase m_TurretData;

    private void Awake()
    {
        m_TurretData = TestTurretDataBase.Instance;

        m_MaxDistance += m_MaxDistance * (m_TurretData.plusDistance / 100);
        transform.localScale = new Vector3(m_MaxDistance, 0.001f, m_MaxDistance);
    }

    private void Update()
    {
        LaveLampProjectileAttack();
    }

    public void Create(float damage)
    {
        this.damage = damage;
    }

    private void LaveLampProjectileAttack()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, m_MaxDistance, m_Mask);

        HealthSystem healthSystem = new HealthSystem();

        Rigidbody rigidbody = new Rigidbody();

        for (int i = 0; i < collider.Length; i++)
        {
            if (collider[i].gameObject.activeSelf)
            {
                healthSystem = collider[i].GetComponent<HealthSystem>();

                rigidbody = collider[i].GetComponent<Rigidbody>();

                rigidbody.velocity = Vector3.up * 10 * Time.deltaTime;

                healthSystem.LaveLamp(damage);
            }
        }
    }
}
