using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReturn : MonoBehaviour
{
    private ObjectPool objPool;

    void Start()
    {
        objPool = FindObjectOfType<ObjectPool>();
    }

    private void OnDisable()
    {
        objPool.ReturnGameObject(this.gameObject);
    }

}
