using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletWideArea : ProjectileMover
{
    [SerializeField]
    private float m_MaxDistance;

    [SerializeField]
    private LayerMask m_Mask;

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground") && targetEnemy.GetComponent<Enemy>().isDying)
        {
            base.OnTriggerEnter(other);
            WideAreaAttack();
        }

        else if (other.tag == "Enemy")
        {

            base.OnTriggerEnter(other);
            WideAreaAttack();
            //DamageText.Create(targetEnemy.position, damage,new Color(1,42/255,42/255));
        }
    }

    private void WideAreaAttack()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, m_MaxDistance, m_Mask);

        HealthSystem healthSystem = new HealthSystem();

        for (int i = 0; i < collider.Length; i++)
        {
            if (collider[i].gameObject.activeSelf)
            {
                healthSystem = collider[i].GetComponent<HealthSystem>();

                healthSystem.WideAreaDamge(damage);
            }
        }
    }
}