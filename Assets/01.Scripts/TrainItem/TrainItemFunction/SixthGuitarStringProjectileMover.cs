using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SixthGuitarStringProjectileMover : MonoBehaviour
{
    Transform targetEnemy;

    float damage;

    public void Create(Transform targetEnemy, float damage)
    {
        this.targetEnemy = targetEnemy;
        this.damage = damage;
    }


}
