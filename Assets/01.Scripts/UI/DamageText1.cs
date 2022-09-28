using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DamageText1 : MonoBehaviour
{
    
    void Start()
    {
        this.transform.DOJump(new Vector3(0,-2,0), 4,1,1);
        Destroy(gameObject, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        // if(TrainScript.instance.isCritical)
        // {
        //     this.gameObject.GetComponent<TextMesh>().color = Color.red;
        // }
    }
    
}
