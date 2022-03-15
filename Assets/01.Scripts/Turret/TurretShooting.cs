using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurretShooting : Turret
{
    [SerializeField]
    private LineRenderer bulletLineRenderer;
    [SerializeField]
    private Transform firePosition;
    [SerializeField]
    private float bulletLineEffectTime = 1f;   
    [SerializeField]
    private GameObject effect = null;


    [SerializeField]
    private float shootTimerMax;

    private float curshootTimer;

    [SerializeField]
    private float maxDistance;

    [SerializeField]
    private float lookForTargetTimerMax = 0.2f;

    [SerializeField]
    private int damage = 10;

    private Transform targetEnemychil;

    public int maxBulletAmount;
    public int bulletAmount;

    public int upgradeCost;



    private void Start()
    {
        curshootTimer = shootTimerMax;
        bulletAmount = maxBulletAmount;
    }

    void Update()
    {
        if (bulletAmount > 0)
        {
            HandleShooting(shootTimerMax, damage);
            HandleTargeting(lookForTargetTimerMax, maxDistance, out targetEnemychil);
            Shot();
        }

    }

    protected override void HandleTargeting(float lookForTargetTimerMax, float maxDistance, out Transform targetEnemy)
    {
        base.HandleTargeting(lookForTargetTimerMax, maxDistance, out targetEnemy);
    }
    protected override void HandleShooting(float shootTimerMax, int damage)
    {
        base.HandleShooting(shootTimerMax, damage);
    }

    private void Shot()
    {
        if(curshootTimer >= 0)
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
            bulletAmount -= 1;

            StartCoroutine(ShotEffect(hitPosition));

        }
    }

    public void Reload()
    {
        bulletAmount = maxBulletAmount;
    }

    private IEnumerator ShotEffect(Vector3 hitPosition)
    {
        //effect?.SetActive(true);
        yield return new WaitForSeconds(1);
        bulletLineRenderer.SetPosition(1,
        bulletLineRenderer.transform.InverseTransformPoint(hitPosition));
        bulletLineRenderer.gameObject.SetActive(true);
        yield return new WaitForSeconds(bulletLineEffectTime);
        bulletLineRenderer.gameObject.SetActive(false);
        //effect?.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Train"))
        {
            CreateTurManager.Instance.DestroyTur(this.gameObject);
        }
    }
}
