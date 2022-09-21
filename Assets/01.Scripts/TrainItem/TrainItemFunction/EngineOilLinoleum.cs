using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineOilLinoleum : MonoBehaviour
{
    [SerializeField]
    private float m_MaxDistance;

    public LayerMask m_Mask;

    private float damage;

    private ObjectPool objectPool;

    private void OnEnable()
    {
        objectPool = ObjectPool.instacne;
    }

    private void Start()
    {
        transform.localScale = new Vector3(m_MaxDistance, 0.001f, m_MaxDistance);
    }

    private void Update()
    {
        EngineOilLinoleumAttack();
    }

    public void Create(float damage)
    {
        this.damage = damage;
    }

    private void EngineOilLinoleumAttack()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, m_MaxDistance, m_Mask);

        HealthSystem healthSystem = new HealthSystem();

        for (int i = 0; i < collider.Length; i++)
        {
            if (collider[i].gameObject.activeSelf)
            {
                healthSystem = collider[i].GetComponent<HealthSystem>();

                healthSystem.OnEngineOil(2,0.5f,damage);
            }
        }
    }
}
