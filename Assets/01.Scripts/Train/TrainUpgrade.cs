using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainUpgrade : MonoBehaviour
{
    public float shield = 0;
    public float hp  = 0;

    public GameObject[] upGradeBtns;
    private void Start()
    {
        upGradeBtns[0].GetComponent<Button>().onClick.AddListener(TrainHpDowngrade);
        upGradeBtns[1].GetComponent<Button>().onClick.AddListener( TrainHpUpgrade);
        upGradeBtns[2].GetComponent<Button>().onClick.AddListener(TrainShieldDowngrade);
        upGradeBtns[3].GetComponent<Button>().onClick.AddListener( TrainShieldUpgrade);
        upGradeBtns[4].GetComponent<Button>().onClick.AddListener(TrainCountDowngrade);
        upGradeBtns[5].GetComponent<Button>().onClick.AddListener( TrainCountUpgrade);
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
