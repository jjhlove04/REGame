using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeMemoryAlloy : TrainItem
{
    public override void ItemEffect()
    {
        GameManager.Instance.OnShapeMemoryAlloy();
    }
}
