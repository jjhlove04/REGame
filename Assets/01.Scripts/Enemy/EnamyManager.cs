using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnamyManager : MonoBehaviour
{
    private float damage = 1;
    private float healthAmountMax = 1;

    private bool isHUp = false;

    public List<EnemyData> enemies = new List<EnemyData>();

    SpawnMananger spawnManager;
    InGameUII ui;
    private void Start()
    {
        spawnManager = SpawnMananger.Instance;
        ui = InGameUII._instance;
    }
    private void Update()
    {
        if ((int)ui.milliSec != 0)
        {
            if ((int)ui.milliSec % 19 == 0)
            {
                isHUp = true;
            }

            if ((int)ui.milliSec % 20 == 0 && isHUp)
            {
                enemies[0].healthAmountMax += 1.5f;
                enemies[0].damage += 1.6f;
                isHUp = false;
            }
        }

    }

    private void OnDisable()
    {
        enemies[0].damage = damage;
        enemies[0].healthAmountMax = healthAmountMax;
    }
}
