using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineSpawn : MonoBehaviour
{
    public Transform spawnPoint;
    private ObjectPool objectPool;
    public List<GameObject> mobs = new List<GameObject>();
    void Start()
    {
        objectPool = FindObjectOfType<ObjectPool>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectMOB(int count)
    {
        GameObject gameobj = objectPool.GetObject(mobs[count]);
        gameobj.transform.position = spawnPoint.position;
    }
}
