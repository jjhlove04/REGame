using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSkillTree : MonoBehaviour
{
    public List<GameObject> canUpgrade = new List<GameObject>();


    public void SkillTree(string nextGameObj)
    {
        canUpgrade.Add(Resources.Load<GameObject>(nextGameObj));
    }

    public void DisableSkillLine()
    {

    }

    public void SkillTreeClear()
    {
        canUpgrade.Clear();
    }

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
