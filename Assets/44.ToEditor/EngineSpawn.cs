using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EngineSpawn : MonoBehaviour
{
    private static EngineSpawn _instance;
    public static EngineSpawn Instance
    {
        get
        {
            return _instance;
        }
    }
    public Transform spawnPoint;
    private ObjectPool objectPool;

    public Text input0, input1, input2, input3, input4, input5;

    public List<GameObject> mobs = new List<GameObject>();
    public List<GameObject> stateMobs = new List<GameObject>();

    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        objectPool = FindObjectOfType<ObjectPool>();
    }

    public void SelectMOB(int count)
    {
        GameObject gameobj = objectPool.GetObject(mobs[count]);
        gameobj.transform.position = spawnPoint.position;
        stateMobs.Add(gameobj);
        for (int i = 0; i < stateMobs.Count; i++)
        {
            stateMobs[i].GetComponent<TestEnemy>().ChangeDamage(float.Parse(input0.text));
            stateMobs[i].GetComponent<TestEnemy>().ChangeSpeed(float.Parse(input4.text));
            stateMobs[i].GetComponent<TestEnemy>().ChangeDistance(float.Parse(input5.text));
        }
        gameobj.transform.position = spawnPoint.position;
    }
}
