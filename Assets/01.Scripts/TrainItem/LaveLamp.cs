using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaveLamp : TrainItem
{
    public override void ItemEffect()
    {
        GameManager.Instance.OnLaveLamp();
    }
}