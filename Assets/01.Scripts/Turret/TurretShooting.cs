using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurretShooting : Turret
{
    //[SerializeField]
    //private Transform firePosition;
    //[SerializeField]
    //private float bulletLineEffectTime = 0.03f;


    [SerializeField]
    private float shootTimerMax;

    //private float curshootTimer;

    [SerializeField]
    private float lookForTargetTimerMax = 0.2f;

    [SerializeField]
    private int damage = 10;

    private int result = 8;

    private Transform targetEnemychil;

    //[SerializeField]
    //private bool onPlayer = false;

    public int upgradeCost;

    private void Start()
    {
        //curshootTimer = shootTimerMax;
        bulAmount = maxBulletAmount;
    }

    void Update()
    {
        HandleShooting(shootTimerMax, damage);
        HandleTargeting();
    }

    protected override void HandleTargeting()
    {
        base.HandleTargeting();
    }
    protected override void HandleShooting(float shootTimerMax, int damage)
    {
        base.HandleShooting(shootTimerMax, damage);
    }

    /*private void Shot()
    {
        if (curshootTimer >= 0)
        {
            curshootTimer -= Time.deltaTime;
        }

        if (targetEnemychil != null && curshootTimer <= 0)
        {
            RaycastHit hit;
            Vector3 hitPosition = Vector3.zero;
            if (Physics.Raycast(
                    firePosition.position,
                    firePosition.forward, out hit, maxDistance))
            {
                hitPosition = hit.point;
            }
            else
            {
                hitPosition = firePosition.position
                                + firePosition.forward * maxDistance;
            }
            curshootTimer = shootTimerMax;
            curBulletAmount--;
            //StartCoroutine(ShotEffect(hitPosition));

        }
    }*/

    //private IEnumerator ShotEffect(Vector3 hitPosition)
    //{
    //    muzzleflash.Play();
    //    bulletLineRenderer.SetPosition(1,bulletLineRenderer.transform.InverseTransformPoint(hitPosition));
    //    bulletLineRenderer.gameObject.SetActive(true);
    //    yield return new WaitForSeconds(bulletLineEffectTime);
    //    bulletLineRenderer.gameObject.SetActive(false);
    //    muzzleflash.Stop();
    //}

    public void Reload()
    {
        if (GameManager.Instance.goldAmount >= result && bulAmount != maxBulletAmount)
        {
            GameManager.Instance.goldAmount -= result;
            bulAmount = maxBulletAmount;
        }
        else
        {
            InGameUI._instance.warningTxt.color = new Color(1, 0.8f, 0, 1);
            InGameUI._instance.warningTxt.text = "Not Enough Gold";
        }
        bulletBar.UpdateBar(bulAmount, maxBulletAmount);
    }
}
