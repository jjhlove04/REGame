using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSkillTree : MonoBehaviour
{   
    public List<GameObject> canUpgrade = new List<GameObject>();

    //skill��ư ù����
    public void SkillTree(string nextGameObj)
    {
        canUpgrade.Add(Resources.Load<GameObject>("Turret/" + nextGameObj));
    }
    //skill��ư �ι���
    public void DisableSkillLine(Button btn/*�ڱ� �ڽ�*/)
    {
        btn.interactable = false;
        btn.GetComponent<Image>().color = Color.red;
    }

    //clear��ư
    public void SkillTreeClear()
    {
        if (canUpgrade.Count >= 1)
        {
            Button[] btn = FindObjectsOfType<Button>();
             
            for (int i = 0; i < btn.Length; i++)
            {
                btn[i].interactable = true;
                if (btn[i].GetComponent<Image>())
                {
                    btn[i].GetComponent<Image>().color = Color.white;
                }
            }

            canUpgrade.Clear();

        }
    }

    //apply��ư
    public void postData(int num)
    {
        switch (num)
        {
            case 0:
                TestTurretDataBase.Instance.curTurretType = canUpgrade.ToList();
                break;
            case 1:
                TestTurretDataBase.Instance.curTurretType1 = canUpgrade.ToList();
                break;
            case 2:
                TestTurretDataBase.Instance.curTurretType2 = canUpgrade.ToList();
                break;
            case 3:
                TestTurretDataBase.Instance.curTurretType3 = canUpgrade.ToList();
                break;
            case 4:
                TestTurretDataBase.Instance.curTurretType4 = canUpgrade.ToList();
                break;
            default:
                break;
        }
    }

}
