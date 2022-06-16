using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSeletor : MonoBehaviour
{
    [SerializeField]
    private int price = 10;

    [SerializeField]
    private GameObject item;

    [SerializeField]
    private GameObject background;

    [SerializeField]
    bool count = false;

    [SerializeField]
    private int maxCountPrice;

    [SerializeField]
    private int countPriceUp;

    [SerializeField]
    static int maxCount = 1;

    [SerializeField]
    private Button upgradeBtn;
    [SerializeField]
    private Text priceText;

    int curCount;

    [SerializeField]
    TextMeshProUGUI text;

    TestDatabase testDatabase;


    void Start()
    {
        testDatabase = TestDatabase.Instance;

        if (upgradeBtn != null)
        {

            TextPrice();

            upgradeBtn.onClick.AddListener(() =>
            {
                UpgradeItmeCount();
            });
        }

        GetComponent<Button>().onClick.AddListener(() => 
        {
            ItemManager.Instance.SelecteItem(item, price, background, count, maxCount, out curCount);

            if(curCount != 0)
            {
                if(testDatabase.resultGold >= price)
                {
                    text.gameObject.SetActive(true);

                    text.text = "" + curCount + " / " + maxCount;
                }
            }
        });
    }

    public void UpgradeItmeCount()
    {
        if(testDatabase.resultGold >= maxCountPrice + countPriceUp * (maxCount - 1))
        {
            testDatabase.resultGold -= maxCountPrice + countPriceUp * (maxCount-1);

            maxCount++;
        }

        TextPrice();
    }

    public void TextPrice()
    {
        priceText.text = ""+(maxCountPrice + countPriceUp * (maxCount - 1));
    }
}
