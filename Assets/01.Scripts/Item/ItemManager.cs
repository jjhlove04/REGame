using System.Collections;
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

    public void SelecteItem(GameObject gameitem, int price, GameObject background, bool count, int maxCount)
    {
        foreach (var item in items)
        {
            if (item == gameitem)
            {
                if (count)
                {
                    if (!(TestDatabase.Instance.resultGold >= price)&&!(maxCount <= itemIsCount[item].count))
                    {
                        return;
                    }

                    TestDatabase.Instance.resultGold -= price;
                    itemIsCount[item].isCount = true;
                    itemIsCount[item].count++;

                    return;
                }

                else
                {
                    UnSelecteItem(gameitem, background, price);
                    return;
                }
            }
        }

        if (TestDatabase.Instance.resultGold >= price)
        {
            items.Add(gameitem);

            itemIsCount.Add(items[items.Count - 1], new ItemCount(1, false));

            TestDatabase.Instance.resultGold -= price;

            background.SetActive(true);
        }
    }

    public void UnSelecteItem(GameObject gameitem,GameObject background, int price)
    {
        if (itemIsCount[gameitem].isCount)
        {
            itemIsCount[gameitem].count--;

            TestDatabase.Instance.resultGold += price;

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

            TestDatabase.Instance.resultGold += price;

            background.SetActive(false);
        }
    }

    public void OnSceneLoeded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if(scene.name == "Main")
        {
            foreach (var item in items)
            {
                GameObject gameItem = Instantiate(item);
                gameItem.transform.parent = GameObject.Find("Items").transform;
                gameItem.transform.localPosition = Vector3.zero;

                if (itemIsCount[item].isCount)
                {
                    gameItem.GetComponent<Item>().count = itemIsCount[item].count;
                }

                gameItems.Add(gameItem);
            }
        }

        else if(scene.name == "TitleScene")
        {
            foreach (var item in items)
            {
                if (itemIsCount[item].count <= 0)
                {
                    items.Remove(item);
                }
            }
            gameItems.Clear();
        }
    }
}
