using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GameObjectBool : SerializableDictionary<string, bool> { };
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

    public GameObjectBool postdic = new GameObjectBool();

    public int floor, floor1, floor2, floor3, floor4;

    [Header("°á°ú°ª")]
    public int resultDamage = 0;
    public int resultEXP = 0;
    public int resultGold = 0;
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
    public void Upgrade(int typrTur)
    {

        switch (typrTur)
        {
            case 0:                
                if (floor < curTurretType.Count)
                {
                    testScriptts.Instance.ChageMakeTur(curTurretType[floor]);
                    floor++;
                }
                break;
            case 1:
                if (floor1 < curTurretType1.Count)
                {
                    testScriptts.Instance.ChageMakeTur(curTurretType1[floor1]);
                    floor1++;
                }
                break;
            case 2:
                if (floor2 < curTurretType2.Count)
                {
                    testScriptts.Instance.ChageMakeTur(curTurretType2[floor2]);
                    floor2++;
                }
                break;
            case 3:
                if (floor3 < curTurretType3.Count)
                {
                    testScriptts.Instance.ChageMakeTur(curTurretType3[floor3]);
                    floor3++;
                }
                break;
            case 4:
                if (floor4 < curTurretType4.Count)
                {
                    testScriptts.Instance.ChageMakeTur(curTurretType4[floor4]);
                    floor4++;
                }
                break;
            default:
                break;
        }
    }

    void UpgradeCoast()
    {

    }

}
