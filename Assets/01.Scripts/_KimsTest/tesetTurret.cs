using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tesetTurret : MonoBehaviour
{
    public bool onTurret;
    private int maxBulletAmount;
    public int curBulletAmount;
    public int turType;
    void Start()
    {
        testScriptts.Instance.turretPoses.Add(this.gameObject.transform);
        testScriptts.Instance.turretData.Add(this.gameObject);
    }
    public void Reload()
    {
        curBulletAmount = maxBulletAmount;
    }
}
