using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsOfVictory : TrainItem
{
    public override void ItemEffect()
    {
        GameManager.Instance.OnNewsOfVictory();

        curCarry++;
    }
}