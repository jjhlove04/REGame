using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMJBullet : ProjectileMover
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground") && targetEnemy.GetComponent<Enemy>().isDying)
        {
            base.OnTriggerEnter(other);
        }

        else if (other.tag == "Enemy")
        {
            base.OnTriggerEnter(other);

            if (onFurryBracelet)
            {
                other.GetComponent<HealthSystem>().FurryBracelet(time);
            }

            other.GetComponent<HealthSystem>().FMJ(damage, additionalDamage);
            //DamageText.Create(targetEnemy.position, damage,new Color(1,42/255,42/255));
        }
    }
}
