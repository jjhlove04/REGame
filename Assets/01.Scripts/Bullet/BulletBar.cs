using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBar : MonoBehaviour
{
    private Transform barTrm;
    public SpriteRenderer barSprite;
    public GameObject txt;

    private void Awake()
    {
        barTrm = transform.Find("bar");
    }
    private void Start()
    {
        CameraManager.Instance.TopView += LookCameraTopView;
        CameraManager.Instance.QuarterView += LookCameraQuarterView;
    }
    private void Update()
    {
        //transform.parent.LookAt(Camera.main.transform);
        transform.parent.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
        BarSize();

        if (barTrm.localScale.x <= 0.3)
        {
            barSprite.color = Color.red;
        }
        else if (barTrm.localScale.x <= 0.6)
        {
            barSprite.color = new Color(0.8f, 0.5f, 0, 1);
            txt.SetActive(true);
        }
        else
        {
            barSprite.color = new Color(0.8f, 0.8f, 0, 1);

            txt.SetActive(false);
        }
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

    private void LookCameraTopView()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(0, -90, 0)), Time.unscaledDeltaTime);
    }

    private void LookCameraQuarterView()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(-30, -50, 0)), Time.unscaledDeltaTime);
    }
}