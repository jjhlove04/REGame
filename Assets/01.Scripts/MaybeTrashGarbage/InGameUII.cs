using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System;

public class InGameUII : MonoBehaviour
{
    public static InGameUII _instance = new InGameUII();
    public static int sceneIndex = 0;

    int index = 1;
    int num = 1;
    int backIndex = 1;
    private List<RectTransform> menuBtnList = new List<RectTransform>();
    [SerializeField] private GameObject stopPanel;
    [SerializeField] private RectTransform stopPanelRect;
    [HideInInspector] public RectTransform upGradePanelRect;

    [SerializeField] private Button upGradePanelBackBtn;
    [SerializeField] private Text time;
    [SerializeField] private GameObject[] selectObj;
    [SerializeField] private GameObject giveUPPanel;
    [SerializeField] private GameObject settingPanel;

    public List<Text> turPriceList = new List<Text>();

    private bool onPanel;

    int screenHeight = Screen.height;
    int screenWidth = Screen.width;
    int applybtnIndex = 0;

    RectTransform bpBot;

    public Text goldAmounTxt;
    public Text killEnemyTxt;
    public Text waveTxt;
    public float milliSec;
    int second;
    int miniute;

    [SerializeField]
    private Button upGradeBtn;
    public CanvasGroup GoldWarning;
    [HideInInspector]
    public Text warningtxt;
    [HideInInspector]
    public Image warningIcon;

    public int selectType;



    [SerializeField]
    public GameObject dashiObj;

    public GameObject moneyPos;
    public Transform downMoneyPos;
    public GameObject incomMoney;
    public float moneyX;



    Coroutine coroutine;

    testScripttss testScriptts;
    GameManager gameManager;

    private ObjectPool objectPool;

    public Button towerActive;

    public List<Button> itemList = new List<Button>();

    private ItemManager itemManager;

    private TowerManager towerManager;

    public Sprite lockSprite;

    [SerializeField]
    private GameObject worldCanvas;

    [SerializeField]
    private List<Button> selectAutoReloadlist = new List<Button>();



    public GameObject[] trainUiObjs;


    public Image cursor;

    [SerializeField]
    private GameObject areaObj;

    private GameObject Obj;

    public GameObject selectPanel;
    public GameObject[] selectPanelBtns;
    public GameObject gariImg;
    private float selectTime = 0;

    private bool onSelect;
    private bool OnSelect
    {
        get { return onSelect; }
        set
        {
            onSelect = value;

            if (gameManager.onNewsOfVictory && !value)
            {
                gameManager.NewsOfVictory();
            }

            if (trainManager.onBurningCoal && !value)
            {
                trainManager.BurningCoal(turretDamage);
            }


        }
    }

    TrainManager trainManager;

    public Image expBar;
    public Image hpBar;
    public Text gameLevel;

    //포탑 스텟 업그레이드 부분
    public int turretDamage = 1;
    private float distance = 30f;
    private float shootTime = 1f;
    private int bulletAmount = 20;
    private int rPrice = 20;

    [Header("���������۰���")]
    public CanvasGroup itemPanel;
    public Image itemBuffPanel;
    public Image itemExPanel;
    public Image buffStrPanel;

    public ParticleSystem noTabParticleObj;
    public ParticleSystem TabParticleObj;
    int particleSpeed = 4;

    public CanvasGroup itemExplainPanel;
    private void Awake()
    {
        _instance = this;
        //        bpBot = bluePrintBot.GetComponent<RectTransform>();
        objectPool = ObjectPool.instacne;
        selectPanel.transform.localScale = Vector3.zero;
        for (int i = 0; i < 3; i++)
        {
            selectPanelBtns[i].transform.localScale = Vector3.zero;
        }

    }

    void Start()
    {
        trainManager = TrainManager.instance;

        towerManager = TowerManager.Instance;

        itemManager = ItemManager.Instance;

        testScriptts = testScripttss.Instance;
        gameManager = GameManager.Instance;

        warningtxt = GoldWarning.transform.GetChild(1).GetComponent<Text>();
        warningIcon = GoldWarning.transform.GetChild(0).GetComponent<Image>();

        goldAmounTxt.text = GameManager.Instance.GoldAmount.ToString();
        killEnemyTxt.text = TestTurretDataBase.Instance.killEnemy.ToString();

        ShowTurPrice();

        if (towerManager != null && towerManager.tower != null)
        {
            Tower tower = towerManager.tower.GetComponent<Tower>();
            towerActive.transform.Find("Icon").GetComponent<Image>().sprite = tower.icon;

            towerActive.onClick.AddListener(() =>
            {
                tower.UseTower();
            });
        }

        else
        {
            towerActive.gameObject.SetActive(false);
        }

        if (itemManager != null && itemManager.gameItems.Count > 0)
        {
            for (int i = 0; i < itemManager.gameItems.Count; i++)
            {
                Item item = itemManager.gameItems[i].GetComponent<Item>();
                itemList[i].gameObject.SetActive(true);
                item.GetItemUI(itemList[i].gameObject);
                itemList[i].transform.Find("Icon").GetComponent<Image>().sprite = item.icon;

                itemList[i].onClick.AddListener(itemManager.gameItems[i].GetComponent<Item>().UseItem);
            }
        }

        else
        {
            foreach (var item in itemList)
            {
                item.gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < TrainScript.instance.traininfo.trainCount; i++)
        {
            trainUiObjs[i].SetActive(true);
        }
    }


    void Update()
    {
        //Cursor.visible = false;

        ExpBar();
        goldAmounTxt.text = gameManager.GoldAmount.ToString();

        killEnemyTxt.text = TestTurretDataBase.Instance.killEnemy.ToString();
        //waveTxt.text = "경과시간 : " + (SpawnMananger.Instance.round - 1).ToString();

        Timer();
        gameLevel.text = "LV : " + gameManager.TrainLevel;
        cursor.transform.position = Input.mousePosition;

        if (Input.GetMouseButtonDown(1))
        {
            CancleAll();
        }

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
            else if (giveUPPanel.activeSelf == true)
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
            TestTurretDataBase.Instance.sceneIndex = 1;
            Time.timeScale = 1f;
            TestTurretDataBase.Instance.resultEXP += ParsingJson.Instnace.changeExp[gameManager.TrainLevel];
            TestTurretDataBase.Instance.resultGold += GameManager.Instance.GoldAmount;
            LoadingSceneUI.LoadScene("RemakeTitle");
            GameManager.Instance.state = GameManager.State.Ready;
        }

        if (GoldWarning.alpha >= 0)
        {
            GoldWarning.alpha = Mathf.Lerp(GoldWarning.alpha, 0, Time.deltaTime * 2);
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

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (dashiObj.activeSelf == false)
            {
                dashiObj.SetActive(true);
            }
            else
            {
                dashiObj.SetActive(false);
            }

        }

        if (TrainScript.instance.destroy)
        {
            worldCanvas.SetActive(false);
        }

        if (Input.GetKey(KeyCode.E))
        {
            itemPanel.alpha = 1;
            itemExplainPanel.alpha = 1;
            ItemExplain();
            BeforeValue();
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            itemPanel.alpha = 0;
            itemExplainPanel.alpha = 0;
        }

        if (OnSelect)
        {
            selectTime += Time.deltaTime;

            if (selectTime >= 1.6f)
            {
                //Time.timeScale = 0;
                //GameManager.Instance.state = GameManager.State.Stop;
                gariImg.SetActive(false);

            }
        }

    }

    void Timer()
    {
        if (!SpawnMananger.Instance.stopSpawn)
        {
            milliSec += Time.unscaledDeltaTime;
        }
        waveTxt.text = string.Format("{0:D2}:{1:D2}:{2:D2}", miniute, second, (int)milliSec);

        if ((int)milliSec > 59)
        {
            milliSec = 0;
            second++;
        }

        if (second > 59)
        {
            second = 0;
            miniute++;
        }
    }

    public void ShowSelectPanel()
    {
        TrainItemManager.Instance.GetRandomItem();
        selectPanel.transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.InExpo).OnComplete(() =>
        {
            selectPanelBtns[0].transform.DOScale(new Vector3(1, 1, 1), 0.3f).SetEase(Ease.InExpo).OnComplete(() =>
            {
                selectPanelBtns[1].transform.DOScale(new Vector3(1, 1, 1), 0.3f).SetEase(Ease.InExpo).OnComplete(() =>
                {
                    selectPanelBtns[2].transform.DOScale(new Vector3(1, 1, 1), 0.3f).SetEase(Ease.InExpo);
                });
            });
        });
    }

    //SelectPanel에 이벤트로 추가되어 있음
    public void CloseSelectPanel()
    {
        selectPanel.transform.DOScale(new Vector3(0, 0, 0), 0.4f).OnComplete(() =>
        {
            selectPanel.transform.localScale = Vector3.zero;
            for (int i = 0; i < 3; i++)
            {
                selectPanelBtns[i].transform.localScale = Vector3.zero;
            }
        });


        selectTime = 0;
        OnSelect = false;
        gariImg.SetActive(true);
        SpawnMananger.Instance.stopSpawn = false;
        BackGround back = FindObjectOfType<BackGround>();
        back.speed = 0.3f;

        gameManager.state = GameManager.State.Play;

        noTabParticleObj.Stop();
        TabParticleObj.Stop();
    }

    public void ClearSelect()
    {
        for (int i = 0; i < 5; i++)
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

        upGradePanelRect.DOAnchorPosX(-200, 1.5f).SetUpdate(true);
    }

    public void CancelTurret()
    {
        testScriptts.turType = -1;
        testScriptts.turPos = -1;
        testScriptts.UnSelectTurret();
    }

    public void CancleAll()
    {
        if (!Input.GetKey(KeyCode.T))
        {
            testScriptts.turType = -1;
            testScriptts.turPos = -1;
            testScriptts.UnSelectTurret();

            CircleTree[] circle = FindObjectsOfType<CircleTree>();

            for (int i = 0; i < circle.Length; i++)
            {
                circle[i].transform.GetChild(0).gameObject.SetActive(false);
                circle[i].transform.GetChild(1).gameObject.SetActive(false);
                circle[i].transform.GetChild(2).gameObject.SetActive(false);
                circle[i].transform.GetChild(3).gameObject.SetActive(false);
            }
        }
    }



    public void backPanelOpne(int backIndex)
    {
        if (backIndex == -1)
        {
            Time.timeScale = 0;
            gameManager.state = GameManager.State.Stop;
            stopPanelRect.DOScale(new Vector3(1, 1, 1), 0.3f).SetUpdate(true);
        }
        if (backIndex == 1)
        {
            Time.timeScale = 1;
            gameManager.state = GameManager.State.Play;
            stopPanelRect.DOScale(new Vector3(0, 0, 0), 0.3f).SetUpdate(true);
        }

    }
    public void backBtn()
    {
        backIndex *= -1;
        backPanelOpne(backIndex);
    }

    //public void PresetBtn()
    //{
    //    if (bpTop.position.x == -300)
    //    {
    //        bpTop.DOAnchorPosX(300, 0.8f).SetUpdate(true);
    //    }

    //    else if (bpTop.position.x == 300)
    //    {
    //        bpTop.DOAnchorPosX(-300, 0.8f).SetUpdate(true);
    //        testScriptts.TurCancle();
    //    }
    //}

    //public void OpenPresetBtn()
    //{
    //    bpTop.DOAnchorPosX(300, 0.8f).SetUpdate(true);
    //}

    //public void ClosePresetBtn()
    //{
    //    bpTop.DOAnchorPosX(-300, 0.8f).SetUpdate(true);
    //}

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

    public void ShowTurPrice()
    {
        for (int i = 0; i < turPriceList.Count; i++)
        {
            turPriceList[i].text = GameManager.Instance.turretPtice + "G";
        }
    }

    public void CreateMonjeyTxt(int goldAmount)
    {
        GameObject prefab = objectPool.GetObject(incomMoney);
        prefab.transform.parent = downMoneyPos;
        prefab.transform.position = new Vector2(downMoneyPos.position.x, downMoneyPos.position.y);
        prefab.TryGetComponent<Text>(out Text txt);
        txt.color = new Color(1f, 0.9f, 0f, 1f);
        txt.text = "+" + goldAmount.ToString();
    }

    public void CreateOutMoney(int goldAmount)
    {
        GameObject prefab = objectPool.GetObject(incomMoney);
        prefab.transform.parent = downMoneyPos;
        prefab.transform.position = new Vector2(downMoneyPos.position.x, moneyPos.transform.position.y);
        prefab.TryGetComponent<Text>(out Text txt);
        txt.color = new Color(1f, 0f, 0f, 1f);
        txt.text = "-" + goldAmount.ToString();
    }

    public List<Button> SetSelectReloadButton()
    {
        return selectAutoReloadlist;
    }

    public void DrawArea(Vector3 size, Vector3 pos)
    {
        Obj = objectPool.GetObject(areaObj);

        Obj.SetActive(true);

        Obj.transform.localScale = size;

        Obj.transform.position = pos;
    }

    public void Drawing(Vector3 pos)
    {
        Obj.transform.position = pos;
    }

    public void ClearArea()
    {
        Obj.SetActive(false);
    }

    public void ItemExplain()
    {
        ItemValueExp(turretDamage,
            TrainScript.instance.recoverAmount,
            TrainScript.instance.trainDef,
            shootTime,
            distance,
            TurretManager.Instance.OnCritical(),
            gameManager.ReturnActivation());

    }

    public void ItemValueExp(int damage, float recover, int def, float shootTime, float distance, float critical, float activate)
    {
        itemExplainPanel.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = damage.ToString();
        itemExplainPanel.transform.GetChild(2).GetChild(1).GetComponent<Text>().text = recover.ToString();
        itemExplainPanel.transform.GetChild(2).GetChild(2).GetComponent<Text>().text = def.ToString();
        itemExplainPanel.transform.GetChild(2).GetChild(3).GetComponent<Text>().text = Math.Round(shootTime, 3).ToString();
        itemExplainPanel.transform.GetChild(2).GetChild(4).GetComponent<Text>().text = distance.ToString();
        itemExplainPanel.transform.GetChild(2).GetChild(5).GetComponent<Text>().text = critical.ToString();
        itemExplainPanel.transform.GetChild(2).GetChild(6).GetComponent<Text>().text = activate.ToString();
    }

    public void BeforeValue()
    {
        itemExplainPanel.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = "+3";
        itemExplainPanel.transform.GetChild(3).GetChild(1).GetComponent<Text>().text = "+0.2";
        itemExplainPanel.transform.GetChild(3).GetChild(2).GetComponent<Text>().text = "+2";
        itemExplainPanel.transform.GetChild(3).GetChild(3).GetComponent<Text>().text = "-" + Math.Round((shootTime * 0.05f), 3);
        itemExplainPanel.transform.GetChild(3).GetChild(4).GetComponent<Text>().text = "+0.2";
        itemExplainPanel.transform.GetChild(3).GetChild(5).GetComponent<Text>().text = "+" + TurretManager.Instance.CriAmount();
        itemExplainPanel.transform.GetChild(3).GetChild(6).GetComponent<Text>().text = "-0";
    }
    public void ExpBar()
    {
        if (gameManager.TrainLevel < 50)
        {
            expBar.fillAmount = Mathf.Lerp(expBar.fillAmount, gameManager.ExpAmount / gameManager.maxExp, Time.deltaTime * (2 + (gameManager.ExpAmount / 500)));

            if (expBar.fillAmount >= 1f)
            {
                gameManager.TrainLevel++;

                TrainScript.instance.LevelUp();
                //gameManager.gameSpeed = 0f; //시간 정지
                SpawnMananger.Instance.stopSpawn = true;
                BackGround back = FindObjectOfType<BackGround>();
                back.speed = 0;



                gameManager.ExpAmount -= gameManager.maxExp;
                if (gameManager.state == GameManager.State.Play)
                {
                    if (CameraManager.Instance.canChangeCamera == true)
                    {
                        noTabParticleObj.Play();
                    }
                    else
                    {
                        TabParticleObj.Play();
                    }
                    OnSelect = true;
                    ShowSelectPanel();
                }

                turretDamage += 3;
                distance += 0.2f;
                shootTime -= (shootTime * 0.05f);
                bulletAmount += 1;
                rPrice += 3;

                for (int i = 0; i < testScriptts.turretData.Count; i++)
                {
                    if (testScriptts.turretData[i].TryGetComponent<Turret>(out Turret tur))
                    {
                        tur.LevelUpDamage(turretDamage, distance, shootTime, bulletAmount, rPrice);
                    }
                }

                if (gameManager.TrainLevel < 21)
                {
                    gameManager.maxExp += 10;
                }
                else if (gameManager.TrainLevel < 41)
                {
                    gameManager.maxExp += 13;
                }
                else if (gameManager.TrainLevel < 100)
                {
                    gameManager.maxExp += 16;
                }


                expBar.fillAmount = 0;
            }
        }
    }
}
