using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DryOil : TrainItem
{
    public override void ItemEffect()
    {
        TurretManager.Instance.OnDryOil();
        curCarry++;
    }
}
