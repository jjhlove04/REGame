using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeBtn : MonoBehaviour
{
    public int myCount;

    private bool isUpgrade = false;

    public Button nextBtn;
    public Button nextLineBtn;
    public GameObject floor;

    TestSkillTree testsk;
    private void OnEnable()
    {
        testsk = FindObjectOfType<TestSkillTree>();

    }

    private void Start()
    {
        this.gameObject.TryGetComponent(out Button btn);
        btn.onClick.AddListener(() =>
        {
            if(floor != null)
            {
                Button[] str = floor.GetComponentsInChildren<Button>();

                foreach (Button item in str)
                {
                    item.interactable = false;
                    isUpgrade = false;
                }

                for (int i = 0; i < str.Length; i++)
                {
                    testsk.btnDic[str[i].gameObject.ToString()] = false;
                }
            }
            this.gameObject.TryGetComponent(out Button obtn);
            obtn.interactable = false;
            isUpgrade = true;


            if (testsk.btnDic[this.gameObject.ToString()] == true)
            {
                testsk.btnDic[this.gameObject.ToString()] = false;
            }
        });

        if (!testsk.btnDic.ContainsKey(this.gameObject.ToString()))
        {
            if (myCount == 1)
            {
                this.gameObject.GetComponent<Button>().interactable = true;

                testsk.btnDic.Add(this.gameObject.ToString(), this.gameObject.GetComponent<Button>().interactable);
            }
            else
            {
                this.gameObject.GetComponent<Button>().interactable = false;

                testsk.btnDic.Add(this.gameObject.ToString(), this.gameObject.GetComponent<Button>().interactable);
            }
        }

        testsk.reDic(this.gameObject);
    }

    private void Update()
    {
        if(isUpgrade)
        {
            if(nextBtn == null)
            {
                return;
            }

            nextBtn.interactable = true;
            if (testsk.btnDic[nextBtn.gameObject.ToString()] == false)
            {
                testsk.btnDic[nextBtn.gameObject.ToString()] = true;
            }

            if (nextLineBtn != null)
            {
                nextLineBtn.interactable = true;
                if (testsk.btnDic[nextLineBtn.gameObject.ToString()] == false)
                {
                    testsk.btnDic[nextLineBtn.gameObject.ToString()] = true;
                }
            }
            isUpgrade = false;


        }
    }
}
