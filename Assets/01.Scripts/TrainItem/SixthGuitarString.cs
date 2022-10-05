using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SixthGuitarString : TrainItem
{
    public override void ItemEffect()
    {
        TurretManager.Instance.OnSixthGuitarString();
        curCarry++;
    }
}
