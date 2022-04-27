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
    private float maxDistance;

    [SerializeField]
    private LayerMask layerMask;

    public int maxBulletAmount;

    public int bulAmount = 10;

    [SerializeField]
    protected BulletBar bulletBar;

    public int turCount;

    public int turType;

    public int turImageCount;

    protected virtual void HandleTargeting()
    {
        if (targetEnemy != null && !targetEnemy.gameObject.GetComponent<Enemy>().isDying)
        {
            Vector3 target = this.targetEnemy.position - transform.position;
            Quaternion rot = Quaternion.LookRotation(target);



            for (int i = 0; i < weapons.Length; i++)
            {
                Transform trm = weapons[i].transform;

                trm.rotation = Quaternion.Lerp(trm.rotation, rot, Time.deltaTime * 5);
                trm.localRotation = Quaternion.Euler(new Vector3(trm.rotation.eulerAngles.x, 0, 0));
            }

            Transform neckTrm = neck.transform;

            neckTrm.rotation = Quaternion.Lerp(neckTrm.rotation, rot, Time.deltaTime * 5);
            neckTrm.rotation = Quaternion.Euler(new Vector3(0, neckTrm.rotation.eulerAngles.y, 0));

            //targetEnemy = this.targetEnemy;
        }
    }


    protected virtual void HandleShooting(float shootTimerMax, int damage)
    {
        if (targetEnemy != null && !targetEnemy.gameObject.GetComponent<Enemy>().isDying && Vector3.Distance(transform.position, targetEnemy.position) < 80)
        {
            shootTimer -= Time.deltaTime;
            if (bulAmount > 0)
            {
                if (shootTimer <= 0f)
                {
                    shootTimer = shootTimerMax;

                    for (int i = 0; i < weapons.Length; i++)
                    {

                        GameObject gameObject = ObjectPool.instacne.GetObject(bullet);
                        gameObject.transform.position = weapons[i].transform.Find("BulletPoint").position;
                        gameObject.GetComponent<ProjectileMover>().Create(targetEnemy, damage);
                        TestTurretDataBase.Instance.resultDamage += damage;
                        bulAmount--;

                        bulletBar.UpdateBar(bulAmount, maxBulletAmount);
                    }
                }
            } 
        }

        else
        {
            targetEnemy = null; 
            LookForTargets();
        }

    }

    void LookForTargets()
    {
        Collider[] hit = Physics.OverlapSphere(transform.position, maxDistance, layerMask);

        foreach (Collider hitEnemy in hit)
        {
            if (hitEnemy.CompareTag("Enemy"))
            {
                if (!hitEnemy.transform.GetComponent<Enemy>().isDying)
                {
                    if (targetEnemy != null)
                    {
                        if (Vector3.Distance(transform.position, hitEnemy.transform.position) < Vector3.Distance(transform.position, targetEnemy.transform.position))
                        {
                            targetEnemy = hitEnemy.transform;
                        }
                    }

                    else
                    {
                        targetEnemy = hitEnemy.transform;
                    }
                }
            }
        }
    }
}