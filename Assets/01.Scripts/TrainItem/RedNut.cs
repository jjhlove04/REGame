using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedNut : TrainItem
{
    public override void ItemEffect()
    {
        TurretManager.Instance.OnRedNut();

        curCarry++;
    }
}