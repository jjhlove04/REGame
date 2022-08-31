using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreBigWallet : TrainItem
{
    public float goldIncrease = 0.04f;

    public override void ItemEffect()
    {
        GameManager.Instance.MoreBigWallet(goldIncrease);
        curCarry++;
    }
}