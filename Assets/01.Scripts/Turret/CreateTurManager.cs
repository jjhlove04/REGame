using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTurManager : MonoBehaviour
{
    private static CreateTurManager instance;

    public static CreateTurManager Instance { get { return instance; } }


    private ObjectPool objPool;

    public GameObject instPos;

    public bool onTurret;

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
        if (!onTurret)
        {
            if (UIManager.UI.scrapAmount >= UIManager.UI.GetNeedAmount())
            {
                TurretCreate(PlayerInput.Instance.curTurret);
                UIManager.UI.scrapAmount -= UIManager.UI.GetNeedAmount();

                onTurret = true;
                Debug.Log("포탑 설치 중");
            }
        }
    }

    public void DestroyTur(GameObject gameObject)
    {
        onTurret = false;
        gameObject.SetActive(false);
    }

    public void RepairTurret(bool onPlayer, float curHp, float maxHp)
    {
        if (onPlayer && UIManager.UI.scrapAmount >= (UIManager.UI.needAmount / 3))
        {
            UIManager.UI.scrapAmount -= (UIManager.UI.needAmount / 3);
            curHp = maxHp;
        }
    }
}
