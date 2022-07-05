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

    public Image itemPanel;

    private int selectNum;

    [SerializeField]
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
        button[0].onClick.AddListener(() =>
        {
            selectNum = 0;
            Debug.Log(0 + " : " + selectNum);
            SelectItem();
        });
        button[1].onClick.AddListener(() =>
        {
            selectNum = 1;
            Debug.Log(1 + " : " + selectNum);
            SelectItem();
        });
        button[2].onClick.AddListener(() =>
        {
            selectNum = 2;
            Debug.Log(2 + " : " + selectNum);
            SelectItem();
        });

        reCount = maxReCount;

    }

    public void GetRandomItem()
    {
        randomItem.Clear();

        for (int i = 0; i < 3; i++)
        {
            randomItem.Add(trainItems[Random.Range(0, trainItems.Count)]);
            button[i].transform.GetChild(0).GetComponent<Text>().text = randomItem[i].ToString();
        }
    }

    private void SelectItem()
    {
        randomItem[selectNum].ItemEffect();
    }

    public void ReRoll()
    {
        if (reCount > 0)
        {
            GetRandomItem();
            reCount--;
        }
    }
}
