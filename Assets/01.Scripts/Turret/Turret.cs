using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    private float shootTimer;

    private Transform targetEnemy;

    private float lookForTargetTimer;

    [SerializeField]
    private Transform BulletSpawnPos;


    protected virtual void HandleTargeting(float lookForTargetTimerMax, float maxDistance, out Transform targetEnemy)
    {
        lookForTargetTimer -= Time.deltaTime;
        if (lookForTargetTimer < 0f)
        {
            lookForTargetTimer += lookForTargetTimerMax;
            LookForTargets(maxDistance);
        }
        transform.LookAt(this.targetEnemy);
        Quaternion quaternion = transform.rotation;
        transform.rotation = Quaternion.Euler(new Vector3(Mathf.Clamp(quaternion.x,0,9), quaternion.eulerAngles.y, quaternion.eulerAngles.z));
        targetEnemy = this.targetEnemy;
    }

    protected virtual void HandleShooting(float shootTimerMax, int damage)
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f)
        {
            shootTimer += shootTimerMax;
            if (targetEnemy != null && targetEnemy.gameObject.activeSelf != false)
            {
                Bullet.Create(BulletSpawnPos.position, targetEnemy, damage);
            }
        }
    }

    void LookForTargets(float maxDistance)
    {
        RaycastHit[] hit = Physics.SphereCastAll(transform.position, maxDistance, transform.forward);
        foreach (RaycastHit hitEnemy in hit)
        {
            if (hitEnemy.transform.CompareTag("Enemy"))
            {

                if (targetEnemy == null || targetEnemy.gameObject.activeSelf == false)
                {
                    targetEnemy = hitEnemy.collider.transform;
                }

                else
                {
                    if (Vector3.Distance(transform.position, hitEnemy.collider.transform.position) < Vector3.Distance(transform.position, targetEnemy.transform.position))
                    {
                        targetEnemy = hitEnemy.collider.transform;
                    }
                }
            }
        }
    }
}