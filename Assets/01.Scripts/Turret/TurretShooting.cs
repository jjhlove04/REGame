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
    private float bulletLineEffectTime = 0.03f;   
    [SerializeField]
    private ParticleSystem muzzleflash;


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

    public float curHp;
    public float maxHp;
    private bool onPlayer = false;



    private void Start()
    {
        curshootTimer = shootTimerMax;
        curHp = maxHp;
    }

    void Update()
    {
        HandleShooting(shootTimerMax, damage);
        HandleTargeting(lookForTargetTimerMax, maxDistance, out targetEnemychil);
        Shot();
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
                IDamageable target = hit.transform.GetComponent<IDamageable>();
                hitPosition = hit.point;
            }
            else
            {
                hitPosition = firePosition.position
                                + firePosition.forward * maxDistance;
            }
            curshootTimer = shootTimerMax;
            StartCoroutine(ShotEffect(hitPosition));

        }
    }

    private IEnumerator ShotEffect(Vector3 hitPosition)
    {
        muzzleflash.Play();
        bulletLineRenderer.SetPosition(1,
        bulletLineRenderer.transform.InverseTransformPoint(hitPosition));
        bulletLineRenderer.gameObject.SetActive(true);
        yield return new WaitForSeconds(bulletLineEffectTime);
        bulletLineRenderer.gameObject.SetActive(false);
        muzzleflash.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            onPlayer = true;
            UIManager.UI.destroyBtn.onClick.AddListener(() =>
            {
                CreateTurManager.Instance.DestroyTur(this.gameObject);
                Debug.Log("제발 그만해");
            });

            UIManager.UI.repairBtn.onClick.AddListener(() =>
            {
                CreateTurManager.Instance.RepairTurret(onPlayer, curHp, maxHp);
            });
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            onPlayer = false;
        }
    }
}
