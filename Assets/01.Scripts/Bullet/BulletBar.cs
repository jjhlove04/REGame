using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBar : MonoBehaviour
{
    public GameObject warningSprite;
    public Material halfCircle;

    private void Awake()
    {
        halfCircle = transform.GetChild(0).GetComponent<SpriteRenderer>().material;
    }
    private void Start()
    {
        CameraManager.Instance.TopView += LookCameraTopView;
        CameraManager.Instance.QuarterView += LookCameraQuarterView;
        //halfCircle.SetFloat("_Arc2", 360);
    }
    private void Update()
    {
        //transform.parent.LookAt(Camera.main.transform);
        transform.parent.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
        //BarSize();

        //if (barTrm.localScale.x <= 0.3)
        //{
        //    barSprite.color = Color.red;
        //}
        //else if (barTrm.localScale.x <= 0.6)
        //{
        //    barSprite.color = new Color(0.8f, 0.5f, 0, 1);
        //    txt.SetActive(true);
        //}
        //else
        //{
        //    barSprite.color = new Color(0.8f, 0.8f, 0, 1);

        //    txt.SetActive(false);
        //}
    }

    public void UpdateBar(int curBulletAmount, int bulletAmountMax)
    {
        //barTrm.localScale = new Vector3(GetBulletAmounetNomalized(curBulletAmount, bulletAmountMax), 1, 1);

        halfCircle.SetFloat("_Arc2", 160f * (1f - ((float)curBulletAmount / (float)bulletAmountMax)));

        if(GetBulletAmounetNomalized(curBulletAmount, bulletAmountMax) <= 0)
        {
            warningSprite.SetActive(true);
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            warningSprite.SetActive(false);
            transform.GetChild(0).gameObject.SetActive(true);
        }
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
        transform.rotation = Quaternion.LookRotation(new Vector3(0, -90, 0));//Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(0, -90, 0)), Time.unscaledDeltaTime);
        warningSprite.transform.rotation = Quaternion.LookRotation(new Vector3(0, -90, 0));//Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(0, -90, 0)), Time.unscaledDeltaTime);
    }

    private void LookCameraQuarterView()
    {
        transform.rotation = Quaternion.LookRotation(new Vector3(-30, -50, 0));//Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(-30, -50, 0)), Time.unscaledDeltaTime);
        warningSprite.transform.rotation = Quaternion.LookRotation(new Vector3(-30, -50, 0));//Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(-30, -50, 0)), Time.unscaledDeltaTime);
    }
}
