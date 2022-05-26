using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainUpgrade : MonoBehaviour
{
    public float shield = 0;
    public float hp  = 0;

    private void Start()
    {
        transform.Find("BackUI/UpgradeObj/Panel/DownBtn").GetComponent<Button>().onClick.AddListener(TrainHpUpgrade);
        transform.Find("BackUI/UpgradeObj/Panel/UPBtn").GetComponent<Button>().onClick.AddListener(TrainHpDowngrade);
        transform.Find("BackUI/UpgradeObj1/Panel/DownBtn").GetComponent<Button>().onClick.AddListener(TrainShieldUpgrade);
        transform.Find("BackUI/UpgradeObj1/Panel/UPBtn").GetComponent<Button>().onClick.AddListener(TrainShieldDowbgrade);
        transform.Find("BackUI/UpgradeObj2/Panel/DownBtn").GetComponent<Button>().onClick.AddListener(TrainCountUpgrade);
        transform.Find("BackUI/UpgradeObj2/Panel/UPBtn").GetComponent<Button>().onClick.AddListener(TrainCountDowngrade);
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

    public void TrainHpUpgrade()
    {
        TestTurretDataBase.Instance.trainHp += hp;
    }
    public void TrainHpDowngrade()
    {
        TestTurretDataBase.Instance.trainHp -= hp;
    }
}
