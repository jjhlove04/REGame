using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainEffective : MonoBehaviour
{
    public ParticleSystem trainSmokeParticle;
    public GameObject smokePos;
    

    private void Start() {
        SmokeStart();
    }
    private void Update() {
        trainSmokeParticle.transform.position = smokePos.transform.position;
        
    }

    public void SmokeStart()
    {
        Instantiate(trainSmokeParticle, smokePos.transform.position , Quaternion.identity);
        trainSmokeParticle.Play();
    }
}
