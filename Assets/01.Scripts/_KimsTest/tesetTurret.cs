using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tesetTurret : MonoBehaviour
{
    public bool onTurret;
    private int maxBulletAmount;
    public int curBulletAmount;
    void Start()
    {
        //testScriptts.Instance.turretPoses.Add(this.gameObject.transform);
    }
    public void Reload()
    {
        curBulletAmount = maxBulletAmount;
    }
}
