using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overclokcing : TrainItem
{
    public float increase = 0.15f;

    public override void ItemEffect()
    {
        TurretManager.Instance.Overclokcing(increase);

        curCarry++;
    }
}