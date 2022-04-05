using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHumanoidBullet : MonoBehaviour
{

    EnemyHumanoidRig enemyHumanoidRig;
    private void Start()
    {
        enemyHumanoidRig = transform.root.GetComponent<EnemyHumanoidRig>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Train"))
        {
            TrainScript.instance.Damage(enemyHumanoidRig.GetDamage() * Time.deltaTime);
            other.GetComponent<TrainHit>()?.Hit();
        }

        else if (other.CompareTag("Turret"))
        {
            other.GetComponent<HealthSystem>()?.Damage(enemyHumanoidRig.GetDamage() * Time.deltaTime);
        }
    }
}