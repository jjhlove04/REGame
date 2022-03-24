using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColorChange : MonoBehaviour
{
    [Header("targeting effect")]
    public Material hitMaterial;
    public Material curMaterial;
    public Material basicMaterial;
    private Renderer newRenderer;

    private void Awake()
    {
        newRenderer = GetComponent<Renderer>();
    }
    private void OnEnable()
    {
        newRenderer.material.color = basicMaterial.color;
    }


    public void Hit()
    {
        StopCoroutine(HitMaterial());
        StartCoroutine(HitMaterial());
    }

    IEnumerator HitMaterial()
    {
        newRenderer.material.color = hitMaterial.color;

        yield return (new WaitForSeconds(0.15f));

        newRenderer.material.color = basicMaterial.color;
    }
}
