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

    public Text reRollCount;

    private int selectNum;
    [SerializeField]
    private List<TrainItem> trainItemLists = new List<TrainItem>();

    private List<TrainItem> randomItem = new List<TrainItem>();

    public List<TrainItem> curTrainItems = new List<TrainItem>();

    public int maxReCount = 0;
    public int reCount = 0;

    public GameObject item;
    public GameObject itemBuff;

    InGameUII inGameUII;
    private ObjectPool objectPool;

    public GameObject train;

    public GameObject trainShield;
    public GameObject trainHeadShield;

    private bool onCupsAndBool = false;
    private int countCupsAndBool = 0;
    private float criticalPercent;

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
        inGameUII = InGameUII._instance;
        objectPool = FindObjectOfType<ObjectPool>();

        trainItemLists = TestTurretDataBase.Instance.postItemObj;

        for (int i = 0; i < trainItemLists.Count; i++)
        {
            trainItemLists[i].GetComponent<TrainItem>().curCarry = 0;
        }
        button[0].onClick.AddListener(() =>
        {
            selectNum = 0;
            SelectItem();
        });
        button[1].onClick.AddListener(() =>
        {
            selectNum = 1;
            SelectItem();
        });
        button[2].onClick.AddListener(() =>
        {
            selectNum = 2;
            SelectItem();
        });

        reCount = maxReCount;

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            for (int i = 0; i < trainItemLists.Count; i++)
            {
                trainItemLists[i].ItemEffect();

                GameObject obj = objectPool.GetObject(item);
                obj.GetComponent<Image>().sprite = trainItemLists[i].itemImage;
                obj.GetComponent<ItemBase>().itemPrefab = trainItemLists[i];
                obj.transform.parent = inGameUII.itemPanel.transform;

                if (trainItemLists[i].curCarry > 1)
                {
                    obj.SetActive(false);
                }
            }
        }
    }

    public void GetRandomItem()
    {
        randomItem.Clear();
        curTrainItems.Clear();

        for (int i = 0; i < 3; i++)
        {
            randomItem.Add(trainItemLists[Random.Range(0, trainItemLists.Count)]);
        }

        CupsAndBool(randomItem);

        for (int i = 0; i < 3; i++)
        {
            if (curTrainItems.Contains(randomItem[i]))
            {
                randomItem[i] = trainItemLists[Random.Range(0, trainItemLists.Count)];
                i = 0;
            }
            else
            {
                if (curTrainItems.Count < i + 1)
                {
                    curTrainItems.Add(randomItem[i]);

                    button[i].interactable = true;
                    button[i].transform.GetChild(0).GetComponent<Text>().text = curTrainItems[i].itemEffect;
                    button[i].transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = curTrainItems[i].itemImage;
                    button[i].transform.GetChild(2).GetChild(0).GetComponent<Text>().text = curTrainItems[i].itemStr;
                    button[i].transform.GetChild(3).GetComponent<Text>().text = curTrainItems[i].needGold.ToString();
                    button[i].transform.GetChild(3).GetComponent<Text>().color = Color.white;

                    if (GameManager.Instance.GoldAmount < curTrainItems[i].needGold && GameManager.Instance.TrainLevel != 2)
                    {
                        button[i].interactable = false;
                        button[i].transform.GetChild(3).GetComponent<Text>().color = Color.red;
                    }
                }
            }
        }
    }

    private void SelectItem()
    {
        curTrainItems[selectNum].ItemEffect();

        GameObject obj = objectPool.GetObject(item);
        obj.GetComponent<Image>().sprite = curTrainItems[selectNum].itemImage;
        obj.GetComponent<ItemBase>().itemPrefab = curTrainItems[selectNum];
        obj.transform.parent = inGameUII.itemPanel.transform;


        StartBuff(curTrainItems[selectNum].itemImage,
            curTrainItems[selectNum].bufColor,
            ParsingJson.Instnace.effectDetail[curTrainItems[selectNum].itemNum],
            curTrainItems[selectNum]);


        if (GameManager.Instance.TrainLevel != 2)
        {
            GameManager.Instance.GoldAmount -= curTrainItems[selectNum].needGold;
            InGameUII._instance.CreateOutMoney(curTrainItems[selectNum].needGold);

            if (curTrainItems[selectNum].grade == Grade.Rare)
            {
                curTrainItems[selectNum].needGold = 100 + (curTrainItems[selectNum].curCarry * 25);
            }
            else if(curTrainItems[selectNum].grade == Grade.Epic)
            {
                curTrainItems[selectNum].needGold = (int)(150 + ((curTrainItems[selectNum].curCarry * 50) + (100 * GameManager.Instance.TrainLevel * 1.05f)));
            }
            else if (curTrainItems[selectNum].grade == Grade.Unique)
            {
                curTrainItems[selectNum].needGold = (int)(200 + (curTrainItems[selectNum].curCarry * 100) + 100 * (GameManager.Instance.TrainLevel * 1.25f));
            }
        }

        if (curTrainItems[selectNum].curCarry > 1)
        {
            obj.SetActive(false);
        }

    }

    public void StartBuff(Sprite image, Color bufCol, string str, TrainItem curCar)
    {
        GameObject bufobj = objectPool.GetObject(itemBuff);
        bufobj.transform.GetChild(0).GetComponent<Image>().sprite = image;
        bufobj.GetComponent<Image>().color = bufCol;
        bufobj.transform.parent = inGameUII.itemBuffPanel.transform;
        bufobj.GetComponent<BuffItem>().bufString = str;

        if (curCar.curCarry > 1)
        {
            bufobj.SetActive(false);
        }
    }

    public void ReRoll()
    {
        if (reCount > 0 && GameManager.Instance.GoldAmount >= 30)
        {
            GetRandomItem();
            reCount--;
            reRollCount.text = reCount.ToString();
            GameManager.Instance.GoldAmount -= 30;
        }
    }

    public void SelectSkip()
    {
        inGameUII.CloseSelectPanel();
    }

    public void ExplosiveShield()
    {
        trainHeadShield.SetActive(true);

        for (int i = 0; i < train.transform.childCount; i++)
        {
            trainShield.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void OnCupsAndBool()
    {
        onCupsAndBool = true;

        countCupsAndBool++;
    }

    private void CupsAndBool(List<TrainItem> trainItems)
    {
        if (onCupsAndBool)
        {
            if (trainItems[0].grade == trainItems[1].grade && trainItems[1].grade == trainItems[2].grade)
            {
                criticalPercent += Mathf.Clamp(0.03f + 0.02f * countCupsAndBool, 0.05f, 0.17f);
            }

            else
            {
                criticalPercent = 0;
            }
        }

        TurretManager.Instance.OnCupsAndBool(criticalPercent);
    }
}