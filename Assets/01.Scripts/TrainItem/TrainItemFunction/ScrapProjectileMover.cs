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
        transform.Rotate(new Vector3(0, 240*Time.deltaTime, 0));

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).localPosition -= new Vector3(0, transform.GetChild(i).localPosition.y, 0);

            transform.GetChild(i).localRotation = Quaternion.Euler(new Vector3(0,0,0));
        }
    }

    public void Create(float damage, int count,Vector3 pos) 
    { 
        this.damage = damage;

        CreateScrap(count);

        StartCoroutine(Return());

        transform.position = pos;
    }

    private void CreateScrap(int count) 
    {
        if(count >= 10)
        {
            count = 10;
        }

        int pos = Random.Range(20, 50);

        float X;
        float Z;
        float angle = (-(pos / 2));

        for (int i = 0; i < count; i++)
        {
            Transform scrapTrm = transform.GetChild(i);

            scrapTrm.gameObject.SetActive(true);

            X = Mathf.Sin(Mathf.Deg2Rad * angle) * pos;
            Z = Mathf.Cos(Mathf.Deg2Rad * angle) * pos;

            scrapTrm.position = new Vector3(X, 0, Z);

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
        if (other.tag == "Enemy")
        {
            HealthSystem healthSystem = other?.GetComponent<HealthSystem>();

            healthSystem.Damage(damage);
        }
    }
}
