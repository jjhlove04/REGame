using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchGun : TrainItem
{
    public override void ItemEffect()
    {
        TurretManager.Instance.OnPunchGun();

        curCarry++;
    }
}