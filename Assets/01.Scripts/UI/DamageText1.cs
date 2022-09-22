using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DamageText1 : MonoBehaviour
{
    
    void Start()
    {
        Destroy(gameObject, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.DOJump(new Vector3(0,-2,0), 4,1,3);
    }
}
