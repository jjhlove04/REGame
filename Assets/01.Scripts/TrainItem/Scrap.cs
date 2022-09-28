using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrap : TrainItem
{
    public override void ItemEffect()
    {
        GameManager.Instance.OnScrap();
        curCarry++;
    }
}
