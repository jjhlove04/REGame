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
    
    TitleMoveScript titleMoveScript;
    public ItemContainer itemContainer;

    public TrainInfo trainInfo;

    public Button[] buyBtns;

    [Header("업그레이드 관련")]
    Sequence upGradeSequence;
    Sequence outUpGradeSequence;
    public GameObject[] upGradeTexts;
    public Button[] upGradeBtns; //0번 터렛, 1번 기차, 2번 타워
    public GameObject[] upGradePanels; //0번 터렛, 1번 기차, 2번 타워
    public Image explainTurretImage;
    public Text[] explainTxt;
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
    public CanvasGroup btnGroupAct;

    [Header("결과 패널 관련")]
    [SerializeField] private Text repairCost;
    [SerializeField] private Text towingCost;
    public Text killEnemy;
    public Text acquiredGold;
    public Text acquiredExp;
    public Text damageTxt;

    public int curExp = 0;
    public int maxExp = 30;
    public Slider expBar;
    public Text levelTxt;

    [Header("업그레이드 패널 관련")]
    public Text curHpText;
    public Button[] towerPanelsBtn;
    public Image[] towerRockImg;
    [Space(20)]
    public Text curCellText;
    [Space(20)]
    public Text curShieldText;
    public Text upPanelTP;
    public Text upPanelMoney;

    public GameObject[] upCostTxt;

    public CanvasGroup tpWarning;

    public Image cursor;
    [Header("튜토리얼 관련")]
    
    public GameObject[] tutorialPanels;

    private void Awake()
    {
        titleMoveScript = GetComponent<TitleMoveScript>();

        RegisterPanelOpen();
        ShowShieldUpGradeText();
        ShowHpUpgradeText();
        ShowCountUPGradeText();
        ShowTPText();

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

        for (int i = 0; i < itemContainer.commonItem.Length; i++)
        {
            if (!TestTurretDataBase.Instance.postItemDic.ContainsKey(itemContainer.commonItem[i].ToString()))
            {
                TestTurretDataBase.Instance.postItemDic.Add(itemContainer.commonItem[i].ToString(), itemContainer.commonItem[i].interactable);
            }
        }

        for (int item = 0; item < 7; item++)
        {
            itemContainer.Gatcha();
        }

        startBtn.onClick.AddListener(() =>
        {
            if(TestTurretDataBase.Instance.curTurretType.Count == 0)
            {
                TestTurretDataBase.Instance.curTurretType.Add("0-0", Resources.Load<GameObject>("Turret/baseTurret0-0"));
            }
            TitleUI.UI.titleBack = true;
            LoadingSceneUI.LoadScene("Main");
        });
        upGradeBtns[0].onClick.AddListener(() =>
        {
            RemoveBtn();
            upGradePanels[0].SetActive(true);
            titleBack = true;
            TitleMoveScript.indexNum = 4;
            titleMoveScript.isback = false; 
        });
        upGradeBtns[1].onClick.AddListener(() =>
        {
            RemoveBtn();
            upGradePanels[1].SetActive(true);
            TitleMoveScript.indexNum = 4;
            titleBack = true;

            titleMoveScript.isback = false;

        });
        upGradeBtns[2].onClick.AddListener(() =>
        {
            RemoveBtn();
            upGradePanels[2].SetActive(true);
            TitleMoveScript.indexNum = 4;
            titleBack = true;

            titleMoveScript.isback = false;
        });

        if(TestTurretDataBase.Instance.level == 1)
        {
            for (int i = 0; i < towerPanelsBtn.Length; i++)
            {
                towerPanelsBtn[i].interactable = false;
            }
        }

        if(TestTurretDataBase.Instance.level >= 3)
        {
            towerRockImg[0].gameObject.SetActive(false);
            towerPanelsBtn[0].interactable = true;
            if(TestTurretDataBase.Instance.level >= 5)
            {
                towerRockImg[1].gameObject.SetActive(false);
                towerPanelsBtn[1].interactable = true;
                if(TestTurretDataBase.Instance.level >= 7)
                {
                    towerRockImg[2].gameObject.SetActive(false);
                    towerPanelsBtn[2].interactable = true;
                }
            }
        }
    }

    private void Start()
    {
        curExp += TestTurretDataBase.Instance.resultEXP;

        levelTxt.text = TestTurretDataBase.Instance.level.ToString();

        repairCost.text = ((TestTurretDataBase.Instance.round - 1) * 1.5f)/*TestTurretDataBase.Instance.trainCount)*/.ToString();
        towingCost.text = 0 + "";//((TestTurretDataBase.Instance.round - 1) * (TestTurretDataBase.Instance.round - 1)).ToString();
        killEnemy.text = TestTurretDataBase.Instance.killEnemy.ToString();
        acquiredGold.text = TestTurretDataBase.Instance.resultGold.ToString();
        acquiredExp.text = TestTurretDataBase.Instance.resultEXP.ToString();
        damageTxt.text = TestTurretDataBase.Instance.resultDamage.ToString();

        //등록관련
        // if (TestDatabase.Instance.isRegister)
        // {
        //     RegisterDataConnect();
        // }

        //if(TestTurretDataBase.Instance.isfirst)
        //{
        //    trainInfo.
        //}

    }
    private void Update()
    {
        //Cursor.visible = false;

        Update_MousePosition();
        InitPlayerInfo();
        ExpBar();
        ShowTPText();

        levelTxt.text = TestTurretDataBase.Instance.level.ToString();

        cursor.transform.position = Input.mousePosition;

        if (tpWarning.alpha >= 0)
        {
            tpWarning.alpha = Mathf.Lerp(tpWarning.alpha, 0, Time.deltaTime * 2);
        }
    }


    
    public void InitPlayerInfo()
    {
        haveGold.text = TestTurretDataBase.Instance.resultGold.ToString();
        haveExp.text = TestTurretDataBase.Instance.level.ToString();
        haveTp.text = TestTurretDataBase.Instance.curTp.ToString();
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
                TestTurretDataBase.Instance.curTp++;
                TestTurretDataBase.Instance.level++;
                curExp = curExp - maxExp;
                if (TestDatabase.Instance.Level % 20 == 0)
                {
                    maxExp = (int)(((maxExp + (TestTurretDataBase.Instance.level + (TestTurretDataBase.Instance.level - 1))) * (TestTurretDataBase.Instance.level / (TestTurretDataBase.Instance.level - 1)) + maxExp) * 1.2f);
                }
                else
                {
                    maxExp = (maxExp + (TestTurretDataBase.Instance.level + (TestTurretDataBase.Instance.level - 1))) * (TestDatabase.Instance.Level / (TestTurretDataBase.Instance.level - 1)) + maxExp;
                }
                expBar.value = 0;
                levelTxt.text = TestTurretDataBase.Instance.level.ToString();
            }
        }
    }

    public void ShowHpUpgradeText()
    {
        curHpText.text = string.Format("{0} -> <color=#34A11F>{1}</color>",
        trainInfo.trainMaxHp,
        (trainInfo.trainMaxHp + trainInfo.hpUpgrade));
        //upHpText.text = (TestTurretDataBase.Instance.trainHp + 50).ToString();
    }
    public void ShowCountUPGradeText()
    {
        //curCellText.text = TestTurretDataBase.Instance.trainCount.ToString();
        curCellText.text = string.Format("{0} -> <color=#34A11F>{1}</color>",
        trainInfo.trainCount,
        (trainInfo.trainCount + 1));
        if (trainInfo.trainCount == 3)
        {
            curCellText.text = string.Format("<color=red>Max</color>");
        }
    }
    public void ShowShieldUpGradeText()
    {
        curShieldText.text = string.Format("{0} -> <color=#34A11F>{1}</color>",
      trainInfo.trainMaxShield,
      (trainInfo.trainMaxShield + trainInfo.shieldUpgrade));
        //upShieldText.text = (TestTurretDataBase.Instance.trainShield+50).ToString();

    }

    public void ShowTPText()
    {
        upPanelTP.text = string.Format("Retain T.P : {0}", TestTurretDataBase.Instance.curTp);
        upPanelMoney.text = "보유 돈 : " + TestTurretDataBase.Instance.resultGold;

        upCostTxt[0].GetComponent<Text>().text = "필요 비용 : " + trainInfo.hpPrice;
        upCostTxt[1].GetComponent<Text>().text = "필요 비용 : " + trainInfo.shieldPrice;
        upCostTxt[2].GetComponent<Text>().text = "필요 비용 : " + trainInfo.trainCountPrice;
    }

    
}