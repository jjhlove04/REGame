using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class Spawner : MonoBehaviour
{
    public float spawnAmount;

    public float amountIncreasion;

    public GameObject enemyMark;
    public GameObject[] enemyCounts;
    private ObjectPool objectPool;
    [SerializeField]
    private GameObject[] prefab;

    [SerializeField]
    private float interval = 3;

    [SerializeField]
    private int round;

    [SerializeField]
    private bool boss = false;

    SpawnMananger spawnMananger;

    [SerializeField]
    private int adjustmentRound = 12;

    public int[] spawnRate;
    private void Start()
    {
        spawnMananger = SpawnMananger.Instance;

        objectPool = FindObjectOfType<ObjectPool>();

        Timing.RunCoroutine(spawnFirst());
        //Timing.RunCoroutine(FindEnemyMarkerLogic());

        if (!boss)
        {
            spawnMananger.spawn += new SpawnMananger.Spawn(SpwanEnemy);
        }
        else
        {
            //InGameUII._instance.bossIcon.SetActive(true);
            spawnMananger.spawn += new SpawnMananger.Spawn(BossSpwanEnemy);
        }

    }

    public IEnumerator<float> spawnFirst()
    {
        GameObject[] gm = new GameObject[20];
        GameObject[] gm1 = new GameObject[20];
        GameObject[] gm2 = new GameObject[20];
        GameObject[] gm3 = new GameObject[20];
        GameObject[] gm4 = new GameObject[20];

        for (int i = 0; i < gm.Length; i++)
        {
            GetPrefab(gm, i, prefab[0]);
            GetPrefab(gm1, i, prefab[1]);
            GetPrefab(gm2, i, prefab[2]);
            GetPrefab(gm3, i, prefab[3]);
            GetPrefab(gm4, i, prefab[4]);

        }
        yield return Timing.WaitForSeconds(0.1f);

        for (int i = 0; i < gm.Length; i++)
        {
            SetPrefab(gm, i);
            SetPrefab(gm1, i);
            SetPrefab(gm2, i);
            SetPrefab(gm3, i);
            SetPrefab(gm4, i);
        }
    }

    public void GetPrefab(GameObject[] gm, int i, GameObject prefab)
    {
        gm[i] = objectPool.GetObject(prefab);
        gm[i].transform.position = transform.position;
    }

    public void SetPrefab(GameObject[] gm, int i)
    {
        gm[i].SetActive(false);
        gm[i].transform.position = transform.position;
    }

    public IEnumerator<float> FindEnemyMarkerLogic()
    {
        GameObject[] markers = new GameObject[10];
        for (int i = 0; i < markers.Length; i++)
        {
            markers[i] = objectPool.GetObject(enemyMark);
            markers[i].transform.position = transform.position;
        }
        yield return Timing.WaitForSeconds(0.1f);

        for (int i = 0; i < markers.Length; i++)
        {
            markers[i].SetActive(false);
            markers[i].transform.position = transform.position;
        }
    }

    private void SpwanEnemy(int s)
    {
        StartCoroutine(Spawn(s));
    }

    IEnumerator Spawn(int s)
    {
        //float amount = spawnAmount + (s * amountIncreasion);

        float time = spawnMananger.Info.roundCurTime; /// amount;

        //if (s % adjustmentRound == 0)
        //{
        //    amount = amount * 0.6f;
        //}

        SpawnRateSet();

        if (s % 20 == 0)
        {
            spawnAmount = spawnAmount + 1;
        }

        if (round <= s)
        {
            for (int i = 0; i < (int)spawnAmount; i++)
            {
                float randX = Random.Range(-interval, interval);
                float randY = Random.Range(-interval, interval);

                int randPrefab = Random.Range(0, 101);

                Vector3 randPos = new Vector3(randX, 0, randY);

                if (randPrefab < spawnRate[0])
                {
                    SpawnPrefab(prefab[0], randPos);
                }
                else if (randPrefab < spawnRate[1])
                {
                    SpawnPrefab(prefab[1], randPos);
                }
                else if (randPrefab < spawnRate[2])
                {
                    SpawnPrefab(prefab[2], randPos);
                }
                else if (randPrefab < spawnRate[3])
                {
                    SpawnPrefab(prefab[3], randPos);
                }
                else if (randPrefab < spawnRate[4])
                {
                    SpawnPrefab(prefab[4], randPos);
                }
                yield return new WaitForSeconds(time);
            }
        }
    }

    public void SpawnPrefab(GameObject prefab, Vector3 randPos)
    {
        GameObject newPrefab = objectPool.GetObject(prefab);
        newPrefab.transform.position = transform.position + randPos;

    }

    public void SpawnRateSet()
    {
        if(spawnMananger.round < 150)
        {
            SpawnRateBase(96, 1, 1, 1, 1);
        }
        else if(spawnMananger.round < 360)
        {
            SpawnRateBase(70, 12, 6, 6, 6);
        }
        else if (spawnMananger.round < 600)
        {
            SpawnRateBase(48, 13, 13, 13, 13);
        }
        else if (spawnMananger.round < 810)
        {
            SpawnRateBase(31, 17, 17, 17, 18);
        }
        else
        {
            SpawnRateBase(8, 23, 23, 23, 23);
        }
    }

    public void SpawnRateBase(int a, int b, int c, int d, int e)
    {
        spawnRate[0] = a;
        spawnRate[1] = spawnRate[0] + b;
        spawnRate[2] = spawnRate[1] + c;
        spawnRate[3] = spawnRate[2] + d;
        spawnRate[4] = spawnRate[3] + e;
    }

    private void BossSpwanEnemy(int s)
    {
        if (s % round == 0)
        {
            for (int i = 0; i < spawnAmount; i++)
            {
                float randX = Random.Range(-interval, interval);
                float randY = Random.Range(-interval, interval);

                Vector3 randPos = new Vector3(randX, 0, randY);

                GameObject newPrefab = objectPool.GetObject(prefab[0]);
                newPrefab.transform.position = transform.position + randPos;
            }
        }
    }


}


// 라운드당 스폰할 적 숫자를 정한다.
// Spawnmanager에서 다음 라운드까지의 시간을 카운트한다.
// 라운드가 시작되면 spawner에서 인스펙터에서 받아온 변수를 통해 정해진 적 유닛의 숫자만큼 소환한다.