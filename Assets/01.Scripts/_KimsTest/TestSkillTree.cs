using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSkillTree : MonoBehaviour
{
    public GameObjectString canUpgrade = new GameObjectString();

    public GameObjectBool btnDic;

    private GameObject gameObj;

    private TestChangePreset presetChange;

    public GameObject baseTurret;

    private void OnEnable()
    {
        LoadData();

        btnDic = TestTurretDataBase.Instance.postdic;
    }

    private void Start()
    {
        presetChange = GetComponent<TestChangePreset>();
    }
    //skill버튼 첫번쨰
    public void SkillTree(string nextGameObj)
    {
        gameObj = Resources.Load<GameObject>("Turret/" + nextGameObj);

        string num = nextGameObj.Substring(10, 3);

        canUpgrade.Add(num, gameObj);
    }

    //clear버튼
    public void SkillTreeClear(int num)
    {
        if (canUpgrade.Count >= 1)
        {
            canUpgrade.Clear();
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

    public void LoadData()
    {
        canUpgrade = TestTurretDataBase.Instance.curTurretType;
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
        }
    }
}
