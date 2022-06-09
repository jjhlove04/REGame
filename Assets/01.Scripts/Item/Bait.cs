using System.Collections.Generic;
using UnityEngine;

public class Bait : MonoBehaviour
{
    // Start is called before the first frame update
    private int aggroArea;

    private float hp;

    Dictionary<EnemyType, EnemyData> dicEnemydata = new Dictionary<EnemyType, EnemyData>();

    BaitHealthSystem baitHealthSystem;

    private int explotionArea = 10;

    public bool isDie;

    private bool timeOut = false;

    private InGameUII inGameUII;

    // Update is called once per frame

    private void OnEnable()
    {
        isDie = false;

        timeOut = false;

        transform.Find("Particle").gameObject.SetActive(false);

        CancelInvoke("Move");

        Invoke("Move", 40);
    }

    private void Awake()
    {
        EnemyDataInit();
    }

    private void Start()
    {
        baitHealthSystem = GetComponent<BaitHealthSystem>();

        baitHealthSystem.maxHp = (int)hp;

        inGameUII = InGameUII._instance;
    }

    void Update()
    {
        if (!isDie)
        {
            Aggro();

            if (timeOut)
            {
                transform.position += new Vector3(0,10,10) * Time.deltaTime;
            }
        }

        else
        {
            transform.position += Vector3.back * 20 * Time.deltaTime;
        }

        transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.LookRotation(new Vector3(-100, 1, 0)),Time.deltaTime);
    }

    private void Aggro()
    {
        Collider[] enemys = Physics.OverlapSphere(transform.position, aggroArea, LayerMask.GetMask("Enemy"));

        foreach (var enemy in enemys)
        {
            enemy.GetComponent<Enemy>()?.Aggro(transform);
        }
    }

    public void Spawn(int aggroArea, int hp)
    {
        this.aggroArea = aggroArea;
        this.hp = hp;
    }

    private void EnemyDataInit()
    {
        dicEnemydata.Add(EnemyType.Fire, Resources.Load<EnemyData>("Fire"));
        dicEnemydata.Add(EnemyType.Drill, Resources.Load<EnemyData>("Drill"));
        dicEnemydata.Add(EnemyType.Guardian, Resources.Load<EnemyData>("Guardian"));
        dicEnemydata.Add(EnemyType.HumanoidRig, Resources.Load<EnemyData>("HumanoidRig"));
        dicEnemydata.Add(EnemyType.Roket, Resources.Load<EnemyData>("Roket"));
    }

    public void Damage(float damage)
    {
        hp -= damage;

        baitHealthSystem.Damage(damage);
        /*for (int i = 0; i < trainhit.Length; i++)
        {
            trainhit[i].Hit();
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isDie)
        {
            if (other.CompareTag("RoketBullet"))
            {
                Damage(dicEnemydata[EnemyType.Roket].GetDamage());
            }

            else if (other.CompareTag("HumanoidRigBullet"))
            {
                Damage(dicEnemydata[EnemyType.HumanoidRig].GetDamage() * Time.deltaTime);
            }

            else
            {
                return;
            }
        }      
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isDie)
        {
            if (other.CompareTag("FireBullet"))
            {
                Damage(dicEnemydata[EnemyType.Fire].GetDamage() * Time.deltaTime);
            }

            else if (other.CompareTag("GuardianBullet"))
            {
                Damage(dicEnemydata[EnemyType.Guardian].GetDamage() * Time.deltaTime);
            }

            else if (other.CompareTag("DrillBullet"))
            {
                Damage(dicEnemydata[EnemyType.Drill].GetDamage() * Time.deltaTime);
            }
        }
    }

    private void Move()
    {
        timeOut = true;
    }

    public void Dying()
    {
        Collider[] enemys = Physics.OverlapSphere(transform.position, explotionArea, LayerMask.GetMask("Enemy"));

        isDie = true;

        foreach (var enemy in enemys)
        {
            enemy.GetComponent<HealthSystem>().Damage(10);
        }

        transform.Find("Particle").gameObject.SetActive(true);

        Invoke("Die", 5);
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }

    private void OnMouseEnter()
    {
        //inGameUII.DrawArea(new Vector3(aggroArea*1.5f, aggroArea*1.5f, 1), transform.position + new Vector3(0,5,0));
    }

    private void OnMouseOver()
    {
        //inGameUII.Drawing(transform.position + new Vector3(0, 5, 0));
    }

    private void OnMouseExit()
    {
        //inGameUII.ClearArea();
    }
}