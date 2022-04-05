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

    public GameObjectBool btnDic;

    private GameObject gameObj;

    private TestChangePreset presetChange;

    private void OnEnable()
    {
        LoadData();

        btnDic = TestTurretDataBase.Instance.postdic;
    }

    private void Start()
    {
        presetChange = GetComponent<TestChangePreset>();
    }
    //skill¹öÆ° Ã¹¹ø¤Š
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

    //clear¹öÆ°
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

                btnDic[btn[i].gameObject.ToString()] = false;

                if (btn[i].GetComponent<SkillTreeBtn>().myCount == 1)
                {
                    btn[i].interactable = true;
                    btnDic[btn[i].gameObject.ToString()] = true;
                }
            }

        }


    }

    //apply¹öÆ°
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

    public void LoadData()
    {
        canUpgrade = TestTurretDataBase.Instance.curTurretType;
        canUpgrade1 = TestTurretDataBase.Instance.curTurretType1;
        canUpgrade2 = TestTurretDataBase.Instance.curTurretType2;
        canUpgrade3 = TestTurretDataBase.Instance.curTurretType3;
        canUpgrade4 = TestTurretDataBase.Instance.curTurretType4;
    }

    public void reDic(GameObject item)
    {

        if (TestTurretDataBase.Instance.postdic.Keys != null)
        {
            //SkillTreeBtn[] brn = FindObjectsOfType<SkillTreeBtn>();

            //for (int i = 0; i < brn.Length; i++)
            //{
            //    brn[i].GetComponent<Button>().interactable = TestTurretDataBase.Instance.postdic[];
            //}
            
            item.GetComponent<Button>().interactable = TestTurretDataBase.Instance.postdic[item.ToString()];

            Debug.Log("µñ¼Å³Ê¸® ¹Ù²Þ");
        }
    }
}
