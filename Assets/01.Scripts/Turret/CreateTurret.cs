using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTurret : MonoBehaviour
{
    [SerializeField]
    bool onPlayer = false;

    public MeshRenderer mesh;

    [SerializeField]
    private bool onTurret;
    //public InGameTurretUI turretUI;

    // Update is called once per frame
    void Update()
    {
        if(onPlayer)
        {
            if (PlayerInput.Instance.curTurret == null)
            {

            }
        }

        TargetPos();
    }

    private void TargetPos()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!UIManager.UI.openPanel && UIManager.UI.isCreate)
        {
            mesh.enabled = true;
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    if (hit.transform != null)
                    {
                        if (hit.transform.gameObject.GetComponent<CreateTurret>())
                        {
                            CreateTurManager.Instance.instPos = hit.transform.gameObject;
                            CreateTurManager.Instance.createTur();
                            if (onTurret)
                            {
                                hit.transform.gameObject.GetComponent<BoxCollider>().enabled = false;
                            }
                            else
                            {
                                hit.transform.gameObject.GetComponent<BoxCollider>().enabled = true;
                            }
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

                        UIManager.UI.destroyBtn.onClick.AddListener(() =>
                        {
                            CreateTurManager.Instance.DestroyTur(hit.transform.gameObject);
                            onTurret = false;
                        });
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            onPlayer = true;
            
            //if(turretUI.onTurret)
            //{
            //    UIManager.UI.installPanel.SetActive(false);
            //}
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //if(other.gameObject.CompareTag("Turret"))
        //{
        //    turretUI.onTurret = true;
        //}
        //else
        //{
        //    turretUI.onTurret = false;
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            onPlayer = false;
            CreateTurManager.Instance.onTurret = false;
            UIManager.UI.installPanel.SetActive(true);
        }
    }
}
