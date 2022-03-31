using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSkillTree : MonoBehaviour
{   
    public List<GameObject> canUpgrade = new List<GameObject>();
    public List<GameObject> canUpgrade1 = new List<GameObject>();
    public List<GameObject> canUpgrade2 = new List<GameObject>();
    public List<GameObject> canUpgrade3 = new List<GameObject>();
    public List<GameObject> canUpgrade4 = new List<GameObject>();

    public List<bool> btnList = new List<bool>();

    private GameObject gameObj;

    private TestChangePreset presetChange;

    private void Start()
    {
        presetChange = GetComponent<TestChangePreset>();
    }
    //skill버튼 첫번쨰
    public void SkillTree(string nextGameObj)
    {
        gameObj = Resources.Load<GameObject>("Turret/" + nextGameObj);
    }
    public void SkillTreeNum(int num)
    {
        switch (num)
        {
            case 0:
                canUpgrade.Add(gameObj);
                break;
            case 1:
                canUpgrade1.Add(gameObj);
                break;
            case 2:
                canUpgrade2.Add(gameObj);
                break;
            case 3:
                canUpgrade3.Add(gameObj);
                break;
            case 4:
                canUpgrade4.Add(gameObj);
                break;
            default:
                break;
        }
    }


    //clear버튼
    public void SkillTreeClear(int num)
    {
        switch (num)
        {
            case 0:

                if (canUpgrade.Count >= 1)
                {
                    canUpgrade.Clear();
                }
                break;
            case 1:
                if (canUpgrade1.Count >= 1)
                {
                    canUpgrade1.Clear();
                }
                break;
            case 2:
                if (canUpgrade2.Count >= 1)
                {
                    canUpgrade2.Clear();
                }
                break;
            case 3:
                if (canUpgrade3.Count >= 1)
                {
                    canUpgrade3.Clear();
                }
                break;
            case 4:
                if (canUpgrade4.Count >= 1)
                {
                    canUpgrade4.Clear();
                }
                break;
            default:
                break;
        }


        Button[] btn = presetChange.presetCanvas[num].GetComponentsInChildren<Button>();
        for (int i = 0; i < btn.Length; i++)
        {
            if (btn[i].GetComponent<SkillTreeBtn>())
            {
                btn[i].interactable = false;
                if (btn[i].GetComponent<SkillTreeBtn>().myCount == 1)
                {
                    btn[i].interactable = true;
                }
            }
        }

    }

    //apply버튼
    public void postData(int num)
    {
        switch (num)
        {
            case 0:
                TestTurretDataBase.Instance.curTurretType = canUpgrade;
                break;
            case 1:
                TestTurretDataBase.Instance.curTurretType1 = canUpgrade1;
                break;
            case 2:
                TestTurretDataBase.Instance.curTurretType2 = canUpgrade2;
                break;
            case 3:
                TestTurretDataBase.Instance.curTurretType3 = canUpgrade3;
                break;
            case 4:
                TestTurretDataBase.Instance.curTurretType4 = canUpgrade4;
                break;
            default:
                break;
        }
    }

}
