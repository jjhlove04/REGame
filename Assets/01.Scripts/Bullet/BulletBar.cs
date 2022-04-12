using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBar : MonoBehaviour
{
    private Transform barTrm;

    private void Awake()
    {
        barTrm = transform.Find("bar");
    }

    // Update is called once per frame

    private void Update()
    {
        transform.parent.LookAt(Camera.main.transform);
    }

    public void UpdateBar(int curBulletAmount, int bulletAmountMax)
    {
        barTrm.localScale = new Vector3(GetBulletAmounetNomalized(curBulletAmount, bulletAmountMax), 1, 1);
    }

    private float GetBulletAmounetNomalized(int curBulletAmount, int bulletAmountMax)
    {
        return (float)curBulletAmount / bulletAmountMax;
    }
}
