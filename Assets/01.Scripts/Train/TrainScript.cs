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

    private void Awake()
    {
        instance = this;

        curTrainHp = maxTrainHp;
    }

    private void OnEnable()
    {
        roomHp = maxTrainHp / TrainManager.instance.curTrainCount;
        smokeHp = roomHp / 10;
        initRoomHp = roomHp;
    }

    private void Start()
    {
        TrainManager.instance.curTrainCount = TrainManager.instance.maxTrainCount;
        TrainManager.instance.CreateTrainPrefab();
    }

    private void Update()
    {
        SmokeTrain();
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

    public void SmokeTrain()
    {
        if(maxTrainHp - roomHp + smokeHp >= curTrainHp)
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
        Hit();
    }

    private void Hit()
    {
        /*foreach (GameObject item in TrainManager.instance.trainContainer)
        {
            item.GetComponent<TrainHit>()?.Hit();
        }
        transform.GetChild(0).GetComponent<TrainHit>()?.Hit();*/
    }
}
