using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSkillTreee : MonoBehaviour
{
    public GameObjectString canUpgrade = new GameObjectString();

    public GameObjectBool btnDic;

    private GameObject gameObj;

    private void OnEnable()
    {
        LoadData();

        btnDic = TestTurretDataBase.Instance.postdic;
    }

    //skill버튼 첫번쨰
    public void SkillTree(string nextGameObj)
    {
        gameObj = Resources.Load<GameObject>("Turret/" + nextGameObj);

        string num = nextGameObj.Substring(10, 3);

        canUpgrade.Add(num, gameObj);
    }

    //clear버튼
    public void SkillTreeClear()
    {
        if (canUpgrade.Count >= 1)
        {
            canUpgrade.Clear();
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

            item.transform.Find("TurretBtn").GetComponent<Button>().interactable = TestTurretDataBase.Instance.postdic[item.ToString()];
        }
    }
}
