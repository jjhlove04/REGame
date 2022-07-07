using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningMagnifyingGlass : TrainItem
{
    public float goldIncrease = 0.08f;

    public override void ItemEffect()
    {
        GameManager.Instance.LearningMagnifyingGlass(goldIncrease);

        curCarry++;
    }
}
