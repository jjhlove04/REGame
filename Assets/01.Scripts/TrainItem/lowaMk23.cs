using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lowaMk23 : TrainItem
{
    public override void ItemEffect()
    {
        TurretManager.Instance.OnLowaMk23();
    }
}
