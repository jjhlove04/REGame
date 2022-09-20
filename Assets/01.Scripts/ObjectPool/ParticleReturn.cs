using System.Collections.Generic;
using UnityEngine;
using MEC;

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
        Invoke("EndParticle", dieTime);
    }

    private void EndParticle()
    {
        objPool.ReturnGameObject(this.gameObject);
    }
}