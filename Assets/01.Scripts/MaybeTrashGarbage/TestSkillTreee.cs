using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSkillTreee : MonoBehaviour
{
    public List<GameObject> canUpgrade = new List<GameObject>();

    public GameObjectBool btnDic;

    private GameObject gameObj;

    private void OnEnable()
    {
        LoadData();

        btnDic = TestTurretDataBasee.Instance.postdic;
    }

    //skill버튼 첫번쨰
    public void SkillTree(string nextGameObj)
    {
        gameObj = Resources.Load<GameObject>("Turret/" + nextGameObj);
        canUpgrade.Add(gameObj);
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
        canUpgrade = TestTurretDataBasee.Instance.curTurretType;
    }

    public void reDic(GameObject item)
    {

        if (TestTurretDataBasee.Instance.postdic.Keys != null)
        {
            //SkillTreeBtn[] brn = FindObjectsOfType<SkillTreeBtn>();

            //for (int i = 0; i < brn.Length; i++)
            //{
            //    brn[i].GetComponent<Button>().interactable = TestTurretDataBase.Instance.postdic[];
            //}

            item.GetComponent<Button>().interactable = TestTurretDataBasee.Instance.postdic[item.ToString()];
        }
    }
}
