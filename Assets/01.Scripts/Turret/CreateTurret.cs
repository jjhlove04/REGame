using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTurret : MonoBehaviour
{
    [SerializeField]
    bool isTurret = false;

    private ObjectPool objPool;
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
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (PlayerInput.Instance.curTurret != null)
                {
                    TurretCreate(PlayerInput.Instance.curTurret);
                    turretUI.onTurret = true;
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
