using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InGameUI : MonoBehaviour
{
    public static InGameUI _instance = new InGameUI();
    [SerializeField] private GameObject bluePrintTop;
    [SerializeField] private GameObject bluePrintBot;
    public static int sceneIndex = 0;

    int index = 1;
    int num = 1;
    int backIndex = 1;
    [SerializeField] private GameObject[] menuBtn;
    [SerializeField] private RectTransform[] menuBtnRect ;
    private List<RectTransform> menuBtnList = new List<RectTransform>();
    [SerializeField] private Button mainMenuBtn;
    [SerializeField] private GameObject btnGroup;
    [SerializeField] private GameObject upGradePanel;
    [SerializeField] private GameObject stopPanel;
    [SerializeField] private  RectTransform stopPanelRect;
    [HideInInspector] public RectTransform upGradePanelRect;

    [SerializeField] private Button upGradePanelBackBtn;
    [SerializeField] private Button[] applyBtn;
    [SerializeField] private GameObject[] selectObj;
    int screenHeight = Screen.height;
    int screenWidth = Screen.width;
    int applybtnIndex = 0;

    RectTransform bpTop;
    RectTransform bpBot;

    public Text goldAmounTxt;
    public Text waveTxt;

    [SerializeField]
    private Button upGradeBtn;
    public Text warningTxt;
    public int selectType;
    private void Awake()
    {
        stopPanel.transform.localScale = Vector2.zero;
        _instance = this;
        bpTop = bluePrintTop.GetComponent<RectTransform>();
        bpBot = bluePrintBot.GetComponent<RectTransform>();
        upGradePanelRect = upGradePanel.GetComponent<RectTransform>();
        for(int i = 0; i < 4; i++)
        {
            menuBtnRect[i] = menuBtn[i].GetComponent<RectTransform>();
        }
        upGradePanelBackBtn.onClick.AddListener(() => 
        {
            upGradePanelRect.DOAnchorPosY(780, 1.5f);
        });
        upGradeBtn.onClick.AddListener(() =>
        {
            TestTurretDataBase.Instance.Upgrade(selectType);
        });

        
        //선택버튼 확인 기능
        applyBtn[0].onClick.AddListener(() => {
            ClearSelect();
            selectObj[0].SetActive(true);
        });
        applyBtn[1].onClick.AddListener(() => {
            ClearSelect();
            selectObj[1].SetActive(true);
        });
        applyBtn[2].onClick.AddListener(() => {
            ClearSelect();
            selectObj[2].SetActive(true);
        });
        applyBtn[3].onClick.AddListener(() => {
            ClearSelect();
            selectObj[3].SetActive(true);
        });
        applyBtn[4].onClick.AddListener(() => {
            ClearSelect();
            selectObj[4].SetActive(true);
        });
        
        
    }

    
    
    void Start()
    {

        mainMenuBtn.onClick.AddListener(() =>
        {
            num *= -1;

            OpenMenu();
        });

        goldAmounTxt.text = GameManager.Instance.goldAmount.ToString();
        waveTxt.text = "WAVE : " + SpawnMananger.Instance.round.ToString();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            index = index * -1;
            
            if (index > 0)
            {
                upGradePanelRect.DOAnchorPosY(780, 1.5f);
            }

            OpenBluePrint(index);
        }
        goldAmounTxt.text = GameManager.Instance.goldAmount.ToString();
        waveTxt.text = "WAVE : " + SpawnMananger.Instance.round.ToString();

        if(Input.GetKeyDown(KeyCode.P))
        {
            LoadingSceneUI.LoadScene("TitleScene");
            sceneIndex = 1;
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            backIndex *= -1;
            backPanelOpne(backIndex);
        }

        if (GameManager.Instance.state == GameManager.State.End)
        {
            Time.timeScale = 1f;
            TestTurretDataBase.Instance.resultEXP += GameManager.Instance.expAmount;
            LoadingSceneUI.LoadScene("TitleScene");
            sceneIndex = 1;
            GameManager.Instance.state = GameManager.State.Ready;
        }

        if(warningTxt.color.a >= 0)
        {
            warningTxt.color = new Color(warningTxt.color.r, warningTxt.color.g, warningTxt.color.b, Mathf.Lerp(warningTxt.color.a, 0, Time.deltaTime * 2));
        }
    }

    public void ClearSelect()
    {
        for(int i = 0; i < 5; i++)
        {
            selectObj[i].SetActive(false);
        }

    }

    public void OpenBluePrint(int index)
    {
        if (index == -1)
        {
            bpTop.DOAnchorPosY(300, 1f).SetEase(Ease.OutQuart);
            bpBot.DOAnchorPosY(-300, 1f).SetEase(Ease.OutQuart);


        }
        if (index == 1)
        {

            bpTop.DOAnchorPosY(781, 1.5f).SetEase(Ease.OutQuart);
            bpBot.DOAnchorPosY(-789, 1.5f).SetEase(Ease.OutQuart);
        }
    }

    public void backPanelOpne(int backIndex)
    {
        if(backIndex == -1)
        {
            stopPanelRect.DOScale(new Vector3(1,1,1), 0.8f);
        }
        if(backIndex == 1)
        {
            stopPanelRect.DOScale(new Vector3(0,0,0), 0.8f);
        }
        
    }

    public void OpenMenu()
    {
        if (num == -1)
        {
            btnGroup.SetActive(true);
            menuBtnRect[0].DOAnchorPosY(-60,1f);
            menuBtnRect[1].DOAnchorPosY(-100,1f);
            menuBtnRect[2].DOAnchorPosY(-140,1f);
            menuBtnRect[3].DOAnchorPosY(-180,1f);
        }

        if (num == 1)
        {
            for (int i = 0; i < 4; i++)
            {
                menuBtnRect[i].DOAnchorPosY(0,1).OnComplete(()=>
                btnGroup.SetActive(false)
                );
            }
            
        }

    }
}
