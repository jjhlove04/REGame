using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedSeriesLaunches : TrainItem
{
    [SerializeField]
    GameObject speedSeriesLaunchesObj;
    public override void ItemEffect()
    {
        TurretManager.Instance.OnSpeedSeriesLaunches(speedSeriesLaunchesObj);

        curCarry++;
    }
}
