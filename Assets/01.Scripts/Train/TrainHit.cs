using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainHit : MonoBehaviour
{
    [SerializeField]
    private List<Renderer> newRenderer;
    
    private List<Material[]> material = new List<Material[]>();       
    [SerializeField]
    private Material hitMaterial;
    private Material[] hitMaterials;

    private bool isPlayCoroutine =false;

    private void Start()
    {
        for (int i = 0; i < newRenderer.Count; i++)
        {
            material.Add(newRenderer[i].materials);
        }

        hitMaterials = new Material[material[0].Length];
        for (int i = 0; i < material[0].Length; i++)
        {
            hitMaterials[i] = hitMaterial;
        }
    }
    public void Hit()
    {
        if (!isPlayCoroutine)
        {
            StopCoroutine(HitMaterial());
            StartCoroutine(HitMaterial());
        }
    }

    IEnumerator HitMaterial()
    {
        isPlayCoroutine = true;
        for (int i = 0; i < newRenderer.Count; i++)
        {
            newRenderer[i].materials = hitMaterials;
        }

        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < newRenderer.Count; i++)
        {
            newRenderer[i].materials = material[i];
        }
        isPlayCoroutine = false;

    }

}
