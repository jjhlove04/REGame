using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hemostatic : TrainItem
{
    public override void ItemEffect()
    {
        TurretManager.Instance.Hemostatic();
    }
}
