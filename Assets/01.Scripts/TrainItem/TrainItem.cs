using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class TrainItem : MonoBehaviour
{
    public enum Grade
    {
        Normal,
        Rare
    }

    public string itemEffect;
    public Sprite itemImage;

    public abstract void ItemEffect();
}
