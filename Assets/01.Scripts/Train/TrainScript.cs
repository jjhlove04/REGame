using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainScript : MonoBehaviour
{
    public static TrainScript instance { get; private set; }


    public float curTrainHp = 50000; //0���Ϸ� ����߸��� ����!
    public float maxTrainHp = 50000;

    private float initRoomHp;
    private float roomHp;

    private float smokeHp;

    private bool destroy = false;

    [SerializeField]
    private float fireDamage;

    [SerializeField]
    private float drillDamage;

    [SerializeField]
    private float guardianDamage;

    [SerializeField]
    private float humanoidDamage;

    [SerializeField]
    private float roketDamage;

    private TrainHit[] trainhit;

    private void Awake()
    {
        instance = this;

        curTrainHp = maxTrainHp;

        TrainManager.instance.curTrainCount = TrainManager.instance.maxTrainCount;
        TrainManager.instance.CreateTrainPrefab();
    }

    private void OnEnable()
    {
        roomHp = maxTrainHp / TrainManager.instance.curTrainCount;
        smokeHp = roomHp / 10;
        initRoomHp = roomHp;
    }

    private void Start()
    {
        trainhit = GetComponentsInChildren<TrainHit>();
    }

    private void Update()
    {
        SmokeTrain();
        if (destroy)
        {
            TrainManager.instance.KeepOffTrain();
        }
    }

    public void DestroyTrain()
    {
        StartCoroutine(Destroy());
    }

    public void SmokeTrain()
    {
        if (maxTrainHp /10 >= curTrainHp)
        {
            TrainManager.instance.OnSmoke();
        }
    }

    public void FixHP()
    {
        curTrainHp = maxTrainHp;
    }

    public void Damage(float damage)
    {
        curTrainHp -= damage;
        testScriptts.Instance.TakeDamageHpBar();

        if (curTrainHp <= 0)
        {
            DestroyTrain();
        }
   }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FireBullet"))
        {
            Damage(fireDamage * Time.deltaTime);
        }

        else if (other.CompareTag("RoketBullet"))
        {
            Damage(roketDamage);
        }

        else if (other.CompareTag("HumanoidRigBullet"))
        {
            Damage(humanoidDamage * Time.deltaTime);
        }

        else
        {
            return;
        }

        for (int i = 0; i < trainhit.Length; i++)
        {
            trainhit[i].Hit();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("GuardianBullet"))
        {
            Damage(guardianDamage * Time.deltaTime);
        }

        else if (other.CompareTag("DrillBullet"))
        {
            Damage(drillDamage * Time.deltaTime);
        }

        else
        {
            return;
        }

        for (int i = 0; i < trainhit.Length; i++)
        {
            trainhit[i].Hit();
        }
    }*/

    IEnumerator Destroy()
    {
        roomHp += initRoomHp;
        if (TrainManager.instance.curTrainCount > 0)
        {
            TrainManager.instance.curTrainCount--;
            destroy = true;
            yield return new WaitForSeconds(0.5f);
            TrainManager.instance.Explotion();
            yield return new WaitForSeconds(0.75f);
            GameManager.Instance.state = GameManager.State.End;
            destroy = false;
        }
    }
}