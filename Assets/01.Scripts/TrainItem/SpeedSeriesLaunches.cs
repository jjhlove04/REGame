using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedSeriesLaunches : TrainItem
{
    public override void ItemEffect()
    {
        TurretManager.Instance.OnSpeedSeriesLaunches();

        curCarry++;
    }
}
