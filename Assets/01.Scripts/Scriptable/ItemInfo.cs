using UnityEngine;

[CreateAssetMenu(fileName = "ItemInfo", menuName = "Scriptable Object/ItemInfo", order = int.MaxValue)]
public class ItemInfo : ScriptableObject
{
    public GameObject item;

    public int price = 10;

    public int maxCountPrice;

    public int countPriceUp;

    public int maxCount = 1;
}