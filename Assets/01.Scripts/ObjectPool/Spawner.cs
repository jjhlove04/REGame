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


// ����� ������ �� ���ڸ� ���Ѵ�.
// Spawnmanager���� ���� ��������� �ð��� ī��Ʈ�Ѵ�.
// ���尡 ���۵Ǹ� spawner���� �ν����Ϳ��� �޾ƿ� ������ ���� ������ �� ������ ���ڸ�ŭ ��ȯ�Ѵ�.