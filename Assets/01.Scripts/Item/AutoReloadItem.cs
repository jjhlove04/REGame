
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoReloadItem : Item
{
    protected Transform turrets;

    private void Start()
    {
        turrets = TrainManager.instance.transform.Find("Turrets");
    }

    private void Update()
    {
        print(useItem);

        if (useItem)
        {
            for (int i = 0; i < turrets.childCount; i++)
            {
                Turret turret = turrets.GetChild(i).GetComponent<Turret>();

                if (turret.IsNeedReload())
                {
                    turret.Reload();
                }
            }
        }
    }

    public override void UseItem()
    { 
        base.UseItem();

        itemUI.transform.Find("Background").gameObject.SetActive(!itemUI.transform.Find("Background").gameObject.activeSelf);

        useItem = !useItem;
    }

    public override void GetItemUI(GameObject UI)
    {
        itemUI = UI;
    }
}