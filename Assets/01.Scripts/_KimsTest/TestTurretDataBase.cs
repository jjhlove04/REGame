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

    public List<bool> btnBool = new List<bool>();
    public List<bool> btnBool1 = new List<bool>();
    public List<bool> btnBool2 = new List<bool>();
    public List<bool> btnBool3 = new List<bool>();
    public List<bool> btnBool4 = new List<bool>();

    public int floor, floor1, floor2, floor3, floor4;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        
    }
    public void Upgrade(int typrTur)
    {
        switch (typrTur)
        {
            case 0:
                testScriptts.Instance.ChageMakeTur(curTurretType[floor]);
                if (floor < curTurretType.Count - 1)
                {
                    floor++;
                }
                break;
            case 1:
                testScriptts.Instance.ChageMakeTur(curTurretType1[floor1]);
                if (floor1 < curTurretType1.Count - 1)
                {
                    floor1++;
                }
                break;
            case 2:
                testScriptts.Instance.ChageMakeTur(curTurretType2[floor2]);
                if (floor2 < curTurretType2.Count - 1)
                {
                    floor2++;
                }
                break;
            case 3:
                testScriptts.Instance.ChageMakeTur(curTurretType3[floor3]);
                if (floor3 < curTurretType3.Count - 1)
                {
                    floor3++;
                }
                break;
            case 4:
                testScriptts.Instance.ChageMakeTur(curTurretType4[floor4]);
                if (floor4 < curTurretType4.Count - 1)
                {
                    floor4++;
                }
                break;
            default:
                break;
        }
    }
}
