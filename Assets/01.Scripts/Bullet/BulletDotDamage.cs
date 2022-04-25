using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDotDamage : ProjectileMover
{
    [SerializeField]
    private GameObject particle;

    [SerializeField]
    private float dotDelay = 0.5f;

    [SerializeField]
    private float dotTime = 3;

    private int dotcount;

    [SerializeField]
    private float dotDamage;

    private void Start()
    {
        dotcount = (int)(dotTime / dotDelay);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground") && targetEnemy.GetComponent<Enemy>().isDying)
        {
            base.OnTriggerEnter(other);
        }

        else if (other.tag == "Enemy")
        {
            base.OnTriggerEnter(other);

            HealthSystem healthSystem = other.GetComponent<HealthSystem>();

            healthSystem.DotDamageCoroutine(particle, dotcount, dotDelay, dotDamage);

            healthSystem.Damage(damage);
        }
    }
}
