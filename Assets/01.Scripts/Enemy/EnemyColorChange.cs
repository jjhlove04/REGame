using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class EnemyColorChange : MonoBehaviour
{
    [Header("targeting effect")]
    public Material hitMaterial;
    public Material stealthMaterial = null;
    private List<Material> basicMaterial = new List<Material>();

    [SerializeField]
    private Renderer[] newRenderer;

    private Enemy enemy;

    private void Awake()
    {
        enemy = transform.root.GetComponent<Enemy>();
    }

    private void Start()
    {
        if (enemy != null &&enemy.IsStealth())
        {
            OnStealth();
        }

        for (int i = 0; i < newRenderer.Length; i++)
        {
            basicMaterial.Add(newRenderer[i].material);
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < newRenderer.Length; i++)
        {
            newRenderer[i].material = basicMaterial[i];
        }
    }

    public void Hit()
    {
        StopCoroutine(HitMaterial());
        Timing.RunCoroutine(HitMaterial());
    }

    IEnumerator<float> HitMaterial()
    {
        for (int i = 0; i < newRenderer.Length; i++)
        {
            newRenderer[i].material = hitMaterial;
        }

        yield return (Timing.WaitForSeconds(0.15f));

        for (int i = 0; i < newRenderer.Length; i++)
        {
            newRenderer[i].material = basicMaterial[i];
        }
    }

    private void OnStealth()
    {
        for (int i = 0; i < newRenderer.Length; i++)
        {
            newRenderer[i].material = stealthMaterial;
        }
    }
}