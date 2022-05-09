
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoReloadItem : Item
{
    private bool useAutoReload;

    private Transform turrets;

    private void Start()
    {
        turrets = TrainManager.instance.transform.Find("Turrets");
    }

    private void Update()
    {
        if (!useAutoReload) return;

        for (int i = 0; i < turrets.childCount; i++)
        {
            Turret turret = turrets.GetChild(i).GetComponent<Turret>();

            if (turret.IsNeedReload())
            {
                turret.Reload();
            }
        }
    }

    public override void UseItem()
    {
        base.UseItem();
        useAutoReload = !useAutoReload;
    }
}