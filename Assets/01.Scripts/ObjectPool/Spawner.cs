using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int spawnAmount;
    private ObjectPool objectPool;
    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private float interval=3;

    [SerializeField]
    private int round;

    private void Start()
    {
        objectPool = FindObjectOfType<ObjectPool>();

        SpawnMananger.Instance.spawn += new SpawnMananger.Spawn(SpwanEnemy);
    }

    private void SpwanEnemy(int s)
    {
        if(round <= SpawnMananger.Instance.round)
        {
            for (int i = 0; i < spawnAmount; i++)
            {
                float randX = Random.Range(-interval, interval);
                float randY = Random.Range(-interval, interval);

                Vector3 randPos = new Vector3(randX,0,randY);

                GameObject newPrefab = objectPool.GetObject(prefab);
                newPrefab.transform.position = transform.position + randPos;
            }
        }
    }
}


// 라운드당 스폰할 적 숫자를 정한다.
// Spawnmanager에서 다음 라운드까지의 시간을 카운트한다.
// 라운드가 시작되면 spawner에서 인스펙터에서 받아온 변수를 통해 정해진 적 유닛의 숫자만큼 소환한다.