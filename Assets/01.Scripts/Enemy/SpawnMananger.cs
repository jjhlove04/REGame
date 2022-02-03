using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMananger : MonoBehaviour
{
    public static SpawnMananger Instance;
    private static SpawnMananger _insstance
    {
        get { return Instance; }
    }

    public int enemyCount;
    public int maxEnemyCount;


    private void Awake()
    {
        Instance = this;
    }
}
