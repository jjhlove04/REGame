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

    public List<GameObject> items = new List<GameObject> ();

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

    public void SelecteItme(GameObject gameitem)
    {
        foreach (var item in items)
        {
            if(item == gameitem)
            {
                return;
            }
        }

        items.Add(gameitem);
    }

    public void Buy(int price)
    {
        TestDatabase.Instance.resultGold -= price;
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
