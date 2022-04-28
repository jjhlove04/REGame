﻿using System.Collections;
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

    private int result = 8;

    [SerializeField]
    private BulletBar bulletBar;

    [SerializeField]
    private float shootTimerMax;

    [SerializeField]
    private int damage = 10;

    public int turCount;

    public int turType;

    public int turImageCount;

    public int upgradeCost;

    [HideInInspector]
    public bool detection = false;
    
    [SerializeField]
    private AudioSource shootAudioSource;

    [SerializeField]
    private AudioSource waitingAudioSource;

    [SerializeField]
    private AudioSource reloadAudioSource;


    private bool waiting = false;


    private void Start()
    {
        bulAmount = maxBulletAmount;
    }

    private void Update()
    {
        HandleTargeting();
        HandleShooting();
    }

    private void HandleTargeting()
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


    private void HandleShooting()
    {
        if (targetEnemy != null && !targetEnemy.gameObject.GetComponent<Enemy>().isDying && Vector3.Distance(transform.position, targetEnemy.position) < 80)
        {
            shootTimer -= Time.deltaTime;
            if (bulAmount > 0)
            {
                if (shootTimer <= 0f)
                {
                    ShootSound();

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

                else
                {
                    WaitingTimeSound();
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
            Enemy enemy = hitEnemy.GetComponent<Enemy>();
            if (hitEnemy.CompareTag("Enemy"))
            {
                if (!enemy.isDying)
                {
                    if (!enemy.IsStealth())
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

                    else if(detection)
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

    public void Reload()
    {
        if (GameManager.Instance.goldAmount >= result && bulAmount != maxBulletAmount)
        {
            ReloadSound();

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

    private void ShootSound()
    {
        if(shootAudioSource.clip != null)
        {
            shootAudioSource.Play();

            shootAudioSource.pitch = (shootAudioSource.clip.length * (1 / shootAudioSource.clip.length)) * Time.timeScale;

            waiting = true;
        }
    }

    private void WaitingTimeSound()
    {
        if(waitingAudioSource.clip != null)
        {
            waitingAudioSource.pitch = waitingAudioSource.clip.length / shootTimerMax * Time.timeScale;

            if (waiting)
            {
                waitingAudioSource.Play();

                waiting = false;
            }
        }
    }

    private void ReloadSound()
    {
        if(reloadAudioSource.clip != null)
        {
            reloadAudioSource.pitch = reloadAudioSource.clip.length / shootTimerMax;

            reloadAudioSource.Play();
        }
    }
}