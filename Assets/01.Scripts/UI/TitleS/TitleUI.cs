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
    [SerializeField] private RectTransform turretPanel;
    public Button startBtn;

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
    public Text upHpText;
    [Space(20)]
    public Text curCellText;
    public Text upCellText;
    [Space(20)]
    public Text curShieldText;
    public Text upShieldText;

  
  

    public Image cursor;

    private void Awake()
    {
        ShowShieldUpGradeText();
        ShowHpUpgradeText();
        ShowCountUPGradeText();
        if(!TestDatabase.Instance.isRegister)
        {
            RegisterPanelOpen();
        }

        btnGroupAct.interactable = false;
        openSequence = DOTween.Sequence();
        closeSequence = DOTween.Sequence();
        _ui = this;
        checkPanel.transform.localScale = Vector3.zero;
        
        registerApplyBtn.onClick.AddListener(() => RegisterDataConnect());

        //닉네임 확인
        //checkPanel.transform.DOScale(new Vector3(1,1,1),0.8f)
        //nickCheckBtn.onClick.AddListener(()=> RegisterDataConnect());


        //for (int i = 0; i < 7; i++)
        //{
        //    buyBtns[i].gameObject.AddComponent<TooltipScript>();
        //}

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

        if (TestDatabase.Instance.isRegister)
        {
            RegisterDataConnect();
        }
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
        //haveTp.text = TestTurretDataBase.Instance.curTp.ToString();
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
        openSequence.SetAutoKill(true);
        closeSequence.SetAutoKill(true);
        if (num == 1)
        {
            openSequence.Kill();
            openSequence.Append(mapPanel.DOAnchorPosX(49, 1.2f).SetEase(Ease.InOutExpo));
            openSequence.Append(turretPanel.DOAnchorPosX(362, 1.2f).SetEase(Ease.InOutExpo));
            openSequence.Append(turretPanel.DOAnchorPosY(291, 1.2f).SetEase(Ease.InOutExpo));
            openSequence.Append(itemPanel.DOAnchorPosX(362, 1.2f).SetEase(Ease.InOutExpo));
            openSequence.Append(itemPanel.DOAnchorPosY(-156, 1.2f).SetEase(Ease.InOutExpo));
        }
        if(num == 2)
        {
            closeSequence.Kill();
            closeSequence.Append(mapPanel.DOAnchorPosX(-727, 0.6f).SetEase(Ease.InOutExpo));
            closeSequence.Append(turretPanel.DOAnchorPosX(1253, 0.6f).SetEase(Ease.InOutExpo));
            closeSequence.Append(turretPanel.DOAnchorPosY(768, 0.6f).SetEase(Ease.InOutExpo));
            closeSequence.Append(itemPanel.DOAnchorPosX(1253, 0.6f).SetEase(Ease.InOutExpo));
            closeSequence.Append(itemPanel.DOAnchorPosY(-768, 0.6f).SetEase(Ease.InOutExpo));

        }

    }
    //초기 등록화면 띄우기
    public void RegisterPanelOpen()
    {
        registerPanel.DOAnchorPosX(0,1f).SetEase(Ease.InOutCubic);
    }

    public void RegisterDataConnect()
    {
        btnGroupAct.interactable = true;
        TestDatabase.Instance._nickName = nickName.text;
        playerCardNick.text = TestDatabase.Instance._nickName;
        registerPanel.DOAnchorPosX(-1289, 1f).SetEase(Ease.InOutCubic);
        playerCard.DOAnchorPosX(-18,1f).SetEase(Ease.InOutCubic);

        TestDatabase.Instance.isRegister = true;
        
    }


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
        curHpText.text = TestTurretDataBase.Instance.trainHp.ToString();
        upHpText.text = (TestTurretDataBase.Instance.trainHp + 50).ToString();
    }
    public void ShowCountUPGradeText()
    {
        curCellText.text = TestTurretDataBase.Instance.trainCount.ToString();
        upCellText.text = (TestTurretDataBase.Instance.trainCount + 1).ToString();
        if(TestTurretDataBase.Instance.trainCount == 3)
        {
            upCellText.text = string.Format("최고 강화 수치입니다.");
        }
    }
    public void ShowShieldUpGradeText()
    {
        curShieldText.text = TestTurretDataBase.Instance.trainShield.ToString();
        upShieldText.text = (TestTurretDataBase.Instance.trainShield+50).ToString();

    }
    
}