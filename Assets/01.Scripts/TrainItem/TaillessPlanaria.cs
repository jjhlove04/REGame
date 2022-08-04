using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaillessPlanaria : TrainItem
{
    public override void ItemEffect()
    {
        TurretManager.Instance.OnTaillessPlanaria();

        curCarry++;
    }
}
