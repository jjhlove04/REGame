using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireEntanglement : TrainItem
{
    public override void ItemEffect()
    {
        TrainScript.instance.OnWireEntanglement();
        curCarry++;
    }
}
