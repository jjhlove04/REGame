using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperGrappler : TrainItem
{
    public override void ItemEffect()
    {
        GameManager.Instance.OnBumperGrappler();
    }
}
