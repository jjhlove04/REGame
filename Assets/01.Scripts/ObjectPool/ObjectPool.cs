using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instacne { get; private set; }
    private void Awake()
    {
        instacne = this;
    }

    private Dictionary<string, Queue<GameObject>> objectPool = new Dictionary<string, Queue<GameObject>>();

    public GameObject GetObject(GameObject gameObject)
    {
        if(objectPool.TryGetValue(gameObject.name, out Queue<GameObject> objectList))
        {
            if(objectList.Count == 0)
            {
                return CreateNewObject(gameObject);
            }
            else
            {
                GameObject obj = objectList.Dequeue();
                obj.SetActive(true);
                return obj;
            }
        }
        else
        {
            return CreateNewObject(gameObject);
        }
    }

    private GameObject CreateNewObject(GameObject gameObject)
    {
        GameObject newObject = Instantiate(gameObject);
        newObject.name = gameObject.name;
        return newObject;
    }

    public void ReturnGameObject(GameObject gameObject)
    {
        if(objectPool.TryGetValue(gameObject.name, out Queue<GameObject> objectList))
        {
            objectList.Enqueue(gameObject);
        }
        else
        {
            Queue<GameObject> newObjQueue = new Queue<GameObject>();
            newObjQueue.Enqueue(gameObject);
            objectPool.Add(gameObject.name, newObjQueue);
        }

        gameObject.SetActive(false);
    }
}
