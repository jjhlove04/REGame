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

    public GameObject itemUI;

    public virtual void UseItem()
    {
        itemUI.transform.Find("Background").gameObject.SetActive(!itemUI.transform.Find("Background").gameObject.activeSelf);
    }
}