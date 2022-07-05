using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overclokcing : TrainItem
{
    public float increase = 0.15f;

    private GameObject turrets;

    public override void ItemEffect()
    {
        turrets = TurretManager.Instance.turrets;

        for (int i = 0; i < turrets.transform.childCount; i++)
        {
            turrets.transform.GetChild(i).GetComponent<Turret>().Overclokcing(increase);
        }
    }
}