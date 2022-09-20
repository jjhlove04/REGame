using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TitleInterfaceManager : MonoBehaviour
{
    public static TitleInterfaceManager Instance = new TitleInterfaceManager();
    
    public int sceneIndex = 0;
    bool canEscape = false;
    
    [Header("타이틀 - 버튼")]
    public RectTransform btnGroup;
    // 0 : 시작, 1 : 업그레이드, 2 : 상태, 3 : 도감, 4 : 설정, 5 : 종료
    public Button[] titleBtn;

    // 0 : 시작 - 출발, 1 : 업그레이드 - 기차, 2 : 업그레이드 - 타워, 3 : 업그레이드 - 아이템
    public Button[] inTitleBtn;


    public Button backBtn;
    [Header("타이틀 - 시퀀스")]
    Sequence titleStart;
    Sequence titleEnd;  

    [Header("타이틀 - 플레이어")]
    public GameObject playerCard;

    [Header("타이틀 - 캔버스")]
    //0 : 출발 캔버스, 1 : 업그레이드 캔버스, 2 : 도감 캔버스
    public GameObject[] canvasGroup;

    [Header("업그레이드 - 오브젝트")]
    public GameObject fadeImg;
    public GameObject[] upPanel;
    

    private void Awake() {
        //최초 한번만 실행
        btnGroupAction(0);

        //버튼 애드리스너 시작
        titleBtn[0].onClick.AddListener(()=> {
            canvasGroup[0].SetActive(true);
            btnGroupAction(1);
            inTitleBtn[0].GetComponent<Text>().DOFade(1,0.5f);
            sceneIndex = 1;   
            backBtn.gameObject.SetActive(true);         

        });
        titleBtn[1].onClick.AddListener(()=> {
            canvasGroup[1].SetActive(true);
            btnGroupAction(1);
            sceneIndex = 2;
            IntitleAction(0);
            backBtn.gameObject.SetActive(true);         

        });
        titleBtn[2].onClick.AddListener(()=> {
            //
            //출발 버튼 액티브 온

        });

        inTitleBtn[1].onClick.AddListener(()=>
        {

        });

        backBtn.onClick.AddListener(()=>
        {
            EscapeBtn();
        });
        
        //버튼 애드리스너 끝
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            EscapeBtn();
        }
    }

    public void btnGroupAction(int index)
    {
        
        //타이틀 들어오기
        if(index == 0) 
        {
            titleStart.Kill();
            titleStart.Append(btnGroup.DOAnchorPosX(70,0.4f));
            titleStart.Append(playerCard.GetComponent<RectTransform>().DOAnchorPosX(-70,0.6f));
            titleStart.SetAutoKill(false);
        }
        //타이틀 뒤로가기
        if(index == 1) 
        {
            titleEnd.Kill();
            titleEnd.Append(btnGroup.DOAnchorPosX(-340,0.4f));
            titleEnd.Append(playerCard.GetComponent<RectTransform>().DOAnchorPosX(570,0.6f).OnComplete(()=>
            {
                canEscape = true;
            }));
            titleEnd.SetAutoKill(false);
        }

        
    }

    public void IntitleAction(int index)
    {
        Sequence upBtnShow = DOTween.Sequence();
        Sequence upBtnClose = DOTween.Sequence();
        Sequence upPanelShow = DOTween.Sequence();
        if(index == 0)
        {
            upBtnShow.Append(inTitleBtn[1].GetComponent<Text>().DOFade(1,0.4f));
            upBtnShow.Append(inTitleBtn[2].GetComponent<Text>().DOFade(1,0.4f));
            upBtnShow.Append(inTitleBtn[3].GetComponent<Text>().DOFade(1,0.4f).OnComplete(()=>{
                canEscape = true;
            }));
            upBtnShow.SetAutoKill(false);
        }
        if(index == 1)
        {
            upBtnClose.Append(inTitleBtn[1].GetComponent<Text>().DOFade(0,0.2f));
            upBtnClose.Join(inTitleBtn[2].GetComponent<Text>().DOFade(0,0.2f));
            upBtnClose.Join(inTitleBtn[3].GetComponent<Text>().DOFade(0,0.2f));
            upBtnClose.SetAutoKill(false);
        }
        if(index ==3)
        {
            upPanelShow.Append(fadeImg.GetComponent<Image>().DOFade(1,0.4f).OnComplete(()=>{
                upPanel[0].SetActive(true);
                upPanelShow.Append(fadeImg.GetComponent<Image>().DOFade(0,0.4f));
            }));
            upPanelShow.SetAutoKill(false);
        }
    }

    public void EscapeBtn()
    {
        if(sceneIndex == 1 && canEscape)
        {
            btnGroupAction(0);
            canvasGroup[0].SetActive(false);
            canEscape = false;
            backBtn.gameObject.SetActive(false);         
        }
        if(sceneIndex == 2 && canEscape)
        {
            btnGroupAction(0);
            canvasGroup[1].SetActive(false);
            canEscape = false;
            backBtn.gameObject.SetActive(false);         
        }
    }
}
