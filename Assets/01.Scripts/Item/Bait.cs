using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bait : MonoBehaviour
{
    // Start is called before the first frame update
    private int aggroArea;

    private float hp;

    Dictionary<EnemyType, EnemyData> dicEnemydata = new Dictionary<EnemyType, EnemyData>();

    BaitHealthSystem baitHealthSystem;

    // Update is called once per frame

    private void Awake()
    {
        EnemyDataInit();
    }

    private void Start()
    {
        baitHealthSystem = GetComponent<BaitHealthSystem>();

        baitHealthSystem.maxHp = (int)hp;
    }

    void Update()
    {
        Invoke("Move", 40);

        Aggro();

        transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.LookRotation(new Vector3(-100, 1, 0)),Time.deltaTime);
    }

    private void Aggro()
    {
        Collider[] enemys = Physics.OverlapSphere(transform.position, aggroArea, LayerMask.GetMask("Enemy"));

        foreach (var enemy in enemys)
        {
            enemy.GetComponent<Enemy>().Aggro(transform);
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

    private void OnTriggerStay(Collider other)
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

        else
        {
            return;
        }
    }

    private void Move()
    {
        transform.position += Vector3.forward * Time.deltaTime * 10;

        if(transform.position.z > 100)
        {
            Destroy(gameObject);
        }
    }
}