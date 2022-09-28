using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum Grade
{
    Normal,
    Rare
}

public abstract class TrainItem : MonoBehaviour
{

    public Grade grade;
    public int itemNum;
    public string itemEffect;
    public string itemStr;
    public Sprite itemImage;
    public int curCarry = 1;
    public Color bufColor;
    public int needGold;

    public abstract void ItemEffect();
}
