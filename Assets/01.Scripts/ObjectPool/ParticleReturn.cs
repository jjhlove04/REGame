using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleReturn : MonoBehaviour
{
    private ObjectPool objPool;

    [SerializeField]
    private float dieTime;

    void Start()
    {
        objPool = FindObjectOfType<ObjectPool>();
    }

    private void OnEnable()
    {
        StopCoroutine(EndParticle());
        StartCoroutine(EndParticle());
    }

    IEnumerator EndParticle()
    {
        yield return new WaitForSeconds(dieTime);
        objPool.ReturnGameObject(this.gameObject);
    }
}