using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemCount
{
    public ItemCount(int count, bool isCount)
    {
        this.count = count;
        this.isCount = isCount;
    }

    public int count;
    public bool isCount;
}

public class ItemManager : MonoBehaviour
{
    private static ItemManager instance;

    public static ItemManager Instance
    {
        get { return instance; }
    }

    public List<GameObject> items = new List<GameObject> ();

    public List<GameObject> gameItems = new List<GameObject> ();

    private Dictionary<GameObject, ItemCount> itemIsCount = new Dictionary<GameObject, ItemCount>();

    private TestTurretDataBase testDatabase;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);

        SceneManager.sceneLoaded += OnSceneLoeded;
    }

    private void Start()
    {
        testDatabase = TestTurretDataBase.Instance;
    }

    public void SelecteItem(GameObject gameitem, int price, GameObject background, bool count, int maxCount, out int curCount)
    {
        foreach (var item in items)
        {
            if (item == gameitem)
            {
                if (count)
                {
                    if (testDatabase.resultGold < price || maxCount <= itemIsCount[item].count)
                    {
                        curCount = itemIsCount[item].count;

                        return;
                    }

                    testDatabase.resultGold -= price;
                    itemIsCount[item].isCount = true;
                    itemIsCount[item].count++;

                    curCount = itemIsCount[item].count;

                    return;
                }

                else
                {
                    UnSelecteItem(gameitem, background, price);
                    curCount = 0;
                    return;
                }
            }
        }

        if (testDatabase.resultGold >= price)
        {
            items.Add(gameitem);

            itemIsCount.Add(gameitem,  new ItemCount(1, count));

            testDatabase.resultGold -= price;

            background.SetActive(true);
        }

        if (!count)
        {
            curCount = 0;
        }

        else
        {
            curCount = 1;
        }
    }

    public void UnSelecteItem(GameObject gameitem,GameObject background, int price)
    {
        if (itemIsCount[gameitem].isCount)
        {
            itemIsCount[gameitem].count--;

            testDatabase.resultGold += price;

            if (itemIsCount[gameitem].count == 0)
            {
                items.Remove(gameitem);
                itemIsCount.Remove(gameitem);

                background.SetActive(false);
            }
        }

        else
        {
            items.Remove(gameitem);

            testDatabase.resultGold += price;

            background.SetActive(false);
        }
    }

    public void OnSceneLoeded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if(scene.name == "Main")
        {
            for (int i = 0;i < items.Count; i++)
            {
                GameObject gameItem = Instantiate(items[i]);
                gameItem.transform.parent = transform;
                gameItem.transform.localPosition = Vector3.zero;

                Item itemCom = gameItem.GetComponent<Item>();

                itemCom.itemNum = i + 1;

                if (itemIsCount[items[i]].isCount)
                {
                    itemCom.count = itemIsCount[items[i]].count;
                }

                gameItems.Add(gameItem);
            }
        }

        else if(scene.name == "TitleScene")
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (itemIsCount[items[i]].isCount)
                {
                    if (itemIsCount[items[i]].count <= 0)
                    {
                        items.Remove(items[i]);
                        itemIsCount.Remove(items[i]);
                    }

                    else
                    {
                        itemIsCount[items[i]].count = gameItems[i].GetComponent<Item>().count;
                    }
                }

                else
                {
                    items.Remove(items[i]);
                }
            }

            foreach (var item in gameItems)
            {
                Destroy(item);
            }
            gameItems.Clear();
        }
    }
}
