using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainScript : MonoBehaviour
{
    public static TrainScript instance { get; private set; }

    
    [SerializeField]
    [Range(0,100)]
    private float curTrainHp = 100f;
    public float maxTrainHp = 100f;

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
}
