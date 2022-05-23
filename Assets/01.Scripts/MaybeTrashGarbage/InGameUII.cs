using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class InGameUII : MonoBehaviour
{
    public static InGameUII _instance = new InGameUII();
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
    public Text waveTxt;

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

    private void Awake()
    {
        _instance = this;
        bpBot = bluePrintBot.GetComponent<RectTransform>();
        upGradePanelRect = upGradePanel.GetComponent<RectTransform>();
        objectPool = FindObjectOfType<ObjectPool>();

    }

    void Start()
    {
        towerManager = TowerManager.Instance;

        itemManager = ItemManager.Instance;

        testScriptts = testScripttss.Instance;

        warningtxt = GoldWarning.transform.GetChild(1).GetComponent<Text>();
        warningIcon = GoldWarning.transform.GetChild(0).GetComponent<Image>();

        mainMenuBtn.onClick.AddListener(() =>
        {
            num *= -1;
        });

        goldAmounTxt.text = GameManager.Instance.goldAmount.ToString();
        waveTxt.text = "WAVE : " + (SpawnMananger.Instance.round - 1).ToString();
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
            Time.timeScale = 1f;
            TestDatabase.Instance.resultEXP += GameManager.Instance.expAmount;
            TestDatabase.Instance.resultGold += GameManager.Instance.goldAmount / 30;

            LoadingSceneUI.LoadScene("TitleScene");
            sceneIndex = 1;
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



    public void backPanelOpne(int backIndex)
    {
        if (backIndex == -1)
        {
            Time.timeScale = 0;
            GameManager.Instance.state = GameManager.State.Stop;
            stopPanelRect.DOScale(new Vector3(1, 1, 1), 0.3f).SetUpdate(true);
        }
        if (backIndex == 1)
        {
            Time.timeScale = 1;
            GameManager.Instance.state = GameManager.State.Play;
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
            turPriceList[i].text = GameManager.Instance.turretPtice.ToString();
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
}
