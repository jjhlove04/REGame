using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnamyManager : MonoBehaviour
{
    private float damage = 1;
    private float healthAmountMax = 1;

    private bool isHUp = false;
    private bool isDUp = false;

    public List<EnemyData> enemies = new List<EnemyData>();

    SpawnMananger spawnManager;
    private void Start()
    {
        spawnManager = SpawnMananger.Instance;
    }
    private void Update()
    {
        if (spawnManager.round % 9 == 0)
        {
            isHUp = true;
        }
        else if(spawnManager.round % 14 == 0)
        {
            isDUp = true;
        }

        if (spawnManager.round % 10 == 0 && isHUp)
        {
            enemies[0].healthAmountMax += 1;
            isHUp = false;
        }
        else if (spawnManager.round % 15 == 0 && isDUp)
        {
            enemies[0].damage += 1;
            isDUp = false;
        }
    }

    private void OnDisable()
    {
        enemies[0].damage = damage;
        enemies[0].healthAmountMax = healthAmountMax;
    }
}
