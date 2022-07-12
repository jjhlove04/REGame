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

    public BoxCollider collider;

    public float keppOffSpeed = 1;

    private int smokeCount = 0;

    private int fireCount = 0;

    public Vector3 center;
    public Vector3 size;

    private void Awake()
    {
        Instance = this;
    }


    public void CreateTrainPrefab(int trainCount)
    {
        curTrainCount = trainCount;

        for (int i = 0; i < curTrainCount; i++)
        {
            var newCube = Instantiate(trainPrefab,transform.Find("Train"));
            newCube.transform.SetParent(gameObject.transform);
            newCube.transform.localPosition = new Vector3(0f, 0f, -(i * distance));
            newCube.transform.localRotation = Quaternion.identity;

            trainContainer.Add(newCube);
        }
        MakeCollider();
    }

    /*public void RemoveList()
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
    }*/

    public void OnSmoke()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).Find("Smoke" + smokeCount)?.gameObject.SetActive(true);
        }

        smokeCount++;
    }

    public void AllOffSmoke()
    {
        for (int i = 0; i < smokeCount; i++)
        {
            for (int j = 0; j < transform.childCount; j++)
            {
                transform.GetChild(j).Find("Smoke" + i)?.gameObject.SetActive(false);
            }
        }
    }

    public void OffSmoke()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).Find("Smoke" + (smokeCount-1))?.gameObject.SetActive(false);
        }

        smokeCount--;
    }

    public void OnBlackSmoke()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).Find("BlackSmoke")?.gameObject.SetActive(true);
        }

        Invoke("AllOffSmoke", 1);
    }

    public void OnFire()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).Find("Fire" + fireCount)?.gameObject.SetActive(true);
        }

        fireCount++;
    }

    public void AllOffFire()
    {
        for (int i = 0; i < fireCount; i++)
        {
            for (int j = 0; j < transform.childCount; j++)
            {
                transform.GetChild(j).Find("Fire" + i)?.gameObject.SetActive(false);
            }
        }
    }

    public void OffFire()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).Find("Fire" + (fireCount - 1))?.gameObject.SetActive(false);
        }

        fireCount--;
    }

    public void OnExplotion()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).Find("ExplotionParticle")?.gameObject.SetActive(true);
        }
    }

    public void OffExplotion()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).Find("ExplotionParticle")?.gameObject.SetActive(false);
        }
    }

    public void MakeCollider()
    {
        
        collider.center = center = new Vector3(0, 5, (curTrainCount * distance) * -0.5f + 27);
        collider.size= size = new Vector3(6, 10, curTrainCount * distance + 27);
    }

    /*public void KeepOffTrain()
    {
        transform.position += -new Vector3(0, 0, keppOffSpeed);
    }*/
}