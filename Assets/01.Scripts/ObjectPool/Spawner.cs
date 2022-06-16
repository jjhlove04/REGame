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
    private GameObject prefab;

    [SerializeField]
    private float interval = 3;

    [SerializeField]
    private int round;

    [SerializeField]
    private bool boss = false;

    SpawnMananger spawnMananger;

    [SerializeField]
    private int adjustmentRound = 12;

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
        GameObject[] gm = new GameObject[30];

        for (int i = 0; i < gm.Length; i++)
        {
            gm[i] = objectPool.GetObject(prefab);
            gm[i].transform.position = transform.position;
        }
        yield return Timing.WaitForSeconds(0.1f);

        for (int i = 0; i < gm.Length; i++)
        {
            gm[i].SetActive(false);
            gm[i].transform.position = transform.position;
        }
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

        if(s % 20 == 0)
        {
            spawnAmount = spawnAmount + 1;
        }

        if (round <= s)
        {
            for (int i = 0; i < (int)spawnAmount; i++)
            {
                float randX = Random.Range(-interval, interval);
                float randY = Random.Range(-interval, interval);

                Vector3 randPos = new Vector3(randX, 0, randY);

                GameObject newPrefab = objectPool.GetObject(prefab);
                newPrefab.transform.position = transform.position + randPos;

                yield return new WaitForSeconds(time);
            }
        }
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

                GameObject newPrefab = objectPool.GetObject(prefab);
                newPrefab.transform.position = transform.position + randPos;
            }
        }
    }


}


// 라운드당 스폰할 적 숫자를 정한다.
// Spawnmanager에서 다음 라운드까지의 시간을 카운트한다.
// 라운드가 시작되면 spawner에서 인스펙터에서 받아온 변수를 통해 정해진 적 유닛의 숫자만큼 소환한다.