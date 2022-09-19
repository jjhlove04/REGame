using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaveLampProjectile : MonoBehaviour
{
    [SerializeField]
    private float m_MaxDistance;

    public LayerMask m_Mask;

    private float damage;

    private ObjectPool objectPool;

    private void OnEnable()
    {
        objectPool = ObjectPool.instacne;

        Invoke("ObjReturn", 0.25F);

        LaveLampProjectileAttack();
    }

    private void Awake()
    {
        transform.localScale = new Vector3(m_MaxDistance, 0.001f, m_MaxDistance);
    }
    private void ObjReturn()
    {
        objectPool.ReturnGameObject(this.gameObject);
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

                rigidbody.velocity = Vector3.up * 5;

                healthSystem.LaveLamp(damage);
            }
        }
    }
}