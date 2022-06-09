using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class TitleUI : MonoBehaviour
{
    private static TitleUI _ui = new TitleUI();
    public static TitleUI UI { get { return _ui; } }

    public Button[] buyBtns;

    [Header("업그레이드 관련")]
    Sequence upGradeSequence;
    Sequence outUpGradeSequence;
    public GameObject[] upGradeTexts;
    public Button[] upGradeBtns; //0번 터렛, 1번 기차, 2번 타워
    public GameObject[] upGradePanels; //0번 터렛, 1번 기차, 2번 타워
    public Image explainTurretImage;
    public Text[] TurretInfo;
    public Image informationPanel;

    [Header("출발준비 패널 관련")]
    Sequence openSequence;
    Sequence closeSequence;
    [SerializeField] private RectTransform mapPanel;
    [SerializeField] private RectTransform itemPanel;
    public Button startBtn;
    private Text startText;
    public bool titleBack = false;

    [Header("등록관련")]
    [SerializeField] private InputField nickName;
    [SerializeField] private RectTransform registerPanel;
    [SerializeField] private GameObject checkPanel;
    [SerializeField] private Button registerApplyBtn;
    [SerializeField] private Button nickCheckBtn;
    [SerializeField] private Text playerCardNick;
    [Header("플레이어 카드 관련")]
    public RectTransform playerCard;
    [SerializeField] private Text haveGold;
    [SerializeField] private Text haveExp;
    [SerializeField] private Text haveTp;


    [Space(30)]
    [SerializeField] private Text repairCost;
    [SerializeField] private Text towingCost;
    public CanvasGroup btnGroupAct;
    
    public int curExp = 0;
    public int maxExp = 30;
    public Slider expBar;
    public Text levelTxt;

    [Header("업그레이드 패널 관련")]
    public Text curHpText;
    [Space(20)]
    public Text curCellText;
    [Space(20)]
    public Text curShieldText;

    public Image cursor;


    private void Awake()
    {

        RegisterPanelOpen();
        ShowShieldUpGradeText();
        ShowHpUpgradeText();
        ShowCountUPGradeText();
        
        openSequence = DOTween.Sequence();
        closeSequence = DOTween.Sequence();

        upGradeSequence = DOTween.Sequence();
        outUpGradeSequence = DOTween.Sequence();

        _ui = this;
        checkPanel.transform.localScale = Vector3.zero;
        startText = startBtn.GetComponent<Text>();
        {//등록관련
        //registerApplyBtn.onClick.AddListener(() => RegisterDataConnect());

        //닉네임 확인
        //checkPanel.transform.DOScale(new Vector3(1,1,1),0.8f)
        //nickCheckBtn.onClick.AddListener(()=> RegisterDataConnect());


        //for (int i = 0; i < 7; i++)
        //{
        //    buyBtns[i].gameObject.AddComponent<TooltipScript>();
        //}
        }

        if(TestTurretDataBase.Instance.curTurretType.Count == 1)
        {
            TestTurretDataBase.Instance.curTurretType.Clear();
        }

        startBtn.onClick.AddListener(() =>
        {
            if(TestTurretDataBase.Instance.curTurretType.Count == 0)
            {
                TestTurretDataBase.Instance.curTurretType.Add("0-0", Resources.Load<GameObject>("Turret/baseTurret0-0"));
            }

            LoadingSceneUI.LoadScene("Main");
        });
        upGradeBtns[0].onClick.AddListener(() =>
        {
            RemoveBtn();
            upGradePanels[0].SetActive(true);
            TitleMoveScript.indexNum = 4;
        });
        upGradeBtns[1].onClick.AddListener(() =>
        {
            RemoveBtn();
            upGradePanels[1].SetActive(true);
            TitleMoveScript.indexNum = 4;
        });
        upGradeBtns[2].onClick.AddListener(() =>
        {
            RemoveBtn();
            upGradePanels[2].SetActive(true);
            TitleMoveScript.indexNum = 4;
        });

        curExp += TestDatabase.Instance.resultEXP;

        levelTxt.text = TestDatabase.Instance.Level.ToString();
    }

    private void Start()
    {
        Cursor.visible = false;
        repairCost.text = ((TestTurretDataBase.Instance.round - 1) * TestTurretDataBase.Instance.createPrice).ToString();
        towingCost.text = ((TestTurretDataBase.Instance.round - 1) * (TestTurretDataBase.Instance.round - 1)).ToString();

        //등록관련
        // if (TestDatabase.Instance.isRegister)
        // {
        //     RegisterDataConnect();
        // }
    }
    private void Update()
    {
        Update_MousePosition();
        InitPlayerInfo();
        ExpBar();
        

        cursor.transform.position = Input.mousePosition;
    }


    
    public void InitPlayerInfo()
    {
        haveGold.text = TestDatabase.Instance.resultGold.ToString();
        haveExp.text = TestDatabase.Instance.Level.ToString();
        haveTp.text = TestDatabase.Instance.curTp.ToString();
    }
    //패널 오픈 함수
    public void RemoveBtn()
    {
        for (int i = 0; i < 3; i++)
        {
            upGradeBtns[i].gameObject.SetActive(false);
        }
    }

    private void Update_MousePosition()
    {
        Vector2 mousePos = Input.mousePosition;
        //{"Wav {0}"}
    }

    public void ReadySetUpPanel(int num)
    {
        openSequence.SetAutoKill(false);
        closeSequence.SetAutoKill(false);
        if (num == 1)
        {
            openSequence.Kill();
            startBtn.enabled = true;
            startBtn.interactable = true;
            openSequence.Append(startText.DOFade(1.0f, 1.2f)).SetEase(Ease.InOutExpo); 
            openSequence.Append(mapPanel.DOAnchorPosX(49, 1.2f).SetEase(Ease.InOutExpo));
            openSequence.Append(itemPanel.DOAnchorPosX(353, 1.2f).SetEase(Ease.InOutExpo));
        }
        if(num == 2)
        {
            closeSequence.Kill();

            openSequence.Append(startText.DOFade(0.0f, 1.2f).SetEase(Ease.InOutExpo).OnComplete(()=>{
                startBtn.enabled = false;
            startBtn.interactable = false;
            }));
            closeSequence.Append(mapPanel.DOAnchorPosX(-727, 0.6f).SetEase(Ease.InOutExpo));
            closeSequence.Append(itemPanel.DOAnchorPosX(1601, 0.6f).SetEase(Ease.InOutExpo));

        }
    }

    public void UpGradePanelOpen(int num)
    {
        // upGradeSequence.SetAutoKill(true);
        // outUpGradeSequence.SetAutoKill(true);

        if(num == 1)
        {
            for(int i = 0; i<upGradeBtns.Length; i++)
            {
                upGradeTexts[i].SetActive(true);
            }
            upGradeSequence.Append(upGradeTexts[0].GetComponent<Text>()
            .DOFade(1,0.6f)
            .SetDelay(0.7f)
            .SetEase(Ease.OutQuint));
            upGradeSequence.Append(upGradeTexts[1].GetComponent<Text>()
            .DOFade(1,0.6f)
            .SetDelay(0.7f)
            .SetEase(Ease.OutQuint));
            upGradeSequence.Append(upGradeTexts[2].GetComponent<Text>()
            .DOFade(1,0.6f)
            .SetDelay(0.7f)
            .SetEase(Ease.OutQuint));
            upGradeSequence.SetAutoKill(false);
            
        }
        if(num == 2)
        {
            for(int i = 0; i<upGradeBtns.Length; i++)
            {
                upGradeTexts[i].SetActive(false);
            }
            outUpGradeSequence.Append(upGradeTexts[0].GetComponent<Text>()
            .DOFade(0,0.6f)
            .SetDelay(0.7f)
            .SetEase(Ease.OutQuint));
            outUpGradeSequence.Append(upGradeTexts[1].GetComponent<Text>()
            .DOFade(0,0.6f)
            .SetDelay(0.7f)
            .SetEase(Ease.OutQuint));
            outUpGradeSequence.Append(upGradeTexts[2].GetComponent<Text>()
            .DOFade(0,0.6f)
            .SetDelay(0.7f)
            .SetEase(Ease.OutQuint));
            outUpGradeSequence.SetAutoKill(false);


        }
        
    }

    //초기 등록화면 띄우기
    public void RegisterPanelOpen()
    {
        registerPanel.DOAnchorPosX(0,1f).SetEase(Ease.InOutCubic);
    }

    // public void RegisterDataConnect()
    // {
    //     btnGroupAct.interactable = true;
    //     TestDatabase.Instance._nickName = nickName.text;
    //     playerCardNick.text = TestDatabase.Instance._nickName;
    //     registerPanel.DOAnchorPosX(-1289, 1f).SetEase(Ease.InOutCubic);
    //     playerCard.DOAnchorPosX(-18,1f).SetEase(Ease.InOutCubic);

    //     TestDatabase.Instance.isRegister = true;
        
    // }


    private void ExpBar()
    {
        expBar.value = Mathf.Lerp(expBar.value, (float)curExp / (float)maxExp, Time.deltaTime * (2 + (curExp / 500)));

        if (expBar.value >= 0.99f)
        {
            if (curExp >= maxExp)
            {
                //TestTurretDataBase.Instance.curTp++;
                TestDatabase.Instance.Level++;
                curExp = curExp - maxExp;
                if (TestDatabase.Instance.Level % 20 == 0)
                {
                    maxExp = (int)(((maxExp + (TestDatabase.Instance.Level + (TestDatabase.Instance.Level - 1))) * (TestDatabase.Instance.Level / (TestDatabase.Instance.Level - 1)) + maxExp) * 1.2f);
                }
                else
                {
                    maxExp = (maxExp + (TestDatabase.Instance.Level + (TestDatabase.Instance.Level - 1))) * (TestDatabase.Instance.Level / (TestDatabase.Instance.Level - 1)) + maxExp;
                }
                expBar.value = 0;
                levelTxt.text = TestDatabase.Instance.Level.ToString();
            }
        }
    }

    public void ShowHpUpgradeText()
    {
        curHpText.text = string.Format("{0} -> <color=#34A11F>{1}</color>",
        TestTurretDataBase.Instance.trainHp,
        (TestTurretDataBase.Instance.trainHp + 50));
        //upHpText.text = (TestTurretDataBase.Instance.trainHp + 50).ToString();
    }
    public void ShowCountUPGradeText()
    {
        //curCellText.text = TestTurretDataBase.Instance.trainCount.ToString();
        curCellText.text = string.Format("{0} -> <color=#34A11F>{1}</color>",
        TestTurretDataBase.Instance.trainCount,
        (TestTurretDataBase.Instance.trainCount + 1));
        if(TestTurretDataBase.Instance.trainCount == 3)
        {
            curCellText.text = string.Format("<color=red>Max</color>");
        }
    }
    public void ShowShieldUpGradeText()
    {
        curShieldText.text = string.Format("{0} -> <color=#34A11F>{1}</color>",
        TestTurretDataBase.Instance.trainShield,
        (TestTurretDataBase.Instance.trainShield+50));
        //upShieldText.text = (TestTurretDataBase.Instance.trainShield+50).ToString();

    }
    
}