using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaderTurret : MonoBehaviour
{
    public LayerMask mask;

    [SerializeField]
    private float maxDistance = 4;
    private void OnEnable()
    {
        foreach (var collider in Physics.OverlapSphere(transform.position, maxDistance, mask))
        {
            collider.GetComponent<Turret>().OnDetection();
        }
    }

    private void OnDisable()
    {
        foreach (var collider in Physics.OverlapSphere(transform.position, maxDistance, mask))
        {
            collider.GetComponent<Turret>().OffDetection();
        }
    }

    private void OnMouseEnter()
    {
        Transform attackRange = transform.Find("AttackRange");

        attackRange.gameObject.SetActive(true);
        attackRange.transform.localScale = new Vector3(maxDistance, maxDistance, 1);
        attackRange.GetComponentInChildren<SpriteRenderer>().color = new Color32(0, 255, 0, 30);
    }

    private void OnMouseExit()
    {
        Transform attackRange = transform.Find("AttackRange");

        attackRange.gameObject.SetActive(false);
    }
}
