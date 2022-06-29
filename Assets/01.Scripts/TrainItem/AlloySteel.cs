using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlloySteel : TrainItem
{
    public float rateOfRise = 0.08f;

    public override void ItemEffect()
    {
        TrainScript.instance.AlloySteel(rateOfRise);
    }
}