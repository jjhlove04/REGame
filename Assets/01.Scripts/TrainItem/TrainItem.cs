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
    public string itemStr;
    public Sprite itemImage;
    public int curCarry;
    public Color bufColor;
    public int needGold;

    public abstract void ItemEffect();
}
