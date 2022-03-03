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


    private void Awake()
    {
        _ui = this;
    }
    private void Start()
    {
        //hp바 세팅
        hpBar.value = (float)TrainScript.instance.curTrainHp / (float)TrainScript.instance.maxTrainHp;
        CheckScrapAmount();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
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
            PanelMove(installPanel, panelPos.transform.position.x);
            PanelMove(upGradePanel, panelPos.transform.position.x);
        }
        if (openPanel == false)
        {
            PanelMove(installPanel, panelHidePos.transform.position.x);
            PanelMove(upGradePanel, panelHidePos.transform.position.x);
        }
    }

    public void PanelMove(GameObject panel, float endValue)
    {
        panel.transform.DOMoveX(endValue, 1.2f);
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

    public int GetNeedAmount()
    {
        return needAmount;
    }
}
