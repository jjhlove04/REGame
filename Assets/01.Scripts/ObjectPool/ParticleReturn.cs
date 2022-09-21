using System.Collections.Generic;
using UnityEngine;
using MEC;
using System.Collections;

public class ParticleReturn : MonoBehaviour
{
    private ObjectPool objPool;

    [SerializeField]
    private float dieTime;

    void Start()
    {
        objPool = ObjectPool.instacne;
    }

    private void OnEnable()
    {
        StartCoroutine(ObjReturn());
    }

    private IEnumerator ObjReturn()
    {
        yield return new WaitForSeconds(dieTime);

        objPool.ReturnGameObject(gameObject);
    }
}