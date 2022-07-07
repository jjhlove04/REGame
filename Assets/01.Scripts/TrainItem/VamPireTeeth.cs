using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VamPireTeeth : TrainItem
{
    public override void ItemEffect()
    {
        GameManager.Instance.OnVamPireTeeth();

        curCarry++;
    }
}