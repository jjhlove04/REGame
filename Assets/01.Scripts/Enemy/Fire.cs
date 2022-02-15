using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField]
    private int damage;

    private void OnParticleCollision(GameObject other)
    {
        if(other.tag == "Train")
        {
            TrainScript.instance.Damage(damage * Time.deltaTime);
        }

        else if(other.tag == "Turret")
        {
            other.GetComponent<HealthSystem>()?.Damage(damage * Time.deltaTime);
        }
    }
}
