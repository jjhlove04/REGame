using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainUpgrade : MonoBehaviour
{
    public float shield = 0;

    private void Start()
    {
        gameObject.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetComponent<Button>().onClick.AddListener(() => { });
        gameObject.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(() => { });
        gameObject.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(0).GetComponent<Button>().onClick.AddListener(TrainShieldUpgrade);
        gameObject.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(TrainShieldDowbgrade);
        gameObject.transform.GetChild(2).GetChild(2).GetChild(0).GetChild(0).GetComponent<Button>().onClick.AddListener(TrainCountUpgrade);
        gameObject.transform.GetChild(2).GetChild(2).GetChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(TrainCountDowngrade);

    }

    public void TrainCountUpgrade()
    {
        TestTurretDataBase.Instance.trainCount++;
    }
    public void TrainCountDowngrade()
    {
        TestTurretDataBase.Instance.trainCount--;
    }

    public void TrainShieldUpgrade()
    {
        TestTurretDataBase.Instance.trainShield += shield;
    }
    public void TrainShieldDowbgrade()
    {
        TestTurretDataBase.Instance.trainShield -= shield;
    }

    public void TrainMoney()
    {

    }
}
