using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainManager : MonoBehaviour
{
    private static TrainManager Instance;
    public static TrainManager instance { get { return Instance; } }

    [SerializeField]
    private GameObject trainPrefab;
    [SerializeField]
    private float distance; //���� ����� ���� ���ؾ���

    public List<GameObject> trainContainer = new List<GameObject>();

    public int curTrainCount;
    public int maxTrainCount;

    public BoxCollider collider;

    public float keppOffSpeed = 1;

    private void Awake()
    {
        Instance = this;
    }


    public void CreateTrainPrefab()
    {
        if (transform.childCount != 0)
        {
            for (int i = transform.childCount - 1; i > 0; i--)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }

        for (int i = 0; i < curTrainCount; i++)
        {
            var newCube = Instantiate(trainPrefab);
            newCube.transform.SetParent(gameObject.transform);
            newCube.transform.localPosition = new Vector3(0f, 0f, -(i * distance));
            newCube.transform.localRotation = Quaternion.identity;

            trainContainer.Add(newCube);
        }
        MakeCollider();
    }

    public void RemoveList()
    {
        if (curTrainCount >= 0)
        {
            RemovePrefab();
            trainContainer.RemoveAt(trainContainer.Count - 1);
            //trainContainer.RemoveRange(0, curTrainCount + 1);
        }
    }

    private void RemovePrefab()
    {
        DestroyImmediate(trainContainer[trainContainer.Count - 1]);
        MakeCollider();
    }

    public void OnSmoke()
    {
        for (int i = 0; i < trainContainer.Count; i++)
        {
            trainContainer[i].transform.Find("Particle").gameObject.SetActive(true);
        }
    }

    public void Explotion()
    {
        for (int i = 0; i < trainContainer.Count; i++)
        {
            trainContainer[i].transform.Find("ExplotionParticle").gameObject.SetActive(true);
        }
    }

    public void MakeCollider()
    {
        collider.center = new Vector3(0, 5, (curTrainCount * distance) * -0.5f + 27);
        collider.size = new Vector3(6, 10, curTrainCount * distance + 27);
    }

    public void KeepOffTrain()
    {
        
        transform.position += -new Vector3(0, 0, keppOffSpeed);
    }
}