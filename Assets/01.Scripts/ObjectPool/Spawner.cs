using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float spawnTime = 5f;
    public float curTime;

    private ObjectPool objectPool;
    [SerializeField]
    private GameObject prefab;

    private void Start()
    {
        objectPool = FindObjectOfType<ObjectPool>();
    }

    private void Update()
    {
        
        curTime += Time.deltaTime;
        if (curTime >= spawnTime)
        {
            if (SpawnMananger.Instance.enemyCount < SpawnMananger.Instance.maxEnemyCount)
            {
                GameObject newPrefab = objectPool.GetObject(prefab);
                newPrefab.transform.position = this.transform.position;
                curTime = 0f;
                SpawnMananger.Instance.enemyCount++;
            }
        }
    }
}
