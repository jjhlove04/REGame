using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTurManager : MonoBehaviour
{
    private static CreateTurManager instance;

    public static CreateTurManager Instance { get { return instance; } }

    private ObjectPool objPool;

    public GameObject instPos;

    public bool onTurret = false;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        objPool = FindObjectOfType<ObjectPool>();
    }

    public void TurretCreate(GameObject firePrefab)
    {
        GameObject bullet = objPool.GetObject(firePrefab);
        bullet.transform.position = instPos.transform.position;
        bullet.transform.rotation = instPos.transform.rotation;
    }

    public void createTur()
    {
        if (UIManager.UI.scrapAmount >= UIManager.UI.GetNeedAmount())
        {
            TurretCreate(PlayerInput.Instance.curTurret);

            UIManager.UI.scrapAmount -= UIManager.UI.GetNeedAmount();
            UIManager.UI.isCreate = false;
            UIManager.UI.CheckScrapAmount();

            PlayerInput.Instance.curTurret = null;
        }
    }

    public void DestroyTur(GameObject gameObject)   
    {
        gameObject.SetActive(false);
        UIManager.UI.CheckScrapAmount();
        UIManager.UI.installPanel.SetActive(true);
        UIManager.UI.openPanel = false;
    }

    public void RepairTurret()
    {
        if (UIManager.UI.scrapAmount >= (UIManager.UI.needAmount / 3))
        {
            UIManager.UI.scrapAmount -= (UIManager.UI.needAmount / 3);
            UIManager.UI.CheckScrapAmount();
        }
    }


}
