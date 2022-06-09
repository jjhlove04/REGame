using UnityEngine;

public enum ItemType
{
    Active,
    Passive
}

public abstract class Item : MonoBehaviour
{
    public ItemType itemType;

    protected GameObject itemUI;

    protected bool useItem;

    public int count;

    public Sprite icon;

    public int itemNum;

    protected virtual void Update()
    {
        KeyDown();
    }

    public abstract void UseItem();

    public abstract void GetItemUI(GameObject UI);

    public virtual void KeyDown()
    {
        switch (itemNum)
        {
            case 1:
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    UseItem();
                }
                    break;
            case 2:
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    UseItem();
                }
                break;
            case 3:
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    UseItem();
                }
                break;
            case 4:
                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    UseItem();
                }
                break;
            default:
                break;
        }
    }

    public virtual void UnUseItem()
    {
        useItem = false;
    }
}