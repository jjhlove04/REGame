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
    private float maxDistance;

    [SerializeField]
    private float lookForTargetTimerMax = 0.2f;

    [SerializeField]
    private int damage = 10;

    private Transform targetEnemychil;

    //[SerializeField]
    //private bool onPlayer = false;

    public int upgradeCost;

    public int maxBulletAmount;

    private void Start()
    {
        //curshootTimer = shootTimerMax;
        bulAmount = maxBulletAmount;
    }

    void Update()
    {
        HandleShooting(shootTimerMax, damage);
        HandleTargeting(maxDistance);
        //Shot();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Reload();
        }
    }

    protected override void HandleTargeting(float maxDistance)
    {
        base.HandleTargeting(maxDistance);
    }
    protected override void HandleShooting(float shootTimerMax, int damage)
    {
        base.HandleShooting(shootTimerMax, damage);

        
    }

    public void Shot()
    {

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
        int result = maxBulletAmount - bulAmount;
        result *= 2;
        GameManager.Instance.goldAmount -= result;
        bulAmount = maxBulletAmount;
    }
}
