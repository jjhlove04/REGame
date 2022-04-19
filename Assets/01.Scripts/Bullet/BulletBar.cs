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
        //transform.parent.LookAt(Camera.main.transform);
        transform.parent.rotation = Quaternion.Euler(new Vector3(90, 0, 90));
        BarSize();
    }

    public void UpdateBar(int curBulletAmount, int bulletAmountMax)
    {
        barTrm.localScale = new Vector3(GetBulletAmounetNomalized(curBulletAmount, bulletAmountMax), 1, 1);
    }

    private float GetBulletAmounetNomalized(int curBulletAmount, int bulletAmountMax)
    {
        return (float)curBulletAmount / bulletAmountMax;
    }

    private void BarSize()
    {
        float size = Camera.main.transform.position.y / 30;
        if (size > 1)
        {
            transform.parent.localScale = new Vector3(size, size, size);
        }
    }
}
