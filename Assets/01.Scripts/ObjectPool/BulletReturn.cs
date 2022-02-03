using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletReturn : MonoBehaviour
{
    private ObjectPool objPool;

    void Start()
    {
        objPool = FindObjectOfType<ObjectPool>();
    }

    void Update()
    {

    }

    private void OnDisable()
    {
        objPool.ReturnGameObject(this.gameObject);
    }
}