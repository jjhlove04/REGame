using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Active,
    Passive
}

public class Item : MonoBehaviour
{
    public ItemType itemType;

    protected GameObject itemUI;

    protected bool useItem;

    public virtual void UseItem() { }

    public virtual void GetItemUI(GameObject UI) { }

}