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
        objPool = FindObjectOfType<ObjectPool>();
    }

    private void OnEnable()
    {
        StopCoroutine(EndParticle());
        Timing.RunCoroutine(EndParticle());
    }

    IEnumerator<float> EndParticle()
    {
        yield return Timing.WaitForSeconds(dieTime);
        objPool.ReturnGameObject(this.gameObject);
    }
}