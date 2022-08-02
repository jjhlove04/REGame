using UnityEngine;

public class BaseBullet : ProjectileMover
{
    protected override void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ground") && targetEnemy.GetComponent<Enemy>().isDying)
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

            other.GetComponent<HealthSystem>().Damage(damage);

            //DamageText.Create(targetEnemy.position, damage,new Color(1,42/255,42/255));
        }
    }
}