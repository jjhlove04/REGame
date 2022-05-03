using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBullet : MonoBehaviour
{
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += Vector3.back * 20 * Time.deltaTime;
    }
}
