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
        if (maxTrainHp - roomHp + smokeHp >= curTrainHp)
        {
            DestroyTrain();
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
        UIManager.UI.TakeDamageHpBar();
    }

    IEnumerator Destroy()
    {
        if (maxTrainHp - roomHp >= curTrainHp)
        {
            roomHp += initRoomHp;
            if (TrainManager.instance.curTrainCount > 0)
            {
                TrainManager.instance.curTrainCount--;
                destroy = true;
                yield return new WaitForSeconds(0.5f);
                TrainManager.instance.Explotion();
                yield return new WaitForSeconds(0.75f);
                TrainManager.instance.RemoveList();
                destroy = false;
            }

            if (TrainManager.instance.curTrainCount % 2 == 1 && TrainManager.instance.curTrainCount == 1)
            {
                roomHp += 1;
            }
        }
    }
}