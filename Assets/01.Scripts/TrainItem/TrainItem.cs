using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TrainItem
{
    public enum Grade
    {
        Normal,
        Rare
    }

    public string itemEffect;

    public abstract void ItemEffect();
}
