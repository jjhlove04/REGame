using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
    public static InGameUI _instance = new InGameUI();
    [SerializeField] private GameObject bluePrintTop;
    [SerializeField] private GameObject bluePrintBot;
    public static int sceneIndex = 0;

    int index = 1;
    int num = 1;
    int backIndex = 1;
    private List<RectTransform> menuBtnList = new List<RectTransform>();
    [SerializeField] private Button mainMenuBtn;
    [SerializeField] private GameObject btnGroup;
    [SerializeField] private GameObject upGradePanel;
    [SerializeField] private GameObject stopPanel;
    [SerializeField] private  RectTransform stopPanelRect;
    [HideInInspector] public RectTransform upGradePanelRect;

    [SerializeField] private Button upGradePanelBackBtn;
    [SerializeField] private Text time;
    [SerializeField] private Button[] applyBtn;
    [SerializeField] private GameObject[] selectObj;
    [SerializeField] private GameObject giveUPPanel;
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject trainWorldUI;

    private bool onPanel;

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

    Coroutine coroutine;

    testScriptts testScriptts;
    private void Awake()
    {
        _instance = this;
        bpTop = bluePrintTop.GetComponent<RectTransform>();
        bpBot = bluePrintBot.GetComponent<RectTransform>();
        upGradePanelRect = upGradePanel.GetComponent<RectTransform>();
        upGradePanelBackBtn.onClick.AddListener(() => 
        {
            upGradePanelRect.DOAnchorPosX(200, 1.5f).SetUpdate(true);
            testScriptts.UnSelectTurret();
        });
        upGradeBtn.onClick.AddListener(() =>
        {
            TestTurretDataBase.Instance.Upgrade(selectType);
        });

        //선택버튼 확인 기능
        applyBtn[0].onClick.AddListener(() => { 
            NewSelect(0); 
        });
        applyBtn[1].onClick.AddListener(() => {
            NewSelect(1);
        });
        applyBtn[2].onClick.AddListener(() => {
            NewSelect(2);
        });
        applyBtn[3].onClick.AddListener(() => {
            NewSelect(3);
        });
        applyBtn[4].onClick.AddListener(() => {
            NewSelect(4);
        });
    }

    void Start()
    {
        testScriptts = testScriptts.Instance;

        mainMenuBtn.onClick.AddListener(() =>
        {
            num *= -1;
        });

        goldAmounTxt.text = GameManager.Instance.goldAmount.ToString();
        waveTxt.text = "WAVE : " + (SpawnMananger.Instance.round - 1).ToString();
    }


    void Update()
    {
        goldAmounTxt.text = GameManager.Instance.goldAmount.ToString();
        waveTxt.text = "WAVE : " + (SpawnMananger.Instance.round - 1).ToString();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //    if ((settingPanel.activeSelf == false && giveUPPanel.activeSelf == true) 
            //        || (settingPanel.activeSelf == true && giveUPPanel.activeSelf == false)
            //        || (settingPanel.activeSelf == false && giveUPPanel.activeSelf == false))
            //    {
            if (settingPanel.activeSelf == true)
            {
                settingPanel.SetActive(false);
            }
            else if(giveUPPanel.activeSelf == true)
            {
                giveUPPanel.SetActive(false);
            }
            else
            {
                backIndex *= -1;
                backPanelOpne(backIndex);
            }
            //}

        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            testScriptts.Reload();
        }

        if (GameManager.Instance.state == GameManager.State.End)
        {
            Time.timeScale = 1f;
            TestTurretDataBase.Instance.resultEXP += GameManager.Instance.expAmount;
            TestTurretDataBase.Instance.resultGold += GameManager.Instance.goldAmount;

            LoadingSceneUI.LoadScene("TitleScene");
            sceneIndex = 1;
            GameManager.Instance.state = GameManager.State.Ready;
        }

        if(warningTxt.color.a >= 0)
        {
            warningTxt.color = new Color(warningTxt.color.r, warningTxt.color.g, warningTxt.color.b, Mathf.Lerp(warningTxt.color.a, 0, Time.deltaTime * 2));
        }

        /*if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();


            if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Ground"))
            {
                CancleAll();
            }
        }*/

    }

    public void ClearSelect()
    {
        for(int i = 0; i < 5; i++)
        {
            selectObj[i].SetActive(false);
        }
    }

    public void NewSelect(int num)
    {
        ClearSelect();

        selectObj[num].SetActive(true);
    }

    /*private void OpenBluePrint()
    {
        bpBot.DOAnchorPosY(-350, 1f).SetEase(Ease.OutQuart).SetUpdate(true);
    }*/

    public void CloseBluePrint()
    {
        bpBot.DOAnchorPosY(-730, 1.5f).SetEase(Ease.OutQuart).SetUpdate(true);

        upGradePanelRect.DOAnchorPosX(200, 1.5f).SetUpdate(true);
    }

    public void CancelTurret()
    {
        ClearSelect();
        testScriptts.turType = -1;
        testScriptts.turPos = -1;
        testScriptts.UnSelectTurret();
        upGradePanelRect.DOAnchorPosX(200, 1.5f).SetUpdate(true);
    }

    public void CancleAll()
    {
        if(bluePrintTop.activeSelf != false)
        {
            ClosePresetBtn();

            upGradePanelRect.DOAnchorPosX(200, 1.5f).SetUpdate(true);

            ClearSelect();
            testScriptts.turType = -1;
            testScriptts.turPos = -1;
            testScriptts.UnSelectTurret();
        }    
    }

    

    public void backPanelOpne(int backIndex)
    {
        if(backIndex == -1)
        {
            Time.timeScale = 0;
            GameManager.Instance.state = GameManager.State.Stop;
            stopPanelRect.DOScale(new Vector3(1,1,1), 0.8f).SetUpdate(true);
        }
        if(backIndex == 1)
        {
            Time.timeScale = 1;
            GameManager.Instance.state = GameManager.State.Play;
            stopPanelRect.DOScale(new Vector3(0,0,0), 0.8f).SetUpdate(true);
        }
        
    }
    public void backBtn()
    {
        backIndex *= -1;
        backPanelOpne(backIndex);
    }

    public void PresetBtn()
    {
        if(bpTop.position.x == -300)
        {
            bpTop.DOAnchorPosX(300, 0.8f).SetUpdate(true);
        }

        else if(bpTop.position.x == 300)
        {
            bpTop.DOAnchorPosX(-300, 0.8f).SetUpdate(true);
            testScriptts.TurCancle();
        }
    }

    public void OpenPresetBtn()
    {
        bpTop.DOAnchorPosX(300, 0.8f).SetUpdate(true);
    }

    public void ClosePresetBtn()
    {
        bpTop.DOAnchorPosX(-300, 0.8f).SetUpdate(true);
    }

    public void OpenTitleScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("TitleScene");

    }

    public void OpenGiveUPPanel()
    {
        giveUPPanel.SetActive(true);
        StartGiveUpPanelBackCoroutine();
    }

    public void CloseGiveUpPanel()
    {
        giveUPPanel.SetActive(false);
        StopGiveUpPanelBackCoroutine();
    }

    private void StartGiveUpPanelBackCoroutine()
    {
        coroutine = StartCoroutine(GiveUpPanelBack());
    }

    private void StopGiveUpPanelBackCoroutine()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }

    private IEnumerator GiveUpPanelBack()
    {
        for (int i = 10; i >= 0; i--)
        {
            time.text = "" + i;
            yield return new WaitForSecondsRealtime(1);
        }

        CloseGiveUpPanel();
    }
}