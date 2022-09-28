using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveGenerator : TrainItem
{
    [SerializeField]
    private GameObject shockwaveGeneratorObj;
    public override void ItemEffect()
    {
        TurretManager.Instance.OnShockwaveGenerator(shockwaveGeneratorObj);
        curCarry++;
    }
}
