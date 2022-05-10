using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemManager : MonoBehaviour
{
    private static ItemManager instance;

    public static ItemManager Instance
    {
        get { return instance; }
    }

    [HideInInspector]
    public List<GameObject> items = new List<GameObject> ();

    [HideInInspector]
    public List<GameObject> gameItems = new List<GameObject> ();

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

    public void SelecteItem(GameObject gameitem, int price, GameObject background)
    {

        foreach (var item in items)
        {
            if (item == gameitem)
            {
                UnSelecteItem(gameitem,background, price);
                return;
            }
        }

        if (TestDatabase.Instance.resultGold >= price)
        {
            items.Add(gameitem);

            TestDatabase.Instance.resultGold -= price;

            background.SetActive(true);
        }
    }

    public void UnSelecteItem(GameObject gameitem,GameObject background, int price)
    {
        items.Remove(gameitem);

        TestDatabase.Instance.resultGold += price;

        background.SetActive(false);
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

                gameItems.Add(gameItem);
            }
        }

        else if(scene.name == "TitleScene")
        {
            items.Clear();
        }
    }
}
