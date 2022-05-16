using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private LayerMask enemy;

    public int maxBulletAmount;

    public int bulAmount = 10;

    private int result = 8;

    [SerializeField]
    private BulletBar bulletBar;

    [SerializeField]
    private float shootTimerMax;

    [SerializeField]
    private int damage = 10;


    [HideInInspector]
    public bool detection = false;

    public int turCount;

    public int turType;

    public int turImageCount;

    public int upgradeCost;

    [SerializeField]
    private AudioSource shootAudioSource;

    [SerializeField]
    private AudioSource waitingAudioSource;

    [SerializeField]
    private AudioSource reloadAudioSource;

    public LayerMask rader;

    private bool waiting = false;

    public GameObject warning;

    private InGameUII inGameUII;

    private void OnEnable()
    {
        OffDetection();

        if(Physics.OverlapSphere(transform.position, 4, rader).Length > 0)
        {
            OnDetection();
        }

    }

    private void Start()
    {
        inGameUII = InGameUII._instance;
        bulAmount = maxBulletAmount;
        gameObject.transform.GetChild(2).gameObject.SetActive(false);
    }

    private void Update()
    {
        HandleTargeting();
        HandleShooting();
    }

    private void OnDisable()
    {
        OffDetection();
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
                        TestDatabase.Instance.resultDamage += damage;
                        bulAmount--;

                        //bulletBar.UpdateBar(bulAmount, maxBulletAmount);
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
        Collider[] hit = Physics.OverlapSphere(transform.position, maxDistance, enemy);

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

    public void OnDetection()
    {
        detection = true;
    }

    public void OffDetection()
    {
        detection = false;
    }

    public bool IsNeedReload()
    {
        if(bulAmount < 1)
        {
            return true;
        }

        return false;
    }

    public void Reload()
    {
        if (GameManager.Instance.goldAmount >= result && bulAmount != maxBulletAmount)
        {
            ReloadSound();

            GameManager.Instance.goldAmount -= result;
            bulAmount = maxBulletAmount;
        }
        else if(bulAmount == maxBulletAmount)
        {
            inGameUII.GoldWarning.GetComponent<CanvasGroup>().alpha = 1;
            inGameUII.warningIcon.color = new Color(1, 0.8f, 0, 1);
            inGameUII.warningtxt.text = "It's Already Loaded";
        }
        else
        {
            inGameUII.GoldWarning.GetComponent<CanvasGroup>().alpha = 1;
            inGameUII.warningIcon.color = new Color(1, 0.8f, 0, 1);
            inGameUII.warningtxt.text = "Not Enough Gold";

        }
        //bulletBar.UpdateBar(bulAmount, maxBulletAmount);
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

    public void OnWarning()
    {
        if (bulAmount / maxBulletAmount <= 0.3)
        {
            warning.SetActive(true);
        }
        else
        {
            warning.SetActive(false);
        }
    }

    public void DesignateTarget(Enemy enemy)
    {
        if (!enemy.isDying)
        {
            if (!enemy.IsStealth())
            {
                targetEnemy = enemy.transform;
            }

            else if (detection)
            {
                targetEnemy = enemy.transform;
            }
        }
    }

    private void OnMouseEnter()
    {
        //gameObject.transform.GetChild(2).gameObject.SetActive(true);

        Transform attackRange = transform.Find("AttackRange");

        attackRange.transform.localScale = new Vector3(maxDistance * 2, maxDistance * 2, 1);
        attackRange.transform.localPosition = new Vector3(0, 0, 0);
        attackRange.gameObject.SetActive(true);


        if (detection)
        {
            attackRange.GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 0, 30);
        }

        else
        {
            attackRange.GetComponentInChildren<SpriteRenderer>().color = new Color32(0, 0, 255, 30);
        }
    }

    private void OnMouseExit()
    {
        //gameObject.transform.GetChild(2).gameObject.SetActive(false);


        Transform attackRange = transform.Find("AttackRange");

        attackRange.gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        TurretManager.Instance.SelectTurret(this);
    }
}