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

        for (int i = 0; i < 5; i++)
        {
            enemies[i].healthAmountMax += enemies[i].healthAmountMax * (testTurretData.plusCurse / 100);
            enemies[i].enemySpeed += enemies[i].enemySpeed * (testTurretData.plusCurse / 100);
        }

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
                enemies[0].healthAmountMax += 5f;
                enemies[0].damage += 1.6f;

                enemies[1].healthAmountMax += 5f;
                enemies[1].damage += 3f;

                enemies[2].healthAmountMax += 5f;
                enemies[2].damage += 1.6f;

                enemies[3].healthAmountMax += 7f;
                enemies[3].damage += 1.6f;

                enemies[4].healthAmountMax += 11f;
                enemies[4].damage += 1.6f;


                isHUp = false;
            }
        }

    }

    private void OnDisable()
    {
        for (int i = 0; i < 5; i++)
        {
            enemies[i].damage = damage;
            enemies[i].healthAmountMax = healthAmountMax;
        }
    }
}
