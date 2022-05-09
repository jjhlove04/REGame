using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class InGameUII : MonoBehaviour
{
    public static InGameUII _instance = new InGameUII();
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
    [SerializeField] private GameObject giveUPPanel;
    [SerializeField] private GameObject settingPanel;

    public List<Text> turPriceList = new List<Text>();

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

    [SerializeField]
    public GameObject dashiObj;

    public GameObject moneyPos;
    public Transform wavePos;
    public GameObject incomMoney;
    public float moneyX;



    Coroutine coroutine;

    testScripttss testScriptts;

    private ObjectPool objectPool;
    private void Awake()
    {
        _instance = this;
        bpBot = bluePrintBot.GetComponent<RectTransform>();
        upGradePanelRect = upGradePanel.GetComponent<RectTransform>();
        objectPool = FindObjectOfType<ObjectPool>();

        upGradePanelBackBtn.onClick.AddListener(() =>
        {
            upGradePanelRect.DOAnchorPosX(-200, 1.5f).SetUpdate(true);
            testScriptts.UnSelectTurret();
        });
        upGradeBtn.onClick.AddListener(() =>
        {
            TestTurretDataBasee.Instance.Upgrade();
        });
    }

    void Start()
    {
        testScriptts = testScripttss.Instance;

        mainMenuBtn.onClick.AddListener(() =>
        {
            num *= -1;
        });

        goldAmounTxt.text = GameManagerr.Instance.goldAmount.ToString();
        waveTxt.text = "WAVE : " + (SpawnMananger.Instance.round - 1).ToString();
        ShowTurPrice();
    }


    void Update()
    {
        goldAmounTxt.text = GameManagerr.Instance.goldAmount.ToString();
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

        if (GameManagerr.Instance.state == GameManagerr.State.End)
        {
            Time.timeScale = 1f;
            TestDatabase.Instance.resultEXP += GameManagerr.Instance.expAmount;
            TestDatabase.Instance.resultGold += GameManagerr.Instance.goldAmount / 30;

            LoadingSceneUI.LoadScene("TitleSceneKIm");
            sceneIndex = 1;
            GameManagerr.Instance.state = GameManagerr.State.Ready;
        }

        if (warningTxt.color.a >= 0)
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
        upGradePanelRect.DOAnchorPosX(-200, 1.5f).SetUpdate(true);
    }

    public void CancleAll()
    {
        ClosePresetBtn();

        upGradePanelRect.DOAnchorPosX(-200, 1.5f).SetUpdate(true);

        testScriptts.turType = -1;
        testScriptts.turPos = -1;
        testScriptts.UnSelectTurret();
    }



    public void backPanelOpne(int backIndex)
    {
        if (backIndex == -1)
        {
            Time.timeScale = 0;
            GameManagerr.Instance.state = GameManagerr.State.Stop;
            stopPanelRect.DOScale(new Vector3(1, 1, 1), 0.8f).SetUpdate(true);
        }
        if (backIndex == 1)
        {
            Time.timeScale = 1;
            GameManagerr.Instance.state = GameManagerr.State.Play;
            stopPanelRect.DOScale(new Vector3(0, 0, 0), 0.8f).SetUpdate(true);
        }

    }
    public void backBtn()
    {
        backIndex *= -1;
        backPanelOpne(backIndex);
    }

    public void PresetBtn()
    {
        if (bpTop.position.x == -300)
        {
            bpTop.DOAnchorPosX(300, 0.8f).SetUpdate(true);
        }

        else if (bpTop.position.x == 300)
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
        SceneManager.LoadScene("TitleSceneKIM");

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
            turPriceList[i].text = GameManagerr.Instance.turretPtice.ToString();
        }
    }

    public void CreateMonjeyTxt(int goldAmount)
    {
        GameObject prefab = objectPool.GetObject(incomMoney);
        prefab.transform.parent = wavePos;
        prefab.transform.position = new Vector2(wavePos.position.x + moneyX, wavePos.position.y);
        prefab.TryGetComponent<Text>(out Text txt);
        txt.color = new Color(1f, 0.9f, 0f, 1f);
        txt.text = "+" + goldAmount.ToString();
    }

    public void CreateOutMoney(int goldAmount)
    {
        GameObject prefab = objectPool.GetObject(incomMoney);
        prefab.transform.parent = wavePos;
        prefab.transform.position = new Vector2(wavePos.position.x + moneyX, moneyPos.transform.position.y);
        prefab.TryGetComponent<Text>(out Text txt);
        txt.color = new Color(1f, 0f, 0f, 1f);
        txt.text = "-" + goldAmount.ToString();
    }
}
