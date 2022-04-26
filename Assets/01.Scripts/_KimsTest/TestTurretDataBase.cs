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

    public GameObject towerObj;

    [Header("°á°ú°ª")]
    public int resultDamage = 0;
    public int resultEXP = 0;
    public int resultGold = 0;

    public int level;
    public int tp;

    public int round;
    public int createPrice;
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
                floor++;
                if (floor < curTurretType.Count)
                {
                    testScriptts.Instance.ChageMakeTur(curTurretType[floor]);
                }
                break;
            case 1:
                floor1++;
                if (floor1 < curTurretType1.Count)
                {
                    testScriptts.Instance.ChageMakeTur(curTurretType1[floor1]);
                }
                break;
            case 2:

                floor2++;
                if (floor2 < curTurretType2.Count)
                {
                    testScriptts.Instance.ChageMakeTur(curTurretType2[floor2]);
                }
                break;
            case 3:

                floor3++;
                if (floor3 < curTurretType3.Count)
                {
                    testScriptts.Instance.ChageMakeTur(curTurretType3[floor3]);
                }
                break;
            case 4:

                floor4++;
                if (floor4 < curTurretType4.Count)
                {
                    testScriptts.Instance.ChageMakeTur(curTurretType4[floor4]);
                }
                break;
            default:
                break;
        }

        testScriptts.Instance.NextUpgrade();
    }

    public void Create(int typrTur)
    {

        switch (typrTur)
        {
            case 0:
                if (floor < curTurretType.Count)
                {
                    testScriptts.Instance.Create(curTurretType[floor]);
                }
                break;
            case 1:
                if (floor1 < curTurretType1.Count)
                {
                    testScriptts.Instance.Create(curTurretType1[floor1]);
                }
                break;
            case 2:
                if (floor2 < curTurretType2.Count)
                {
                    testScriptts.Instance.Create(curTurretType2[floor2]);
                }
                break;
            case 3:
                if (floor3 < curTurretType3.Count)
                {
                    testScriptts.Instance.Create(curTurretType3[floor3]);
                }
                break;
            case 4:
                if (floor4 < curTurretType4.Count)
                {
                    testScriptts.Instance.Create(curTurretType4[floor4]);
                }
                break;
            default:
                break;
        }
    }

    public int[] GetTurretImageCount()
    {
        int[] intArr = new int[2];
        switch (InGameUI._instance.selectType)
        {
            case 0:

                if (floor < curTurretType.Count)
                {
                    intArr[0] = curTurretType[floor].GetComponent<Turret>().turImageCount;

                    if (floor+1 < curTurretType.Count)
                    {
                        intArr[1] = curTurretType[floor + 1].GetComponent<Turret>().turImageCount;
                    }

                    else
                    {
                        intArr[1] = 100;
                    }
                }

                else
                {
                    intArr[0] = 100;
                    intArr[1] = 100;
                }

                break;

            case 1:

                if (floor1 < curTurretType1.Count)
                {
                    intArr[0] = curTurretType1[floor1].GetComponent<Turret>().turImageCount;

                    if (floor1 + 1 < curTurretType1.Count)
                    {
                        intArr[1] = curTurretType1[floor1 + 1].GetComponent<Turret>().turImageCount;
                    }

                    else
                    {
                        intArr[1] = 100;
                    }
                }

                else
                {
                    intArr[0] = 100;
                    intArr[1] = 100;
                }

                break;

            case 2:

                if (floor2 < curTurretType2.Count)
                {
                    intArr[0] = curTurretType2[floor2].GetComponent<Turret>().turImageCount;

                    if (floor2 + 1 < curTurretType2.Count)
                    {
                        intArr[1] = curTurretType2[floor2 + 1].GetComponent<Turret>().turImageCount;
                    }

                    else
                    {
                        intArr[1] = 100;
                    }
                }

                else
                {
                    intArr[0] = 100;
                    intArr[1] = 100;
                }

                break;

            case 3:

                if (floor3 < curTurretType3.Count)
                {
                    intArr[0] = curTurretType3[floor3].GetComponent<Turret>().turImageCount;

                    if (floor3 + 1 < curTurretType3.Count)
                    {
                        intArr[1] = curTurretType3[floor3 + 1].GetComponent<Turret>().turImageCount;
                    }

                    else
                    {
                        intArr[1] = 100;
                    }
                }

                else
                {
                    intArr[0] = 100;
                    intArr[1] = 100;
                }

                break;

            case 4:

                if (floor4 < curTurretType4.Count)
                {
                    intArr[0] = curTurretType4[floor4].GetComponent<Turret>().turImageCount;

                    if (floor4 + 1 < curTurretType4.Count)
                    {
                        intArr[1] = curTurretType4[floor4 + 1].GetComponent<Turret>().turImageCount;
                    }

                    else
                    {
                        intArr[1] = 100;
                    }
                }

                else
                {
                    intArr[0] = 100;
                    intArr[1] = 100;
                }
                break;
                /*case 5:

                    if (floor4 < curTurretType4.Count)
                    {
                        testScriptts.Instance.ChageMakeTur(curTurretType5[floor5]);
                        floor5++;
                    }

                    break;*/
        }

        return intArr;
    }
}
