using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText1 : MonoBehaviour
{
    
    void Start()
    {
        Destroy(gameObject, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(new Vector3(0,0.5f,0));
    }
}
