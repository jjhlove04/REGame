using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    EnemyFireAttack enemyFireAttack;
    private void Start()
    {
        enemyFireAttack = transform.root.GetComponent<EnemyFireAttack>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if(other.CompareTag("Train"))
        {
            TrainScript.instance.Damage(enemyFireAttack.GetDamage() * Time.deltaTime);
            other.GetComponent<TrainHit>()?.Hit();
        }

        else if(other.CompareTag("Turret"))
        {
            other.GetComponent<HealthSystem>()?.Damage(enemyFireAttack.GetDamage() * Time.deltaTime);
        }
    }
}
