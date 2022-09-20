using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowaMk23 : TrainItem
{
    public override void ItemEffect()
    {
        TurretManager.Instance.OnLowaMk23();
    }
}
