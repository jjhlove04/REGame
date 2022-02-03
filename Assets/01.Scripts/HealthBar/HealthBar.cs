using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private HealthSystem healthSystem;

    private Transform barTrm;

    private void Awake()
    {
        barTrm = transform.Find("bar");
    }

    private void Start()
    {
        healthSystem.OnDamaged += CallHealthSystemOnDamaged;
        healthSystem.OnDied += CallHealthSystemOnDamaged;

        CallHealthSystemOnDamaged();
    }

    void CallHealthSystemOnDamaged()
    {
        UpdateBar();
        UpdateHealthBarVisible();
    }

    void UpdateBar()
    {
        barTrm.localScale = new Vector3(healthSystem.GetHealthAmounetNomalized(), 1, 1);
    }

    void UpdateHealthBarVisible()
    {
        if (healthSystem.IsFullHealth())
        {
            gameObject.SetActive(false);
        }

        else
        {
            gameObject.SetActive(true);
        }
    }
}