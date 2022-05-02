using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaderTurret : MonoBehaviour
{
    public LayerMask mask;
    private void OnEnable()
    {
        foreach (var collider in Physics.OverlapSphere(transform.position, 4, mask))
        {
            collider.GetComponent<Turret>().OnDetection();
        }
    }

    private void OnDisable()
    {
        foreach (var collider in Physics.OverlapSphere(transform.position, 4, mask))
        {
            collider.GetComponent<Turret>().OffDetection();
        }
    }
}
