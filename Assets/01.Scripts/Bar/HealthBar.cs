using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private HealthSystem healthSystem;

    private Transform barTrm;

    bool topView;

    private void Awake()
    {
        barTrm = transform.Find("bar");

        CameraManager.Instance.TopView += TopView;
        CameraManager.Instance.QuarterView += QuarterView;
    }

    private void Start()
    {
        healthSystem.OnDamaged += CallHealthSystemOnDamaged;

        CallHealthSystemOnDamaged();
    }

    private void OnDisable()
    {
        CallHealthSystemOnDamaged();
    }

    private void Update()
    {
        if (topView)
        {
            transform.parent.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
        }

        else
        {
            transform.parent.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        }
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

    private void TopView()
    {
        topView = true;
    }

    private void QuarterView()
    {
        topView = false;
    }
}