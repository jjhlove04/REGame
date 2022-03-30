using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSkillTree : MonoBehaviour
{   
    public List<GameObject> canUpgrade = new List<GameObject>();

    public List<bool> btnList = new List<bool>();

    private void Start()
    {
        if (canUpgrade != null)
        {
            Debug.Log("비움");
            canUpgrade.Clear();
        }

        
    }
    //skill버튼 첫번쨰
    public void SkillTree(string nextGameObj)
    {
        canUpgrade.Add(Resources.Load<GameObject>("Turret/" + nextGameObj));
    }


    //clear버튼
    public void SkillTreeClear()
    {
        if (canUpgrade.Count >= 1)
        {
            SkillTreeBtn[] btn = FindObjectsOfType<SkillTreeBtn>();
             
            for (int i = 0; i < btn.Length; i++)
            {
                btn[i].GetComponent<Button>().interactable = false;
                if (btn[i].myCount == 1)
                {
                    btn[i].GetComponent<Button>().interactable = true;
                }
            }

            canUpgrade.Clear();
        }
    }

    public void LoadSkillTree(List<GameObject> list)
    {
        canUpgrade = list;
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
                TestTurretDataBase.Instance.curTurretType1 = canUpgrade;
                break;
            case 2:
                TestTurretDataBase.Instance.curTurretType2 = canUpgrade;
                break;
            case 3:
                TestTurretDataBase.Instance.curTurretType3 = canUpgrade;
                break;
            case 4:
                TestTurretDataBase.Instance.curTurretType4 = canUpgrade;
                break;
            default:
                break;
        }
    }

}
