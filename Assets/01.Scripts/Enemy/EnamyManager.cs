using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnamyManager : MonoBehaviour
{
    private float damage;
    private float healthAmountMax;

    private bool isUp = false;

    public List<EnemyData> enemies = new List<EnemyData>();

    SpawnMananger spawnManager;
    private void Start()
    {
        spawnManager = SpawnMananger.Instance;
    }
    private void Update()
    {
        if (spawnManager.round % 15 == 0 || spawnManager.round % 20 == 0)
        {
            isUp = true;
            if (spawnManager.round % 15 == 0 && isUp)
            {
                enemies[0].healthAmountMax += 1;
                isUp = false;
            }
            else if (spawnManager.round % 20 == 0 && isUp)
            {
                enemies[0].damage += 1;
                isUp = false;
            }
        }
    }

    private void OnDisable()
    {
        enemies[0].damage = 1;
        enemies[0].healthAmountMax = 1;
    }
}
