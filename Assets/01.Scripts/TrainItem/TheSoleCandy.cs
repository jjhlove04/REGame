using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheSoleCandy : TrainItem
{
    public override void ItemEffect()
    {
        TurretManager.Instance.OnTheSoleCandy();

        curCarry++;
    }
}
