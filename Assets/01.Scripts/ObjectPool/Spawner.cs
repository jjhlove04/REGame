using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

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

        Timing.RunCoroutine(spawnFirst());

        if (!boss)
        {
            spawnMananger.spawn += new SpawnMananger.Spawn(SpwanEnemy);
        }
        else
        {
            spawnMananger.spawn += new SpawnMananger.Spawn(BossSpwanEnemy);
        }

    }

    IEnumerator<float> spawnFirst()
    {
        GameObject[] gm = new GameObject[30];

        for (int i = 0; i < gm.Length; i++)
        {
            gm[i] = objectPool.GetObject(prefab);
        }
        yield return Timing.WaitForSeconds(0.1f);

        for (int i = 0; i < gm.Length; i++)
        {
            gm[i].SetActive(false);
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