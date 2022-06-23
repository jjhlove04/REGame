using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretItemList : MonoBehaviour
{
    public List<ItemVO> itemList = new List<ItemVO>();
    testScripttss testScript;
    private void Start()
    {
        testScript = testScripttss.Instance;
    }

    public void ItemInTurret()
    {
        for (int i = 0; i < testScript.turretData.Count; i++)
        {
            if(testScript.turretData[i].TryGetComponent<Turret>(out Turret turret))
            {
                
            }
        }
        
    }
}
