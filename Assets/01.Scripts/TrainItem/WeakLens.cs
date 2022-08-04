using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakLens : TrainItem
{
    public override void ItemEffect()
    {
        TurretManager.Instance.OnWeakLens();

        curCarry++;
    }
}