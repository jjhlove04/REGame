using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBase : MonoBehaviour
{
    private ObjectPool objPool;
    public TrainItem itemPrefab;
    void Start()
    {
        objPool = ObjectPool.instacne;
    }

    private void Update()
    {
        if (itemPrefab != null)
        {
            transform.GetChild(0).GetComponent<Text>().text = "x" + itemPrefab.GetComponent<TrainItem>().curCarry;
        }
    }

    private void OnDisable()
    {
        objPool.ReturnGameObject(this.gameObject);
    }
}
