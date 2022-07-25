using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurryBracelet : TrainItem
{
    private GameObject turrets;

    public override void ItemEffect()
    {
        turrets = TurretManager.Instance.turrets;

        for (int i = 0; i < turrets.transform.childCount; i++)
        {
            turrets.transform.GetChild(i).GetComponent<Turret>().OnFurryBracelet();
        }
    }
}
