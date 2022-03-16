using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    private static UIManager _ui = new UIManager();
    public static UIManager UI { get { return _ui; } }
    [SerializeField] private GameObject upGradePanel;
    [Space]
    [SerializeField] private Slider hpBar;
    [Space]
    public bool openPanel = false;
    public bool isCreate = false;   
    public List<Button> panelOpenList = new List<Button>();

    [SerializeField]
    private Text scrapTxt;
    public GameObject installPanel;
    public int scrapAmount = 0;
    public int needAmount = 5;

    //public float moveValue;
    //public float minusMoveValue;
    public GameObject panelPos;
    public GameObject panelHidePos;

    public Button destroyBtn;
    public Button upgradeBtn;
    public Button repairBtn;

    public Button speedBtn;
    public Text speedTxt;
    private int speedBtnCount;

    public Text upgradeCostTxt;


    private void Awake()
    {
        _ui = this;
    }
    private void Start()
    {
        //hp바 세팅
        hpBar.value = (float)TrainScript.instance.curTrainHp / (float)TrainScript.instance.maxTrainHp;
        speedBtn.onClick.AddListener(ChangeSpeed);
        CheckScrapAmount();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(openPanel)
            {
                openPanel = false; 
            }
            else if(!openPanel)
            {
                openPanel = true;
                installPanel.SetActive(true);
            }
        }
        PanelOpen();
        TakeDamageHpBar();
    }


    //패널 오픈 함수
    public void PanelOpen()
    {
        if (openPanel)
        {
            PanelMove(installPanel, panelPos.transform.position.z);
            PanelMove(upGradePanel, panelPos.transform.position.z);
        }
        if (openPanel == false)
        {
            PanelMove(installPanel, panelHidePos.transform.position.z);
            PanelMove(upGradePanel, panelHidePos.transform.position.z);
        }
    }

    public void PanelMove(GameObject panel, float endValue)
    {
        panel.transform.DOMoveZ(endValue, 1.2f);
    }
    public void TakeDamageHpBar()
    {
        //Time.deltaTime 옆에 * (TakeDamage) 만큼 곱해줘야함. 생략되어 있음.
        hpBar.value = Mathf.Lerp(hpBar.value, (float)TrainScript.instance.curTrainHp / (float)TrainScript.instance.maxTrainHp, Time.deltaTime);
    }

    public void CheckScrapAmount()
    {
        scrapTxt.text = scrapAmount.ToString();
    }

    public void SelectTurret(GameObject turret)
    {
        PlayerInput.Instance.curTurret = turret;
        isCreate = true;
        openPanel = false;
    }

    public void GetTurretAmount(int needAmount)
    {
        this.needAmount = needAmount;
    }

    public void ChangeSpeed()
    {
        speedBtnCount++;
        switch (speedBtnCount)
        {
            case 0:
                GameManager.Instance.gameSpeed = 1f;
                speedTxt.text = GameManager.Instance.gameSpeed + "X";
                break;
            case 1:
                GameManager.Instance.gameSpeed = 1.5f;
                speedTxt.text = GameManager.Instance.gameSpeed + "X";
                break;
            case 2:
                GameManager.Instance.gameSpeed = 2f;
                speedTxt.text = GameManager.Instance.gameSpeed + "X";
                break;
            case 3:
                GameManager.Instance.gameSpeed = 4f;
                speedTxt.text = GameManager.Instance.gameSpeed + "X";
                speedBtnCount = -1;
                break;
            default:
                break;
        }
    }

    public int GetNeedAmount()
    {
        return needAmount;
    }
}
