using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGround : MonoBehaviour
{
    private float size;

    public float speed = 1;

    [SerializeField]
    private int roundCheckCount = 5;

    public int mapNumber = 0;

    public List<TerrainData> terrainList = new List<TerrainData>();

    public List<TerrainData> terrainChainList = new List<TerrainData>();

    public TerrainData changeTerrainObj;
    public TerrainData changeTerrainChainObj;

    Action OnChangeTerrain;

    bool isChangingTerrain = false;

    private void Start()
    {
        size =  200;

        changeTerrainObj = terrainList[0];
        changeTerrainChainObj = terrainChainList[0];

        OnChangeTerrain += () =>
        {
            isChangingTerrain = true;

            mapNumber++;
            if (mapNumber == terrainList.Count)
            {
                mapNumber = 0;
            }

            changeTerrainObj = terrainList[mapNumber];
            changeTerrainChainObj = terrainChainList[mapNumber];
        };
    }


    private void Update()
    {
        if (GameManager.Instance.state != GameManager.State.Stop)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                MoveBackground(transform.GetChild(i));

                if (-(size * 1.75f) >= transform.GetChild(i).position.z)
                {
                    SwapBackground(transform.GetChild(i));
                    
                    if (SpawnMananger.Instance.round >= roundCheckCount)
                    {
                        OnChangeTerrain?.Invoke();

                        roundCheckCount+= 5;
                    }
                }
            }
        }
    }

    private void MoveBackground(Transform transform)
    {
        if (TrainScript.instance.destroy)
        {
            speed = Mathf.Lerp(speed, 0, Time.deltaTime);
        }

        transform.position -= new Vector3(0, 0, speed * GameManager.Instance.gameSpeed);
    }

    private void ChangeTerrainObj(Transform trm)
    {
        if (isChangingTerrain)
        {
            trm.GetChild(1).GetComponent<Terrain>().terrainData = changeTerrainChainObj;
            trm.GetChild(1).GetComponent<TerrainCollider>().terrainData = changeTerrainChainObj;
            isChangingTerrain = false;
        }

        else
        {
            trm.GetChild(1).GetComponent<Terrain>().terrainData = changeTerrainObj;
            trm.GetChild(1).GetComponent<TerrainCollider>().terrainData = changeTerrainObj;
        }
    }

    private void SwapBackground(Transform trm)
    {
        trm.transform.position = new Vector3(trm.transform.position.x , trm.transform.position.y, trm.transform.position.z + size * 3f);

        ChangeTerrainObj(trm);
    }
}