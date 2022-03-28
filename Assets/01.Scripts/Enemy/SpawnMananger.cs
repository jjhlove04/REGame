using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnMananger : MonoBehaviour
{
    public static SpawnMananger Instance;
    private static SpawnMananger _insstance
    {
        get { return Instance; }
    }

    public delegate void Spawn(int i);

    public Spawn spawn;

    public float curTime;
    public float roundCurTime;

    [HideInInspector]
    public int round;
    public int maxRound;

    private bool stopSpawn = false;

    private void Awake()
    {
        Instance = this;

        spawn = new Spawn((round)=> { });
    }
    private void Start()
    {
        curTime = roundCurTime;
    }

    private void Update()
    {
        if (!stopSpawn)
        {
            curTime += Time.deltaTime;

            if (curTime >= roundCurTime)
            {
                spawn(round);

                curTime = 0;
                round++;

                if (round > maxRound)
                {
                    stopSpawn = true;
                }
            }
        }
    }
}
