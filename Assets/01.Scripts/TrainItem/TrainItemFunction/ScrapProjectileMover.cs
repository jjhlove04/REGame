using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapProjectileMover : MonoBehaviour
{
    private float damage;

    ObjectPool objectPool;

    private void Start()
    {
        objectPool = ObjectPool.instacne;
    }

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 360*Time.deltaTime, 0));
    }

    public void Create(float damage, int count) 
    { 
        this.damage = damage;

        CreateScrap(count);

        StartCoroutine(Return());
    }

    private void CreateScrap(int count) 
    {
        if(count >= 10)
        {
            count = 10;
        }

        int pos = Random.Range(20, 70);

        float X;
        float Z;
        float angle = (-(pos / 2));

        for (int i = 0; i < count; i++)
        {
            Transform scrapTrm = transform.GetChild(i);

            scrapTrm.gameObject.SetActive(true);

            X = Mathf.Sin(Mathf.Deg2Rad * angle) * pos;
            Z = Mathf.Cos(Mathf.Deg2Rad * angle) * pos;

            scrapTrm.localRotation = Quaternion.Euler(new Vector3(0,0,0));

            scrapTrm.localPosition = new Vector3(X, 0,Z);

            angle += (360 / count);
        }

        
    }


    IEnumerator Return()
    {
        yield return new WaitForSeconds(5f);

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        objectPool.ReturnGameObject(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        HealthSystem healthSystem = other.GetComponent<HealthSystem>();

        print(1);

        if (other.tag == "Enemy")
        {
            print(2);
            healthSystem.Damage(damage);
        }
    }
}
