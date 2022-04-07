using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

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
        Timing.RunCoroutine(HitMaterial());
    }

    IEnumerator<float> HitMaterial()
    {
        newRenderer.material.color = hitMaterial.color;

        yield return (Timing.WaitForSeconds(0.15f));

        newRenderer.material.color = basicMaterial.color;
    }
}
