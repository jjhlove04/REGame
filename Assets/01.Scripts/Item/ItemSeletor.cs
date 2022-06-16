using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSeletor : MonoBehaviour
{
    public ItemInfo _itemInfo;

    [SerializeField]
    private GameObject background;

    [SerializeField]
    bool count = false;

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
            ItemManager.Instance.SelecteItem(_itemInfo.item, _itemInfo.price, background, count, _itemInfo.maxCount, out curCount);

            if(curCount != 0)
            {
                if(testDatabase.resultGold >= _itemInfo.price)
                {
                    text.gameObject.SetActive(true);

                    text.text = "" + curCount + " / " + _itemInfo.maxCount;
                }
            }
        });
    }

    public void UpgradeItmeCount()
    {
        if(testDatabase.resultGold >= _itemInfo.maxCountPrice + _itemInfo.countPriceUp * (_itemInfo.maxCount - 1))
        {
            testDatabase.resultGold -= _itemInfo.maxCountPrice + _itemInfo.countPriceUp * (_itemInfo.maxCount - 1);

            _itemInfo.maxCount++;
        }

        TextPrice();
    }

    public void TextPrice()
    {
        priceText.text = ""+(_itemInfo.maxCountPrice + _itemInfo.countPriceUp * (_itemInfo.maxCount - 1));
    }
}
