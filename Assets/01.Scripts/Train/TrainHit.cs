using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainHit : MonoBehaviour
{
    private Renderer[] newRenderer;
    
    private List<Material[]> material = new List<Material[]>();       
    [SerializeField]
    private Material hitMaterial;
    private Material[] hitMaterials;

    private bool startCotoutine = false;

    private void Start()
    {
        newRenderer = GetComponentsInChildren<Renderer>();

        for (int i = 0; i < newRenderer.Length; i++)
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
        if (!startCotoutine)
        {
            StartCoroutine(HitMaterial());
            startCotoutine = true;
        }
    }

    IEnumerator HitMaterial()
    {
        for (int i = 0; i < newRenderer.Length; i++)
        {
            newRenderer[i].materials = hitMaterials;
        }

        yield return new WaitForSeconds(0.1f);

        startCotoutine = false;

        for (int i = 0; i < newRenderer.Length; i++)
        {
            newRenderer[i].materials = material[i];
        }

    }

}
