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


// ����� ������ �� ���ڸ� ���Ѵ�.
// Spawnmanager���� ���� ��������� �ð��� ī��Ʈ�Ѵ�.
// ���尡 ���۵Ǹ� spawner���� �ν����Ϳ��� �޾ƿ� ������ ���� ������ �� ������ ���ڸ�ŭ ��ȯ�Ѵ�.