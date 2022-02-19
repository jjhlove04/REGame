using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTurret : MonoBehaviour
{
    [SerializeField]
    bool isTurret = false;

    private ObjectPool objPool;

    private int onTurrectCount;
    void Start()
    {
        objPool = FindObjectOfType<ObjectPool>();
    }

    public InGameTurretUI turretUI;

    // Update is called once per frame
    void Update()
    {
        if(isTurret)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                if(onTurrectCount > 0)
                {
                    Debug.Log("이미 설치된 포탑입니다");
                    UIManager.UI.installPanel.SetActive(false);
                }

                if(onTurrectCount <= 0)
                {
                    UIManager.UI.installPanel.SetActive(true);

                }

                if (PlayerInput.Instance.curTurret == null)
                {
                    Debug.Log("test");
                }
            }

            if(Input.GetMouseButtonDown(1))
            {
                if(onTurrectCount <= 0)
                {
                    createTur();
                }
            }
        }
    }

    public void TurretCreate(GameObject firePrefab)
    {
        GameObject bullet = objPool.GetObject(firePrefab);
        bullet.transform.position = this.transform.position;
        bullet.transform.rotation = this.transform.rotation;
    }

    public void createTur()
    {
        if (UIManager.UI.scrapAmount >= UIManager.UI.GetNeedAmount())
        {
            TurretCreate(PlayerInput.Instance.curTurret);
            turretUI.onTurret = true;
            onTurrectCount++;
            Debug.Log("포탑 설치 중");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isTurret = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isTurret = false;
        }
    }
}
