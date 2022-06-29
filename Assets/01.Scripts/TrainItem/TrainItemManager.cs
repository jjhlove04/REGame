using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainItemManager : MonoBehaviour
{
    private static TrainItemManager instance;

    public static TrainItemManager Instance
    {
        get { return instance; }
    }

    public Button[] button;

    public Button selectButton;

    private int selectNum;

    private List<TrainItem> randomItem = new List<TrainItem>();

    [SerializeField]
    private List<TrainItem> trainItems = new List<TrainItem>();

    public int maxReCount = 0;
    public int reCount = 0;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < button.Length; i++)
        {
            button[i].onClick.AddListener(()=> { selectNum = i; });
        }

        selectButton.onClick.AddListener(SelectItem);
    }

    public void GetRandomItem()
    {
        randomItem.Clear();

        for (int i = 0; i < 3; i++)
        {
            randomItem[i] = trainItems[Random.Range(0, trainItems.Count)];
        }
    }

    private void SelectItem()
    {
        randomItem[selectNum].ItemEffect();
    }

    public void ReRoll()
    {
        if(reCount <= maxReCount)
        {
            GetRandomItem();
        }
    }
}
