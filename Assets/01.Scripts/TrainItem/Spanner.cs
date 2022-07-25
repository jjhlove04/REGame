using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spanner : TrainItem
{
    public override void ItemEffect()
    {
        TrainScript.instance.OnSpanner();
    }
}
