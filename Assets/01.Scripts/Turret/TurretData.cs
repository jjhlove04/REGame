using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Turret Data", menuName = "Scriptable Object/Turret Data", order = int.MaxValue)]
public class TurretData : ScriptableObject
{
    public GameObject turret;

    public int damage;

    public int maxBulletAmount;

    public float maxDistance;

    public float shootTimerMax;

    public float turretPrice;
    public float reloadPrice;
}
