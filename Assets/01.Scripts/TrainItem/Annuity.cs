using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Annuity : TrainItem
{
    public override void ItemEffect()
    {
        GameManager.Instance.OnAnnuity();
    }
}
