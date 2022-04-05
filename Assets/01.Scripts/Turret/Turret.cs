using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    private float shootTimer;

    private Transform targetEnemy;

    [SerializeField]
    private GameObject bullet = null;

    [SerializeField]
    private GameObject[] weapons;

    [SerializeField]
    private GameObject neck;

    [SerializeField]
    private LayerMask layerMask;


    protected virtual void HandleTargeting(float maxDistance)
    {
        if (targetEnemy != null && !targetEnemy.gameObject.GetComponent<Enemy>().isDying)
        {
            Vector3 target = this.targetEnemy.position - transform.position;
            Quaternion rot = Quaternion.LookRotation(target);
            for (int i = 0; i < weapons.Length; i++)
            {
                weapons[i].transform.rotation = Quaternion.Lerp(weapons[i].transform.rotation, rot, Time.deltaTime * 5);
                weapons[i].transform.localRotation = Quaternion.Euler(new Vector3(weapons[i].transform.rotation.eulerAngles.x, 0, 0));
            }

            print(neck);
            neck.transform.rotation = Quaternion.Lerp(neck.transform.rotation, rot, Time.deltaTime * 5);
            neck.transform.rotation = Quaternion.Euler(new Vector3(0, neck.transform.rotation.eulerAngles.y, 0));

            //targetEnemy = this.targetEnemy;
        }

        else
        {
            LookForTargets(maxDistance);
        }

        /*else
        {
            targetEnemy = null;
        }*/
    }


    protected virtual void HandleShooting(float shootTimerMax, int damage)
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f)
        {
            shootTimer += shootTimerMax;
            if (targetEnemy != null && !targetEnemy.gameObject.GetComponent<Enemy>().isDying)
            {
                for (int i = 0; i < weapons.Length; i++)
                {
                    GameObject gameObject = ObjectPool.instacne.GetObject(bullet);
                    gameObject.transform.position = weapons[i].transform.Find("BulletPoint").position;
                    gameObject.GetComponent<ProjectileMover>().Create(targetEnemy, damage);
                }
            }
        }
    }

    void LookForTargets(float maxDistance)
    {
        RaycastHit[] hit = Physics.SphereCastAll(transform.position, maxDistance, transform.forward, 100, layerMask);

        foreach (RaycastHit hitEnemy in hit)
        {
            if (hitEnemy.transform.CompareTag("Enemy"))
            {
                if (hitEnemy.transform == null|| hitEnemy.transform.GetComponent<Enemy>().isDying)
                {
                    targetEnemy = null;
                }

                else
                {
                    if (targetEnemy != null)
                    {
                        //Debug.Log(targetEnemy.gameObject.layer+targetEnemy.transform.name);
                        if (targetEnemy.transform.GetComponent<Enemy>().isDying)
                        {
                            targetEnemy = null;
                        }

                        else if (Vector3.Distance(transform.position, hitEnemy.collider.transform.position) < Vector3.Distance(transform.position, targetEnemy.transform.position))
                        {
                            targetEnemy = hitEnemy.collider.transform;
                        }
                    }

                    else
                    {
                        targetEnemy = hitEnemy.collider.transform;
                    }

                }
            }
        }
    }
}