using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    int maxCount = 0;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => 
        {
            ItemManager.Instance.SelecteItem(item, price, background, count, maxCount);
        });
    }
}
