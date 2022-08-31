using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningMagnifyingGlass : TrainItem
{
    public float expIncrease = 0.03f;

    public override void ItemEffect()
    {
        GameManager.Instance.LearningMagnifyingGlass(expIncrease);
        curCarry++;
    }
}