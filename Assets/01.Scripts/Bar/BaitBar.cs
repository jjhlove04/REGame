using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitBar : MonoBehaviour
{
    [SerializeField]
    private BaitHealthSystem baitHealthSystem;

    private Transform barTrm;

    private void Awake()
    {
        barTrm = transform.Find("bar");
    }

    private void Start()
    {
        baitHealthSystem.OnDamaged += CallHealthSystemOnDamaged;
        baitHealthSystem.OnDied.AddListener(CallHealthSystemOnDamaged);

        CallHealthSystemOnDamaged();
    }

    private void Update()
    {
        transform.parent.LookAt(Camera.main.transform);
    }

    void CallHealthSystemOnDamaged()
    {
        UpdateBar();
        UpdateHealthBarVisible();
    }

    void UpdateBar()
    {
        barTrm.localScale = new Vector3(baitHealthSystem.GetHealthAmounetNomalized(), 1, 1);
    }

    void UpdateHealthBarVisible()
    {
        if (baitHealthSystem.IsFullHealth())
        {
            gameObject.SetActive(false);
        }

        else
        {
            gameObject.SetActive(true);
        }
    }
}
