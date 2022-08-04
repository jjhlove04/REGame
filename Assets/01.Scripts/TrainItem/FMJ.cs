using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMJ : TrainItem
{
    public override void ItemEffect()
    {
        TurretManager.Instance.OnFMJ();

        curCarry++;
    }
}