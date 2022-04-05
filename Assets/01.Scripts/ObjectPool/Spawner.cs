using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float spawnAmount;

    public float amountIncreasion;

    private ObjectPool objectPool;
    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private float interval=3;

    [SerializeField]
    private int round;

    [SerializeField]
    private bool boss = false;

    SpawnMananger spawnMananger;

    private void Start()
    {
        spawnMananger = SpawnMananger.Instance;

        objectPool = FindObjectOfType<ObjectPool>();

        if (!boss)
        {
            spawnMananger.spawn += new SpawnMananger.Spawn(SpwanEnemy);
        }
        else
        {
            spawnMananger.spawn += new SpawnMananger.Spawn(BossSpwanEnemy);
        }

    }

    private void SpwanEnemy(int s)
    {
        int amount = (int)(spawnAmount + (s * amountIncreasion));

        if (round <= s)
        {
            for (int i = 0; i < amount; i++)
            {
                float randX = Random.Range(-interval, interval);
                float randY = Random.Range(-interval, interval);

                Vector3 randPos = new Vector3(randX,0,randY);

                GameObject newPrefab = objectPool.GetObject(prefab);
                newPrefab.transform.position = transform.position + randPos;
            }
        }
    }
    
    private void BossSpwanEnemy(int s)
    {
        if(round == s)
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