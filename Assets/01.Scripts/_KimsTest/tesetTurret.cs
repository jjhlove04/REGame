using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tesetTurret : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float deltime = 0f;
    private float curTime = 0f;
    [SerializeField]
    bool isOn = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {  
        if (Input.GetKeyDown(KeyCode.E) && isOn)
        {
            isOn = false;
        }
        else if(Input.GetKeyDown(KeyCode.E))
        {
            isOn = true;
        }


        if(isOn)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0.6f, -0.2f), speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 1.8f, -0.2f), speed * Time.deltaTime);
        }

    }

    void Rote()
    {
        curTime += Time.deltaTime;

        this.gameObject.transform.Rotate(new Vector3(0, speed * Time.deltaTime, 0));

        if (curTime >= deltime)
        {
            speed += speed * 0.3f;
            curTime = 0;
        }
    }
}
