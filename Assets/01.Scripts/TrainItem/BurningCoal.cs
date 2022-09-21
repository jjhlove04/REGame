using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningCoal : TrainItem
{
    public override void ItemEffect()
    {
        TrainManager.instance.OnBurningCoal();
        curCarry++;
    }
}
