using UnityEngine;

public class BaseBullet : ProjectileMover
{
    protected override void OnTriggerEnter(Collider other)
    {
        HealthSystem healthSystem = other.GetComponent<HealthSystem>();

        if (other.CompareTag("Ground") && targetEnemy.GetComponent<Enemy>().isDying)
        {
            base.OnTriggerEnter(other);
        }

        else if (other.tag == "Enemy")
        {
            base.OnTriggerEnter(other);

            if (onFurryBracelet)
            {
                healthSystem.FurryBracelet(time);
            }

            if (onHemostatic)
            {
                healthSystem.DotDamageCoroutine(hemostaticParticle, 4,0.5f,hemostaticDamage);
            }

            DryOil(healthSystem);

            healthSystem.Damage(Damage);

            //DamageText.Create(targetEnemy.position, damage,new Color(1,42/255,42/255));
        }
    }
}