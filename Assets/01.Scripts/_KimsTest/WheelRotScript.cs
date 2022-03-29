using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotScript : MonoBehaviour
{
    public float speed = 5;

    private void Update()
    {
        transform.Rotate(new Vector3(speed * Time.deltaTime, 0,  0));
    }
}
