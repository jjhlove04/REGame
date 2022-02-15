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
        initRoomHp = roomHp;
    }

    private void Update()
    {
        DestroyTrain();
    }

    public void DestroyTrain()
    {
        if (maxTrainHp - roomHp >= curTrainHp)
        {
            roomHp += initRoomHp;
            if (TrainManager.instance.curTrainCount > 0)
            {
                TrainManager.instance.curTrainCount--;
                TrainManager.instance.RemoveList();
            }

            if (TrainManager.instance.curTrainCount % 2 == 1 && TrainManager.instance.curTrainCount == 1)
            {
                roomHp += 1;
            }


            TrainManager.instance.CreateTrainPrefab();
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
}
