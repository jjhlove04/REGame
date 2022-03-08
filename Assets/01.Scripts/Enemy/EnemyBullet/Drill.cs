using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : MonoBehaviour
{
    [SerializeField]
    private float damage;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Train")
        {
            TrainScript.instance.Damage(damage * Time.deltaTime);
            other.GetComponent<TrainHit>()?.Hit();
        }

        else if (other.tag == "Turret")
        {
            other.GetComponent<HealthSystem>()?.Damage(damage * Time.deltaTime);
        }
    }
}
