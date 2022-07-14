using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveShield : TrainItem
{
    public override void ItemEffect()
    {
        TrainScript.instance.OnExplosiveShield();

        TrainItemManager.Instance.ExplosiveShield();

        curCarry++;
    }
}