using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    testFire enemyFireAttack;
    private void Start()
    {
        enemyFireAttack = transform.root.GetComponent<testFire>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if(other.tag == "Train")
        {
            TrainScript.instance.Damage(enemyFireAttack.GetDamage() * Time.deltaTime);
            other.GetComponent<TrainHit>()?.Hit();
        }

        else if(other.tag == "Turret")
        {
            Debug.Log(enemyFireAttack);
            other.GetComponent<HealthSystem>()?.Damage(enemyFireAttack.GetDamage() * Time.deltaTime);
        }
    }
}
