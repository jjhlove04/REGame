using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : MonoBehaviour
{
    EnemyDrill enemyDrill;
    private void Start()
    {
        enemyDrill = transform.root.GetComponent<EnemyDrill>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Train")
        {
            TrainScript.instance.Damage(enemyDrill.GetDamage() * Time.deltaTime);
            other.GetComponent<TrainHit>()?.Hit();
        }

        else if (other.tag == "Turret")
        {
            other.GetComponent<HealthSystem>()?.Damage(enemyDrill.GetDamage() * Time.deltaTime);
        }
    }
}
