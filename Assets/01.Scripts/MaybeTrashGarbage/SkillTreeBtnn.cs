using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeBtnn : MonoBehaviour
{
    public int myCount;

    private bool isUpgrade = false;

    public Button nextBtn;
    public Button[] nextLineBtn;
    public GameObject floor;

    TestSkillTreee testsk;
    private void OnEnable()
    {
        testsk = FindObjectOfType<TestSkillTreee>();
    }

    private void Start()
    {
        this.gameObject.TryGetComponent(out Button btn);
        btn.onClick.AddListener(() =>
        {
            if (floor != null)
            {
                Button[] str = floor.GetComponentsInChildren<Button>();

                foreach (Button item in str)
                {
                    item.interactable = false; 
                    isUpgrade = false;
                }

                for (int i = 0; i < str.Length; i++)
                {
                    testsk.btnDic[str[i].transform.parent.gameObject.ToString()] = false;
                }
            }

            btn.interactable = false;
            isUpgrade = true;

            if (testsk.btnDic[this.transform.parent.gameObject.ToString()] == true)
            {
                testsk.btnDic[this.transform.parent.gameObject.ToString()] = false;
            }
        });

        if (!testsk.btnDic.ContainsKey(this.transform.parent.gameObject.ToString()))
        {
            if (myCount == 1)
            {
                this.gameObject.GetComponent<Button>().interactable = true;

                testsk.btnDic.Add(this.transform.parent.gameObject.ToString(), this.transform.GetComponent<Button>().interactable);
            }
            else
            {
                this.gameObject.GetComponent<Button>().interactable = false;

                testsk.btnDic.Add(this.transform.parent.gameObject.ToString(), this.transform.GetComponent<Button>().interactable);
            }
        }

        testsk.reDic(this.transform.parent.gameObject);
    }

    private void Update()
    {
        if (isUpgrade)
        {
            if (nextBtn == null)
            {
                return;
            }

            nextBtn.interactable = true;
            if (testsk.btnDic[nextBtn.transform.parent.gameObject.ToString()] == false)
            {
                testsk.btnDic[nextBtn.transform.parent.gameObject.ToString()] = true;
            }

            if (nextLineBtn.Length > 0)
            {
                for (int i = 0; i < nextLineBtn.Length; i++)
                {
                    nextLineBtn[i].interactable = true;
                    if (testsk.btnDic[nextLineBtn[i].transform.parent.gameObject.ToString()] == false)
                    {
                        testsk.btnDic[nextLineBtn[i].transform.parent.gameObject.ToString()] = true;
                    }
                }
            }

            isUpgrade = false;
        }
    }
}
