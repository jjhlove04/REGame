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
    private int round;

    private void Start()
    {
        objectPool = FindObjectOfType<ObjectPool>();

        SpawnMananger.Instance.spawn += new SpawnMananger.Spawn(SpwanEnemy);
    }

    private void SpwanEnemy(int s)
    {
        if(round <= SpawnMananger.Instance.round)
        for (int i = 0; i < spawnAmount; i++)
        {
            GameObject newPrefab = objectPool.GetObject(prefab);
            newPrefab.transform.position = this.transform.position;
        }
    }
}


// ����� ������ �� ���ڸ� ���Ѵ�.
// Spawnmanager���� ���� ��������� �ð��� ī��Ʈ�Ѵ�.
// ���尡 ���۵Ǹ� spawner���� �ν����Ϳ��� �޾ƿ� ������ ���� ������ �� ������ ���ڸ�ŭ ��ȯ�Ѵ�.