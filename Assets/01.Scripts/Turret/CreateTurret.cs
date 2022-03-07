using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTurret : MonoBehaviour
{
    public MeshRenderer mesh;

    public bool onTurret = false;

    void Update()
    {
        TargetPos();
    }

    private void TargetPos()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!UIManager.UI.openPanel && UIManager.UI.isCreate)
        {
            mesh.enabled = true;
            if (Input.GetMouseButtonDown(0) && onTurret == false)
            {
                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    if (hit.transform != null)
                    {
                        if (hit.transform.gameObject.GetComponent<CreateTurret>() && hit.transform.gameObject.GetComponent<CreateTurret>().onTurret == false)
                        {
                            CreateTurManager.Instance.instPos = hit.transform.gameObject;
                            CreateTurManager.Instance.createTur();
                        }
                    }
                }
            }
        }
        else
        {
            mesh.enabled = false;
            CreateTurManager.Instance.instPos = null;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform != null)
                {
                    if (hit.transform.gameObject.GetComponent<TurretShooting>())
                    {
                        UIManager.UI.installPanel.SetActive(false);
                        UIManager.UI.openPanel = true;
                        UIManager.UI.upgradeCostTxt.text = hit.transform.gameObject.GetComponent<TurretShooting>().upgradeCost.ToString();

                        UIManager.UI.destroyBtn.onClick.AddListener(() =>
                        {
                            CreateTurManager.Instance.DestroyTur(hit.transform.gameObject);
                        });

                        UIManager.UI.repairBtn.onClick.AddListener(() =>
                        {
                            CreateTurManager.Instance.RepairTurret();
                            hit.transform.gameObject.GetComponent<HealthSystem>().InitHealth();
                        });
                    }
                    if (hit.transform.gameObject.GetComponent<CreateTurret>())
                    {
                        if (hit.transform.gameObject.GetComponent<CreateTurret>().onTurret)
                        {
                            UIManager.UI.destroyBtn.onClick.AddListener(() =>
                            {
                                hit.transform.gameObject.GetComponent<CreateTurret>().onTurret = false;
                            });
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Turret"))
        {
            onTurret = true;
        }
    }
}
