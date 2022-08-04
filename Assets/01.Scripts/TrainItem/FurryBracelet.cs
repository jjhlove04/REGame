using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurryBracelet : TrainItem
{
    public override void ItemEffect()
    {
        TurretManager.Instance.OnFurryBracelet();

        curCarry++;
    }
}
