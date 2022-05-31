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
        transform.Find("BackUI/hpUP/Panel/DownBtn").GetComponent<Button>().onClick.AddListener(TrainHpDowngrade);
        transform.Find("BackUI/hpUP/Panel/UPBtn").GetComponent<Button>().onClick.AddListener( TrainHpUpgrade);
        transform.Find("BackUI/ShUP/Panel/DownBtn").GetComponent<Button>().onClick.AddListener(TrainShieldDowngrade);
        transform.Find("BackUI/ShUP/Panel/UPBtn").GetComponent<Button>().onClick.AddListener( TrainShieldUpgrade);
        transform.Find("BackUI/countUP/Panel/DownBtn").GetComponent<Button>().onClick.AddListener(TrainCountDowngrade);
        transform.Find("BackUI/countUP/Panel/UPBtn").GetComponent<Button>().onClick.AddListener( TrainCountUpgrade);
    }

    public void TrainCountUpgrade()
    {
        if (TestTurretDataBase.Instance.trainCount < 3)
        {
            ++TestTurretDataBase.Instance.trainCount;
            TitleUI.UI.ShowCountUPGradeText();

        }
    }
    public void TrainCountDowngrade()
    {
        if (TestTurretDataBase.Instance.trainCount > 1)
        {
            --TestTurretDataBase.Instance.trainCount;
            TitleUI.UI.ShowCountUPGradeText();
        }
    }

    public void TrainShieldUpgrade()
    {
        TitleUI.UI.ShowShieldUpGradeText();
        TestTurretDataBase.Instance.trainShield += shield;
    }
    public void TrainShieldDowngrade()
    {
        TitleUI.UI.ShowShieldUpGradeText();
        TestTurretDataBase.Instance.trainShield -= shield;
    }

    public void TrainHpUpgrade()
    {
        TestTurretDataBase.Instance.trainHp += hp;
        TitleUI.UI.ShowHpUpgradeText();
    }
    public void TrainHpDowngrade()
    {
        TestTurretDataBase.Instance.trainHp -= hp;
        TitleUI.UI.ShowHpUpgradeText();
    }
}
