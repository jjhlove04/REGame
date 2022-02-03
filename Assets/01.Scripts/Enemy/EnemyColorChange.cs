using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColorChange : MonoBehaviour
{
    [Header("targeting effect")]
    public Material curMaterial;
    private Renderer newRenderer;

    private void Start()
    {
        newRenderer = GetComponent<Renderer>();
    }

    private void OnMouseEnter()
    {
        Debug.Log("target");
        newRenderer.material.color = Color.white;
        PlayerInput.Instance.isEnemy = true;
    }

    private void OnMouseExit()
    {
        newRenderer.material.color = curMaterial.color;
        PlayerInput.Instance.isEnemy = false;
    }
}
