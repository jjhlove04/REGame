using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TitleInterfaceManager : MonoBehaviour
{
    public static TitleInterfaceManager Instance = new TitleInterfaceManager();

    public int sceneIndex;
    public bool canEscape = false;
    //세부타이틀 들어갔다 나올때.
    public bool canSubEscape = false;
    GameManager gameManager;
    TestTurretDataBase testTurretDatabase;
    ParsingJson parsingJson;
    public ItemContainer itemContainer;

    [Header("타이틀 - 버튼")]
    public RectTransform btnGroup;
    // 0 : 시작, 1 : 업그레이드, 2 : 상태, 3 : 도감, 4 : 설정, 5 : 종료
    public Button[] titleBtn;

    // 0 : 시작 - 출발, 1 : 업그레이드 - 기차, 2 : 업그레이드 - 타워, 3 : 업그레이드 - 아이템, 4 : 수리
    public Button[] inTitleBtn;


    public Button backBtn;
    [Header("타이틀 - 시퀀스")]
    Sequence titleStart;
    Sequence titleEnd;

    [Header("타이틀 - 플레이어")]
    public GameObject playerCard;
    [SerializeField] private Text haveGold;
    [SerializeField] private Text haveLevel;
    [SerializeField] private Text haveTp;
    public Slider curCardExp;

    [Header("타이틀 - 캔버스")]
    //0 : 출발 캔버스, 1 : 업그레이드 캔버스, 2 : 도감 캔버스, 3 : 결과 캔버스
    public GameObject[] canvasGroup;

    [Header("업그레이드 - 오브젝트")]
    bool isNowTitel = false;
    public GameObject fadeImg;
    public GameObject[] upPanel;
    public List<Button> upgradeBtn = new List<Button>();
    public List<Button> statList = new List<Button>();
    public GameObject trainUpgradeContent;
    public GameObject levelBox;
    public Text[] upgradeNames;
    public Text moneyTxt;
    public Text needGoldTxt;
    public Text explainTxt;
    public Text expName;

    private float maxMoney = 0;

    [Header("탸이틀 - 결과패널")]
    public GameObject resultPanel;
    public float curExp = 0;
    public float maxExp = 30;
    public Slider expBar;
    public Text levelTxt;
    [SerializeField] private Text repairCost;
    [SerializeField] private Text towingCost;
    public Text killEnemy;
    public Text acquiredGold;
    public Text acquiredExp;
    public Text damageTxt;

    public CanvasGroup warningTxt;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        parsingJson = ParsingJson.Instnace;
        testTurretDatabase = TestTurretDataBase.Instance;

        Sequence first = DOTween.Sequence();
        first.Append(fadeImg.transform.DOScale(1, 0.1f));
        first.Append(fadeImg.transform.DOScale(0, 0.1f));

        if (testTurretDatabase.curTurretType.Count == 1)
        {
            testTurretDatabase.curTurretType.Clear();
        }
        for (int i = 0; i < itemContainer.commonItem.Length; i++)
        {
            if (!testTurretDatabase.postItemDic.ContainsKey(itemContainer.commonItem[i].ToString()))
            {
                testTurretDatabase.postItemDic.Add(itemContainer.commonItem[i].ToString(), itemContainer.commonItem[i].interactable);
            }
        }

        //최초 한번만 실행
        if (testTurretDatabase.isfirst)
        {
            btnGroupAction(0);
            testTurretDatabase.isfirst = false;
        }
        //버튼 애드리스너 시작
        titleBtn[0].onClick.AddListener(() => {
            canvasGroup[0].SetActive(true);
            inTitleBtn[0].GetComponent<Text>().DOFade(1, 0.5f);
            sceneIndex = 1;
            btnGroupAction(1);
            backBtn.gameObject.SetActive(true);

        });
        titleBtn[1].onClick.AddListener(() => {
            canvasGroup[1].SetActive(true);
            sceneIndex = 2;
            btnGroupAction(1);
            IntitleAction(0);
            backBtn.gameObject.SetActive(true);

        });
        titleBtn[2].onClick.AddListener(() => {
            //상태창


        });
        titleBtn[3].onClick.AddListener(() => {

            btnGroupAction(2);
            backBtn.gameObject.SetActive(true);
        });
        titleBtn[5].onClick.AddListener(() => {
            Application.Quit();


        });
        inTitleBtn[0].onClick.AddListener(() => {
            if (testTurretDatabase.curTurretType.Count == 0)
            {
                testTurretDatabase.curTurretType.Add("0-0", Resources.Load<GameObject>("Turret/baseTurret0-0"));
            }
            LoadingSceneUI.LoadScene("Main");
        });


        inTitleBtn[1].onClick.AddListener(() => {
            IntitleAction(2);
            isNowTitel = true;
            sceneIndex = 3;
        });
        inTitleBtn[2].onClick.AddListener(() => {
            IntitleAction(4);
            isNowTitel = true;
            sceneIndex = 4;
        });
        inTitleBtn[3].onClick.AddListener(() => {
            IntitleAction(6);
            isNowTitel = true;
            sceneIndex = 5;
        });
        //수리
        inTitleBtn[4].onClick.AddListener(() => {

            testTurretDatabase.resultEXP = 0;
            testTurretDatabase.killEnemy = 0;
            testTurretDatabase.resultDamage = 0;
            testTurretDatabase.resultGold -= (int)(((testTurretDatabase.round - 1) * 1.5f) + ((testTurretDatabase.round - 1) * 1.5f) * (testTurretDatabase.plusRepair / 100));

            for (int i = 0; i < testTurretDatabase.postItemObj.Count; i++)
            {
                testTurretDatabase.postItemObj[i].curCarry = 0;

            }

            testTurretDatabase.isfirst = false;
            IntitleAction(10);
            sceneIndex = 0;
        });

        backBtn.onClick.AddListener(() =>
        {
            EscapeBtn();
        });



        //버튼 애드리스너 끝

        for (int i = 0; i < upgradeBtn.Count; i++)
        {
            if (parsingJson.count[i] == parsingJson.upgradeCose[i])
            {
                upgradeNames[i].text = "MAX";
            }
            else
            {
                upgradeNames[i].text = parsingJson.count[i].ToString();
            }
        }

        for (int i = 0; i < upgradeBtn.Count; i++)
        {
            TrainUpgradeBtn(i);
        }

        for (int j = 0; j < statList.Count; j++)
        {
            TrainStatPrice(j);
        }


    }
    void Start()
    {
        if (testTurretDatabase.sceneIndex == 1)
        {
            IntitleAction(8);
        }
        
        if (testTurretDatabase.postItemObj.Count < 1)
        {

            for (int item = 0; item < 5; item++)
            {
                itemContainer.Gatcha();
            }
        }



        repairCost.text = (((testTurretDatabase.round - 1) * 1.5f) + ((testTurretDatabase.round - 1) * 1.5f) * (testTurretDatabase.plusRepair/100)).ToString();
        towingCost.text = 0 + "";
        killEnemy.text = testTurretDatabase.killEnemy.ToString();
        acquiredGold.text = testTurretDatabase.resultGold.ToString();
        acquiredExp.text = testTurretDatabase.resultEXP.ToString();
        damageTxt.text = testTurretDatabase.resultDamage.ToString();


        levelTxt.text = testTurretDatabase.level.ToString();
        curExp += testTurretDatabase.tCurExp + testTurretDatabase.resultEXP;

        maxExp = parsingJson.maxExp[testTurretDatabase.level];

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscapeBtn();
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log((int)(parsingJson.price[0] * 0.53f));
        }

        if (Input.GetKey(KeyCode.P))
        {
            TestTurretDataBase.Instance.curTp++;
            TestTurretDataBase.Instance.resultGold += 1000;
        }

        InitPlayerInfo();
        ExpBar();
        moneyTxt.text = "소지골드 : " + testTurretDatabase.resultGold.ToString();

        if(warningTxt.alpha >= 0)
        {
            warningTxt.alpha = Mathf.Lerp(warningTxt.alpha, 0, Time.deltaTime * 2);
        }
    }

    public void btnGroupAction(int index)
    {
        Sequence collection1 = DOTween.Sequence();
        Sequence collection2 = DOTween.Sequence();

        //타이틀 들어오기
        if (index == 0)
        {
            titleStart.Kill();
            titleStart.Append(btnGroup.DOAnchorPosX(70, 0.3f));
            titleStart.Join(playerCard.GetComponent<RectTransform>().DOAnchorPosX(-70, 0.3f));
            titleStart.SetAutoKill(false);
        }
        //타이틀 뒤로가기
        if (index == 1)
        {
            titleEnd.Kill();
            titleEnd.Append(btnGroup.DOAnchorPosX(-340, 0.3f));
            titleEnd.Join(playerCard.GetComponent<RectTransform>().DOAnchorPosX(570, 0.3f).OnComplete(() =>
            {
                if (sceneIndex == 1 || sceneIndex == 2)
                {
                    canEscape = true;
                }
            }));
            titleEnd.SetAutoKill(false);
        }
        //도감버튼
        if (index == 2)
        {
            sceneIndex = 6;
            collection1.Append(fadeImg.transform.DOScale(1, 0.1f));
            collection1.Append(fadeImg.GetComponent<Image>().DOFade(1, 0.4f).OnComplete(() => {
                canvasGroup[2].SetActive(true);
                btnGroupAction(1);
                collection1.Append(fadeImg.GetComponent<Image>().DOFade(0, 0.4f).OnComplete(() => {
                    collection1.Append(fadeImg.transform.DOScale(0, 0.1f).OnComplete(() =>
                    {
                        canEscape = true;
                    }));
                }));
            }));
            collection1.SetAutoKill(false);
        }
        //도감 나오기
        if (index == 3)
        {
            canEscape = false;
            collection2.Append(fadeImg.transform.DOScale(1, 0.1f));
            collection2.Append(fadeImg.GetComponent<Image>().DOFade(1, 0.4f).OnComplete(() => {
                canvasGroup[2].SetActive(false);
                collection2.Append(fadeImg.GetComponent<Image>().DOFade(0, 0.4f).OnComplete(() => {
                    collection2.Append(fadeImg.transform.DOScale(0, 0.1f).OnComplete(() =>
                    {
                        btnGroupAction(0);
                    }));
                }));
            }));
            sceneIndex = 0;
            collection2.SetAutoKill(false);
        }


    }

    //세부 버튼 누르면 이벤트
    public void IntitleAction(int index)
    {
        Sequence upBtnShow = DOTween.Sequence();
        Sequence upBtnClose = DOTween.Sequence();
        Sequence upPanel1Show = DOTween.Sequence();
        Sequence upPanel2Show = DOTween.Sequence();
        Sequence upPanel3Show = DOTween.Sequence();
        Sequence upPanel1Close = DOTween.Sequence();
        Sequence upPanel2Close = DOTween.Sequence();
        Sequence upPanel3Close = DOTween.Sequence();
        Sequence resultShow = DOTween.Sequence();
        if (index == 0)
        {
            upBtnShow.Append(inTitleBtn[1].GetComponent<Text>().DOFade(1, 0.3f));
            upBtnShow.Append(inTitleBtn[2].GetComponent<Text>().DOFade(1, 0.3f));
            upBtnShow.Append(inTitleBtn[3].GetComponent<Text>().DOFade(1, 0.3f).OnComplete(() => {
                canEscape = true;
            }));
            upBtnShow.SetAutoKill(false);
        }
        if (index == 1)
        {
            upBtnClose.Append(inTitleBtn[1].GetComponent<Text>().DOFade(0, 0.2f));
            upBtnClose.Join(inTitleBtn[2].GetComponent<Text>().DOFade(0, 0.2f));
            upBtnClose.Join(inTitleBtn[3].GetComponent<Text>().DOFade(0, 0.2f).OnComplete(() =>
            {
                canvasGroup[1].SetActive(false);

            }));
            upBtnClose.SetAutoKill(false);
        }

        //up기차 들어가기
        if (index == 2)
        {
            upPanel1Show.Append(fadeImg.transform.DOScale(1, 0.1f));
            upPanel1Show.Append(fadeImg.GetComponent<Image>().DOFade(1, 0.4f).OnComplete(() => {
                IntitleAction(9);
                upPanel[0].SetActive(true);
                upPanel1Show.Append(fadeImg.GetComponent<Image>().DOFade(0, 0.4f).OnComplete(() => {
                    upPanel1Show.Append(fadeImg.transform.DOScale(0, 0.1f).OnComplete(() =>
                    {
                        canEscape = false;
                        canSubEscape = true;
                    }));
                }));
            }));

            TrainUpgradePanel();

            sceneIndex = 3;
            upPanel1Show.SetAutoKill(false);
        }
        //up기차에서 나오기
        if (index == 3)
        {
            upPanel1Close.Append(fadeImg.transform.DOScale(1, 0.1f));
            upPanel1Close.Append(fadeImg.GetComponent<Image>().DOFade(1, 0.4f).OnComplete(() =>
            {
                upPanel[0].SetActive(false);
                upPanel1Close.Append(fadeImg.GetComponent<Image>().DOFade(0, 0.4f).OnComplete(() =>
                {
                    upPanel1Close.Append(fadeImg.transform.DOScale(0, 0.1f));
                    IntitleAction(0);
                }));
                sceneIndex = 2;
            }));
            upPanel1Close.SetAutoKill(false);

        }
        //up 타워 패널 들어가기
        if (index == 4)
        {
            upPanel2Show.Append(fadeImg.transform.DOScale(1, 0.1f));
            upPanel2Show.Append(fadeImg.GetComponent<Image>().DOFade(1, 0.4f).OnComplete(() => {
                IntitleAction(9);
                upPanel[1].SetActive(true);
                upPanel2Show.Append(fadeImg.GetComponent<Image>().DOFade(0, 0.4f).OnComplete(() => {
                    upPanel2Show.Append(fadeImg.transform.DOScale(0, 0.1f).OnComplete(() =>
                    {
                        canEscape = false;
                        canSubEscape = true;
                    }));
                }));
            }));
            sceneIndex = 4;
            upPanel2Show.SetAutoKill(false);
        }
        //up타워에서 나오기
        if (index == 5)
        {
            upPanel2Close.Append(fadeImg.transform.DOScale(1, 0.1f));
            upPanel2Close.Append(fadeImg.GetComponent<Image>().DOFade(1, 0.4f).OnComplete(() =>
            {
                upPanel[1].SetActive(false);
                Debug.Log("패널 1 닫힘");

                upPanel2Close.Append(fadeImg.GetComponent<Image>().DOFade(0, 0.4f).OnComplete(() =>
                {
                    upPanel2Close.Append(fadeImg.transform.DOScale(0, 0.1f));
                    IntitleAction(0);
                }));
                sceneIndex = 2;
            }));
            upPanel2Close.SetAutoKill(false);

        }

        //up 아이템 뽑기
        if (index == 6)
        {
            upPanel3Show.Append(fadeImg.transform.DOScale(1, 0.1f));
            upPanel3Show.Append(fadeImg.GetComponent<Image>().DOFade(1, 0.4f).OnComplete(() => {
                IntitleAction(9);
                upPanel[2].SetActive(true);
                upPanel3Show.Append(fadeImg.GetComponent<Image>().DOFade(0, 0.4f).OnComplete(() => {
                    upPanel3Show.Append(fadeImg.transform.DOScale(0, 0.1f).OnComplete(() =>
                    {
                        canEscape = false;
                        canSubEscape = true;
                    }));
                }));
            }));
            sceneIndex = 5;
            upPanel3Show.SetAutoKill(false);
        }
        //up 아이템에서 나오기
        if (index == 7)
        {
            upPanel3Close.Append(fadeImg.transform.DOScale(1, 0.1f));
            upPanel3Close.Append(fadeImg.GetComponent<Image>().DOFade(1, 0.4f).OnComplete(() =>
            {
                upPanel[2].SetActive(false);
                Debug.Log("패널 2 닫힘");
                upPanel3Close.Append(fadeImg.GetComponent<Image>().DOFade(0, 0.4f).OnComplete(() =>
                {
                    upPanel3Close.Append(fadeImg.transform.DOScale(0, 0.1f));
                    IntitleAction(0);
                }));
                sceneIndex = 2;
            }));
            upPanel3Close.SetAutoKill(false);

        }

        //리페어 버튼 전
        if (index == 8)
        {
            resultShow.Append(resultPanel.transform.DOScale(1, 0.4f));
        }

        //리페어 버튼 누르기
        if (index == 10)
        {
            resultShow.Append(resultPanel.transform.DOScale(1.2f, 0.2f));
            resultShow.Append(resultPanel.transform.DOScale(0, 0.2f).OnComplete(() =>
            {
                btnGroupAction(0);
                canvasGroup[0].SetActive(true);
            }));
        }


        //캔버스 살아있는 업버튼 트위닝 => 세부 패널 들어갔다가 나올때만 호출
        if (index == 9)
        {
            upBtnClose.Append(inTitleBtn[1].GetComponent<Text>().DOFade(0, 0.2f));
            upBtnClose.Join(inTitleBtn[2].GetComponent<Text>().DOFade(0, 0.2f));
            upBtnClose.Join(inTitleBtn[3].GetComponent<Text>().DOFade(0, 0.2f).OnComplete(() =>
            {
            }));
            upBtnClose.SetAutoKill(false);
        }
    }

    public void TrainUpgradePanel()
    {
        for (int i = 0; i < trainUpgradeContent.transform.childCount; i++)
        {
            trainUpgradeContent.transform.GetChild(i).GetChild(1).GetComponent<Text>().text = ParsingJson.Instnace.upgradeName[i];
            for (int j = 0; j < ParsingJson.Instnace.upgradeCose[i]; j++)
            {
                Instantiate(levelBox, trainUpgradeContent.transform.GetChild(i).GetChild(2));
            }
        }
    }


    public void EscapeBtn()
    {
        //출발 나오기
        if (sceneIndex == 1 && canEscape)
        {
            btnGroupAction(0);
            canvasGroup[0].SetActive(false);
            canEscape = false;
            backBtn.gameObject.SetActive(false);
        }
        //업그레이드 창 나가기
        if (sceneIndex == 2 && canEscape)
        {
            btnGroupAction(0);
            IntitleAction(1);
            canEscape = false;
            backBtn.gameObject.SetActive(false);
            isNowTitel = false;
        }
        //업그레이드 세부 창1 나가기 
        if (sceneIndex == 3 && canSubEscape)
        {
            canSubEscape = false;
            IntitleAction(3);

        }
        //업그레이드 세부 창2 나가기 

        if (sceneIndex == 4 && canSubEscape)
        {
            canSubEscape = false;
            IntitleAction(5);

        }
        //업그레이드 세부 창2 나가기 

        if (sceneIndex == 5 && canSubEscape)
        {
            canSubEscape = false;
            IntitleAction(7);

        }
        if (sceneIndex == 6 && canEscape)
        {
            btnGroupAction(3);
            canEscape = false;
            backBtn.gameObject.SetActive(false);
        }
    }

    public void InitPlayerInfo()
    {
        haveGold.text = testTurretDatabase.resultGold.ToString();
        haveLevel.text = testTurretDatabase.level.ToString();
        haveTp.text = testTurretDatabase.curTp.ToString();
        curCardExp.value = expBar.value;
    }

    private void ExpBar()
    {
        expBar.value = Mathf.Lerp(expBar.value, curExp / maxExp, Time.deltaTime * (2 + (curExp / 500)));

        if (expBar.value >= 0.99f)
        {
            if (curExp >= maxExp)
            {
                testTurretDatabase.curTp++;
                testTurretDatabase.level++;
                curExp = curExp - maxExp;

                maxExp = parsingJson.maxExp[testTurretDatabase.level];

                expBar.value = 0;
                levelTxt.text = testTurretDatabase.level.ToString();
            }
        }
        testTurretDatabase.tCurExp = curExp;
    }

    public void TrainStatPrice(int j)
    {
        statList[j].onClick.AddListener(() =>
        {
            needGoldTxt.text = "가격 : " + parsingJson.price[j].ToString();
            explainTxt.text = parsingJson.explation[j];
            expName.text = parsingJson.upgradeName[j];
        });
    }

    public void TrainUpgradeBtn(int i)
    {
        upgradeBtn[i].onClick.AddListener(() =>
        {
            if (parsingJson.price[i] <= testTurretDatabase.resultGold)
            {
                if (parsingJson.count[i] < parsingJson.upgradeCose[i])
                {
                    TrainUpgrade(i);
                    testTurretDatabase.resultGold -= parsingJson.price[i];

                    maxMoney += parsingJson.price[i];
                    Debug.Log(maxMoney);

                    parsingJson.price[i] += (int)(parsingJson.price[i] * 0.53f);



                    for (int j = 0; j < upgradeBtn.Count; j++)
                    {
                        if (j != i)
                        {
                            parsingJson.price[j] += (int)(parsingJson.price[j] * 0.09f);
                        }
                    }
                    parsingJson.count[i]++;

                    if (parsingJson.count[i] == parsingJson.upgradeCose[i])
                    {
                        upgradeNames[i].text = "MAX";
                    }
                    else
                    {
                        upgradeNames[i].text = parsingJson.count[i].ToString();
                    }
                }
                else
                {
                    warningTxt.transform.GetChild(0).GetComponent<Text>().text = "더 이상 강화가 불가능합니다";
                    warningTxt.alpha = 1;
                }
            }
            else
            {
                warningTxt.transform.GetChild(0).GetComponent<Text>().text = "소지한 골드가 부족합니다"; 
                warningTxt.alpha = 1;
            }


            needGoldTxt.text = "가격 : " + parsingJson.price[i].ToString();
            explainTxt.text = parsingJson.explation[i];
            expName.text = parsingJson.upgradeName[i];
        });
    }

    public void TrainUpgrade(int i)
    {
        switch (i)
        {
            case 0:
                testTurretDatabase.plusDamage += 4;
                ChangeCol(i);
                break;
            case 1:
                testTurretDatabase.plusDef += 1;
                ChangeCol(i);
                break;
            case 2:
                testTurretDatabase.plusRedDamage += 1;
                ChangeCol(i);
                break;
            case 3:
                testTurretDatabase.plusMaxHp += 10;
                ChangeCol(i);
                break;
            case 4:
                if (testTurretDatabase.plusRecoverAmount == 0)
                {
                    testTurretDatabase.plusRecoverAmount += 0.1f;
                    ChangeCol(i);
                }
                else if (testTurretDatabase.plusRecoverAmount == 0.1f)
                {
                    testTurretDatabase.plusRecoverAmount += 0.1f;
                    ChangeCol(i);
                }
                else if (testTurretDatabase.plusRecoverAmount == 0.2f)
                {
                    testTurretDatabase.plusRecoverAmount += 0.3f;
                    ChangeCol(i);
                }
                break;
            case 5:
                testTurretDatabase.plusDistance += 5f;
                ChangeCol(i);
                break;
            case 6:
                testTurretDatabase.plusBuffTime += 3f;
                ChangeCol(i);
                break;
            case 7:
                testTurretDatabase.plustTurretDistance += 10f;
                ChangeCol(i);
                break;
            case 8:
                testTurretDatabase.plusReload += 8;
                ChangeCol(i);
                break;
            case 9:
                testTurretDatabase.plusLuck += 10f;
                ChangeCol(i);
                break;
            case 10:
                testTurretDatabase.plusSpeed += 10f;
                ChangeCol(i);
                break;
            case 11:
                testTurretDatabase.plusRepair += 2f;
                ChangeCol(i);
                break;
            case 12:
                testTurretDatabase.trainInfo.trainCount += 1;
                ChangeCol(i);
                break;
            case 13:
                testTurretDatabase.plusCurse += 10;
                ChangeCol(i);
                break;
            case 14:
                testTurretDatabase.plusJesus += 1;
                ChangeCol(i);
                break;
            default:
                break;
        }
    }

    public void ChangeCol(int i)
    {
        statList[i].transform.GetChild(2).GetChild(parsingJson.count[i]).GetComponent<Image>().color = new Color(1, 1, 0);
    }
}
