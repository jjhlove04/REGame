using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField]
    private int damage;

    private void OnParticleCollision(GameObject other)
    {
        other.GetComponent<HealthSystem>()?.Damage(damage);

        Debug.Log(other.name );
        Debug.Log(other.GetComponent<HealthSystem>());
    }
}
