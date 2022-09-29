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

    TestTurretDataBase testTurretData;
    private void Start()
    {
        spawnManager = SpawnMananger.Instance;
        ui = InGameUII._instance;
        testTurretData = TestTurretDataBase.Instance;

        enemies[0].healthAmountMax += enemies[0].healthAmountMax * (testTurretData.plusCurse / 100);
        enemies[0].enemySpeed += enemies[0].enemySpeed * (testTurretData.plusCurse / 100);

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
