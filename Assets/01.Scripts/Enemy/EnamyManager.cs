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
        else if (spawnManager.round % 14 == 0)
        {
            isDUp = true;
        }

        if (spawnManager.round % 10 == 0 && isHUp)
        {
            enemies[0].healthAmountMax += 1.5f;
            isHUp = false;
        }
        else if (spawnManager.round % 15 == 0 && isDUp)
        {
            enemies[0].damage += 1.6f;
            isDUp = false;
        }

        if (GameManager.Instance.TrainLevel < 3)
        {
            enemies[0].enemy.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
        }
        else if(GameManager.Instance.TrainLevel < 10)
        {
            enemies[0].enemy.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color(1, 0, 0);
        }
        else if (GameManager.Instance.TrainLevel < 12)
        {
            enemies[0].enemy.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color(0, 1, 0);
        }
        else if (GameManager.Instance.TrainLevel < 16)
        {
            enemies[0].enemy.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color(0, 0, 1);
        }

    }

    private void OnDisable()
    {
        enemies[0].damage = damage;
        enemies[0].healthAmountMax = healthAmountMax;
    }
}
