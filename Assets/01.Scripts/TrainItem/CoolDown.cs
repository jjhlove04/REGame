using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDown : TrainItem
{
    public override void ItemEffect()
    {
        TrainScript.instance.OnCoolDown();
    }
}
