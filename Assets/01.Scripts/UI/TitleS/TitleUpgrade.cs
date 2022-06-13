using UnityEngine;
using UnityEngine.UI;

public class TitleUpgrade : MonoBehaviour
{
    public enum UpgradeType
    {
        Tower,
        Train
    };

    public UpgradeType type;

    public int price;

    private void Start()
    {
        gameObject.TryGetComponent(out Button btn);

        if (type == UpgradeType.Tower)
        {
            btn.onClick.AddListener(() =>
            {
                if(TestTurretDataBase.Instance.resultGold > price)
                {
                    TestTurretDataBase.Instance.resultGold -= price;
                    TowerUpgrade();
                }
            });
        }

        else if (type == UpgradeType.Train)
        {
            btn.onClick.AddListener(() =>
            {
                if (TestTurretDataBase.Instance.resultGold > price)
                {
                    TestTurretDataBase.Instance.resultGold -= price;
                    TrainUpgrade();
                }
            });
        }

    }

    private void TowerUpgrade()
    {
        //타워 업그레이드 관련
    }

    private void TrainUpgrade()
    {
        //기차 업그레이드 관련
    }
}
