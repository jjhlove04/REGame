using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillTreeBtnn : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public string turretGameObj;
    private GameObject gameObj;

    public int myCount;

    private bool isUpgrade = false;

    public Button nextBtn;
    public Button[] nextLineBtn;
    public GameObject floor;

    TestSkillTreee testsk;
    TitleUI titleUI;

    private bool isClick;

    private float clickTime;
    //[Header("포탑 정보")]
    //public float 
    private void OnEnable()
    {
        testsk = FindObjectOfType<TestSkillTreee>();
    }

    private void Start()
    {
        titleUI = TitleUI.UI;
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
        if(isClick)
        {
            Vector3 mousPos = Input.mousePosition;
            clickTime += Time.deltaTime;

            titleUI.upgradeBar.fillAmount = clickTime / 2;
            titleUI.upgradeBar.transform.position = mousPos;
        }
        else
        {
            clickTime = 0;
        }

        if(clickTime >= 2f)
        {
            clickBtn();
            titleUI.upgradeBar.gameObject.SetActive(false);
            isClick = false;
            clickTime = 0;

        }

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
    private void clickBtn()
    {
        TryGetComponent(out Button btn);

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

        gameObj = Resources.Load<GameObject>("Turret/" + turretGameObj);

        string num = turretGameObj.Substring(10, 3);

        testsk.canUpgrade.Add(num, gameObj);

    }
    

    public void OnPointerUp(PointerEventData eventData)
    {
        titleUI.upgradeBar.gameObject.SetActive(false);
        isClick = false;
        clickTime = 0;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        TitleUI.UI.explainTurretImage.sprite = this.gameObject.GetComponent<Image>().sprite;
        if (gameObject.TryGetComponent(out Button btn))
        {
            if(btn.interactable == true)
            {
                titleUI.upgradeBar.gameObject.SetActive(true);
                isClick = true;
            }
        }

    }
}
