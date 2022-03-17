using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraQuarterViewMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 20;

    [SerializeField]
    private GameObject topCamera;

    private void Update()
    {
        float speed = this.speed;

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            topCamera.SetActive(true);
            gameObject.SetActive(false);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed *= 2;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += -Vector3.forward *Time.deltaTime * speed;
        }        
        
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.forward * Time.deltaTime * speed;
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z, -65, 10));
    }
}
