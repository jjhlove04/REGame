using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskOfCriminal : TrainItem
{
    public override void ItemEffect()
    {
        TrainScript.instance.OnMaskOfCriminal();

        curCarry++;
    }
}