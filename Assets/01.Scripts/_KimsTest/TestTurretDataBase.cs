using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTurretDataBase : MonoBehaviour
{
    private static TestTurretDataBase instance;

    public static TestTurretDataBase Instance
    {
        get { return instance; }
    }

    public List<GameObject> curTurretType = new List<GameObject>();
    public List<GameObject> curTurretType1 = new List<GameObject>();
    public List<GameObject> curTurretType2 = new List<GameObject>();
    public List<GameObject> curTurretType3 = new List<GameObject>();
    public List<GameObject> curTurretType4 = new List<GameObject>();
    public int floor, floor1, floor2, floor3, floor4;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        instance = this;
    }
    public void Upgrade(int typrTur)
    {
        switch (typrTur)
        {
            case 0:
                testScriptts.Instance.turretType[0] = curTurretType[floor];
                UpgradeFloor(floor);
                break;
            case 1:
                testScriptts.Instance.turretType[1] = curTurretType[floor1];
                UpgradeFloor(floor1);
                break;
            case 2:
                testScriptts.Instance.turretType[2] = curTurretType[floor2];
                UpgradeFloor(floor2);
                break;
            case 3:
                testScriptts.Instance.turretType[3] = curTurretType[floor3];
                UpgradeFloor(floor3);
                break;
            case 4:
                testScriptts.Instance.turretType[4] = curTurretType[floor4];
                UpgradeFloor(floor4);
                break;
            default:
                break;
        }
    }

    public void UpgradeFloor(int floor)
    {
        if(floor < curTurretType.Count)
        {
            floor++;
        }
    }
}
