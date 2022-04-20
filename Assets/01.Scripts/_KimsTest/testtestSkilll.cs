using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testtestSkilll : MonoBehaviour
{
    private GameObject upgradeBtn;

    [SerializeField]
    private int count;
    [SerializeField]
    private int selectType = -1;
    [SerializeField]
    private int floor;

    [HideInInspector]
    public bool isTest;

    private Image outLine;

    void Start()
    {
        upgradeBtn = GameObject.Find("UP");
        outLine = transform.GetChild(0).GetComponent<Image>();

        this.gameObject.TryGetComponent(out Button btnm);

        //btnm.onClick.AddListener(FuckYou);

        upgradeBtn.GetComponent<Button>().onClick.AddListener(FuckYouKim);
    }

    public void FuckYou()
    {
        {
            testSkilll[] tskill = FindObjectsOfType<testSkilll>();

            for (int i = 0; i < tskill.Length; i++)
            {
                if (tskill[i] != this.gameObject)
                {
                    tskill[i].isTest = false;
                }
            }

            testScriptts.Instance.turPos = count;

            if (selectType == -1 && GameManager.Instance.goldAmount >= 10)
            {
                selectType = InGameUI._instance.selectType;
                testScriptts.Instance.turType = selectType;
                outLine.color = new Color32(255, 204, 1, 255);
            }
            else
            {
                testScriptts.Instance.turType = selectType;
            }

            switch (selectType)
            {
                case 0:
                    TestTurretDataBase.Instance.floor = floor;
                    break;
                case 1:
                    TestTurretDataBase.Instance.floor1 = floor;
                    break;
                case 2:
                    TestTurretDataBase.Instance.floor2 = floor;
                    break;
                case 3:
                    TestTurretDataBase.Instance.floor3 = floor;
                    break;
                case 4:
                    TestTurretDataBase.Instance.floor4 = floor;
                    break;
                default:
                    break;
            }
            TestTurretDataBase.Instance.Create(selectType);
            isTest = true;
        }
    }

    public void FuckYouKim()
    {
        if (isTest)
        {
            switch (InGameUI._instance.selectType)
            {
                case 0:
                    floor = TestTurretDataBase.Instance.floor;
                    break;
                case 1:
                    floor = TestTurretDataBase.Instance.floor1;
                    break;
                case 2:
                    floor = TestTurretDataBase.Instance.floor2;
                    break;
                case 3:
                    floor = TestTurretDataBase.Instance.floor3;
                    break;
                case 4:
                    floor = TestTurretDataBase.Instance.floor4;
                    break;
                default:
                    break;
            }
        }
    }
}