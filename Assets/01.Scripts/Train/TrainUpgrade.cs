using UnityEngine;
using UnityEngine.UI;

public class TrainUpgrade : MonoBehaviour
{
    public TrainInfo trainInfo;

    public GameObject[] upGradeBtns;

    private TestTurretDataBase testTurretDataBase;

    private void Start()
    {
        testTurretDataBase = TestTurretDataBase.Instance;

        upGradeBtns[0].GetComponent<Button>().onClick.AddListener(TrainHpDowngrade);
        upGradeBtns[1].GetComponent<Button>().onClick.AddListener( TrainHpUpgrade);
        upGradeBtns[2].GetComponent<Button>().onClick.AddListener(TrainShieldDowngrade);
        upGradeBtns[3].GetComponent<Button>().onClick.AddListener( TrainShieldUpgrade);
        upGradeBtns[4].GetComponent<Button>().onClick.AddListener(TrainCountDowngrade);
        upGradeBtns[5].GetComponent<Button>().onClick.AddListener( TrainCountUpgrade);
    }

    public void TrainCountUpgrade()
    {
        if(testTurretDataBase.resultGold >= trainInfo.trainCountPrice)
        {
            if (trainInfo.trainCount < 3)
            {
                ++trainInfo.trainCount;
                TitleUI.UI.ShowCountUPGradeText();
                testTurretDataBase.resultGold -= trainInfo.trainCountPrice;
                trainInfo.trainCountPrice += trainInfo.trainCountPriceUp;
            }
        }
    }
    public void TrainCountDowngrade()
    {
        if (trainInfo.trainCount > 1)
        {
            trainInfo.trainCountPrice -= trainInfo.trainCountPriceUp;
            testTurretDataBase.resultGold += trainInfo.trainCountPrice;
            --trainInfo.trainCount;
            TitleUI.UI.ShowCountUPGradeText();
        }
    }

    public void TrainShieldUpgrade()
    {
        if (testTurretDataBase.resultGold >= trainInfo.shieldPrice)
        {
            testTurretDataBase.resultGold -= trainInfo.shieldPrice;
            trainInfo.shieldPrice += trainInfo.shieldPriceUp;
            TitleUI.UI.ShowShieldUpGradeText();
            trainInfo.trainMaxShield += trainInfo.shieldUpgrade;
        }
    }
    public void TrainShieldDowngrade()
    {
        trainInfo.shieldPrice -= trainInfo.shieldPriceUp;
        testTurretDataBase.resultGold += trainInfo.shieldPrice;
        TitleUI.UI.ShowShieldUpGradeText();
        trainInfo.trainMaxShield -= trainInfo.shieldUpgrade;
    }

    public void TrainHpUpgrade()
    {
        if (testTurretDataBase.resultGold >= trainInfo.hpPrice)
        {
            testTurretDataBase.resultGold -= trainInfo.hpPrice;
            trainInfo.hpPrice += trainInfo.hpPriceUp;
            trainInfo.trainMaxHp += trainInfo.hpUpgrade;
            TitleUI.UI.ShowHpUpgradeText();
        }
    }
    public void TrainHpDowngrade()
    {
        trainInfo.hpPrice -= trainInfo.hpPriceUp;
        testTurretDataBase.resultGold += trainInfo.hpPrice;
        trainInfo.trainMaxHp -= trainInfo.hpUpgrade;
        TitleUI.UI.ShowHpUpgradeText();
    }
}