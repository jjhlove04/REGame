using UnityEngine;
using UnityEngine.UI;

public class TrainUpgrade : MonoBehaviour
{
    public TrainInfo trainInfo;

    public GameObject[] upGradeBtns;

    private TestTurretDataBase testTurretDataBase;

    public CanvasGroup warning;

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

    private void Update()
    {
        if(warning.alpha >= 0)
        {
            warning.alpha = Mathf.Lerp(warning.alpha, 0, Time.deltaTime * 2);
        }
    }

    public void TrainCountUpgrade()
    {
        if(testTurretDataBase.resultGold >= trainInfo.trainCountPrice)
        {
            if (trainInfo.trainCount < 3)
            {
                ++trainInfo.trainCount;
                testTurretDataBase.resultGold -= trainInfo.trainCountPrice;
                trainInfo.trainCountPrice += trainInfo.trainCountPriceUp;
                TitleUI.UI.ShowCountUPGradeText();
            }
        }
        else
        {
            warning.transform.GetChild(0).GetComponent<Text>().text = "너는 적다 가지고 있는 돈";
            warning.alpha = 1;
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
        else
        {
            warning.transform.GetChild(0).GetComponent<Text>().text = "최소다 너의 기차 칸 수";
            warning.alpha = 1;
        }
    }

    public void TrainShieldUpgrade()
    {
        if (testTurretDataBase.resultGold >= trainInfo.shieldPrice)
        {
            testTurretDataBase.resultGold -= trainInfo.shieldPrice;
            trainInfo.shieldPrice += trainInfo.shieldPriceUp;
            trainInfo.trainMaxShield += trainInfo.shieldUpgrade;
            TitleUI.UI.ShowShieldUpGradeText();
        }
        else
        {
            warning.transform.GetChild(0).GetComponent<Text>().text = "너는 적다 가지고 있는 돈";
            warning.alpha = 1;
        }
    }
    public void TrainShieldDowngrade()
    {
        if (trainInfo.trainMaxShield > 50)
        {
            trainInfo.shieldPrice -= trainInfo.shieldPriceUp;
            testTurretDataBase.resultGold += trainInfo.shieldPrice;
            trainInfo.trainMaxShield -= trainInfo.shieldUpgrade;
            TitleUI.UI.ShowShieldUpGradeText();
        }
        else
        {
            warning.transform.GetChild(0).GetComponent<Text>().text = "최소다 너의 방어막";
            warning.alpha = 1;
        }
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
        else
        {
            warning.transform.GetChild(0).GetComponent<Text>().text = "너는 적다 가지고 있는 돈";
            warning.alpha = 1;
        }
    }
    public void TrainHpDowngrade()
    {
        if (trainInfo.trainMaxHp > 50)
        {
            trainInfo.hpPrice -= trainInfo.hpPriceUp;
            testTurretDataBase.resultGold += trainInfo.hpPrice;
            trainInfo.trainMaxHp -= trainInfo.hpUpgrade;
            TitleUI.UI.ShowHpUpgradeText();
        }
        else
        {
            warning.transform.GetChild(0).GetComponent<Text>().text = "최소다 너의 체력";
            warning.alpha = 1;
        }
    }
}