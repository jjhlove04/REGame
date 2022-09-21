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

    public GameObject train;

    public GameObject mortarTube;

    public bool onBurningCoal = false;
    private int burningCoalAmount = 8;
    [SerializeField]
    private GameObject burningCoalProjectile;

    public LayerMask enemy;

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
        transform.GetChild(0).Find("Smoke" + smokeCount)?.gameObject.SetActive(true);

        for (int i = 0; i < train.transform.childCount; i++)
        {
            train.transform.GetChild(i).Find("Smoke" + smokeCount)?.gameObject.SetActive(true);
        }

        if(smokeCount < 2)
        {
            smokeCount++;
        }
    }

    public void AllOffSmoke()
    {
        for (int i = 0; i < smokeCount; i++)
        {
            transform.GetChild(0).Find("Smoke" + i)?.gameObject.SetActive(false);

            for (int j = 0; j < train.transform.childCount; j++)
            {
                train.transform.GetChild(j).Find("Smoke" + i)?.gameObject.SetActive(false);
            }
        }

        smokeCount = 0;
    }

    public void OffSmoke()
    {
        transform.GetChild(0).Find("Smoke" + (smokeCount - 1))?.gameObject.SetActive(false);

        for (int i = 0; i < train.transform.childCount; i++)
        {
            train.transform.GetChild(i).Find("Smoke" + (smokeCount-1))?.gameObject.SetActive(false);
        }

        if(smokeCount > 0)
        {
            smokeCount--;
        }
    }

    public void OnBlackSmoke()
    {
        transform.GetChild(0).Find("BlackSmoke")?.gameObject.SetActive(true);

        for (int i = 0; i < train.transform.childCount; i++)
        {
            train.transform.GetChild(i).Find("BlackSmoke")?.gameObject.SetActive(true);
        }

        Invoke("AllOffSmoke", 1);
    }

    public void OffBlackSmoke()
    {
        transform.GetChild(0).Find("BlackSmoke")?.gameObject.SetActive(false);

        for (int i = 0; i < train.transform.childCount; i++)
        {
            train.transform.GetChild(i).Find("BlackSmoke")?.gameObject.SetActive(false);
        }
    }

    public void OnFire()
    {
        transform.GetChild(0).Find("Fire" + fireCount)?.gameObject.SetActive(true);

        for (int i = 0; i < train.transform.childCount; i++)
        {
            train.transform.GetChild(i).Find("Fire" + fireCount)?.gameObject.SetActive(true);
        }

        if(fireCount < 2)
        {
            fireCount++;
        }
    }

    public void AllOffFire()
    {
        for (int i = 0; i < fireCount; i++)
        {
            transform.GetChild(0).Find("Fire" + i)?.gameObject.SetActive(false);

            for (int j = 0; j < train.transform.childCount; j++)
            {
                train.transform.GetChild(j).Find("Fire" + i)?.gameObject.SetActive(false);
            }
        }
    }

    public void OffFire()
    {
        transform.GetChild(0).Find("Fire" + (fireCount - 1))?.gameObject.SetActive(false);

        for (int i = 0; i < train.transform.childCount; i++)
        {
            train.transform.GetChild(i).Find("Fire" + (fireCount - 1))?.gameObject.SetActive(false);
        }

        fireCount--;
    }

    public void OnExplotion()
    {
        transform.GetChild(0).Find("ExplotionParticle")?.gameObject.SetActive(true);

        for (int i = 0; i < train.transform.childCount; i++)
        {
            train.transform.GetChild(i).Find("ExplotionParticle")?.gameObject.SetActive(true);
        }
    }

    public void OffExplotion()
    {
        transform.GetChild(0).Find("ExplotionParticle")?.gameObject.SetActive(false);

        for (int i = 0; i < train.transform.childCount; i++)
        {
            train.transform.GetChild(i).Find("ExplotionParticle")?.gameObject.SetActive(false);
        }
    }

    public void MakeCollider()
    {       
        collider.center = center = new Vector3(0, 5, (curTrainCount * distance) * -0.5f + 27);
        collider.size= size = new Vector3(6, 10, curTrainCount * distance + 27);
    }

    public void MortarTube(float damage)
    {
        for (int i = 0; i < 2; i++)
        {
            GameObject bullet = ObjectPool.instacne.GetObject(mortarTube);

            bullet.GetComponent<MortarTubeProjectileMover>().Create(damage);

            bullet.transform.position = new Vector3(transform.position.x,5, (-(trainContainer.Count) * 20)+27);
        }    
    }

    public void OnBurningCoal()
    {
        if (onBurningCoal)
        {
            burningCoalAmount += 2;
        }

        onBurningCoal = true;
    }

    public void BurningCoal(int damage)
    {
        List<Transform> enemys = LookForTargets(new Vector3(100, 0, 0));

        int count = enemys.Count < burningCoalAmount ? enemys.Count : burningCoalAmount;

        for (int i = 0; i < count; i++)
        {
            GameObject bullet = ObjectPool.instacne.GetObject(burningCoalProjectile);   

            bullet.GetComponent<BurningCoalProjectileMover>().Create(damage, enemys[i]);

            bullet.transform.position = new Vector3(transform.position.x, 5, (-(trainContainer.Count) * 20) + 27);
        }
    }

    List<Transform> LookForTargets(Vector3 targetPos)
    {
        List<Transform> enemys = new List<Transform>();

        Collider[] hit = Physics.OverlapSphere(targetPos, 50, enemy);

        Transform targetEnemy = null;

        int count = hit.Length > burningCoalAmount ? burningCoalAmount : hit.Length;

        for (int i = 0; i < count; i++)
        {
            Enemy enemy = hit[i].GetComponent<Enemy>();
            if (hit[i].CompareTag("Enemy"))
            {
                if (!enemy.isDying)
                {
                    targetEnemy = hit[i].transform;
                }
            }
        }

        enemys.Add(targetEnemy);

        return enemys;
    }

    /*public void KeepOffTrain()
    {
        transform.position += -new Vector3(0, 0, keppOffSpeed);
    }*/
}