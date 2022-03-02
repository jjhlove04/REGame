using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTurret : MonoBehaviour
{
    [SerializeField]
    bool onPlayer = false;

    public InGameTurretUI turretUI;

    // Update is called once per frame
    void Update()
    {
        if(onPlayer)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                if(CreateTurManager.Instance.onTurret == true)
                {
                    Debug.Log("이미 설치된 포탑입니다");
                    UIManager.UI.installPanel.SetActive(false);
                }
                else if(CreateTurManager.Instance.onTurret == false)
                {
                    UIManager.UI.installPanel.SetActive(true);
                }

                if (PlayerInput.Instance.curTurret == null)
                {
                    
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            onPlayer = true;
            CreateTurManager.Instance.instPos = this.gameObject;
            if(turretUI.onTurret)
            {
                UIManager.UI.installPanel.SetActive(false);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Turret"))
        {
            turretUI.onTurret = true;
        }
        else
        {
            turretUI.onTurret = false;
        }
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
