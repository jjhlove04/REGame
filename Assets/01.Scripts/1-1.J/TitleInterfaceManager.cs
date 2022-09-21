using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TitleInterfaceManager : MonoBehaviour
{
    public static TitleInterfaceManager Instance = new TitleInterfaceManager();
    
    public int sceneIndex = 0;
    public bool canEscape = false;
    //세부타이틀 들어갔다 나올때.
    public bool canSubEscape = false;
    
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
    bool isNowTitel = false;
    public GameObject fadeImg;
    public GameObject[] upPanel;
    

    private void Awake() {
        Sequence first = DOTween.Sequence();
        first.Append(fadeImg.transform.DOScale(1,0.1f));
        first.Append(fadeImg.transform.DOScale(0,0.1f));

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

        inTitleBtn[1].onClick.AddListener(()=> {
            IntitleAction(2);
            isNowTitel = true;
            sceneIndex = 3; 
        });
        inTitleBtn[2].onClick.AddListener(()=> {
            IntitleAction(4);
            isNowTitel = true;
            sceneIndex = 4; 
        });
        inTitleBtn[3].onClick.AddListener(()=> {
            IntitleAction(6);
            isNowTitel = true;
            sceneIndex = 5; 
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
        Sequence upPanel1Show = DOTween.Sequence();
        Sequence upPanel2Show = DOTween.Sequence();
        Sequence upPanel3Show = DOTween.Sequence();
        Sequence upPanel1Close = DOTween.Sequence();
        Sequence upPanel2Close = DOTween.Sequence();
        Sequence upPanel3Close = DOTween.Sequence();
        if(index == 0)
        {
            upBtnShow.Append(inTitleBtn[1].GetComponent<Text>().DOFade(1,0.3f));
            upBtnShow.Append(inTitleBtn[2].GetComponent<Text>().DOFade(1,0.3f));
            upBtnShow.Append(inTitleBtn[3].GetComponent<Text>().DOFade(1,0.3f).OnComplete(()=>{
                canEscape = true;
            }));
            upBtnShow.SetAutoKill(false);
        }
        if(index == 1)
        {
            upBtnClose.Append(inTitleBtn[1].GetComponent<Text>().DOFade(0,0.2f));
            upBtnClose.Join(inTitleBtn[2].GetComponent<Text>().DOFade(0,0.2f));
            upBtnClose.Join(inTitleBtn[3].GetComponent<Text>().DOFade(0,0.2f).OnComplete(()=> 
            {
                    canvasGroup[1].SetActive(false);
                
            }));
            upBtnClose.SetAutoKill(false);
        }

        //up기차 들어가기
        if(index == 2)
        {
            upPanel1Show.Append(fadeImg.transform.DOScale(1,0.1f));
            upPanel1Show.Append(fadeImg.GetComponent<Image>().DOFade(1,0.4f).OnComplete(()=>{
                IntitleAction(9);
                upPanel[0].SetActive(true);
                upPanel1Show.Append(fadeImg.GetComponent<Image>().DOFade(0,0.4f).OnComplete(()=>{
                    upPanel1Show.Append(fadeImg.transform.DOScale(0,0.1f).OnComplete(()=>
                    {
                        canEscape = false;
                        canSubEscape = true;
                    }));
                }));
            }));
            sceneIndex = 3;
            upPanel1Show.SetAutoKill(false);
        }
        //up기차에서 나오기
        if(index == 3)
        {
            upPanel1Close.Append(fadeImg.transform.DOScale(1,0.1f));
            upPanel1Close.Append(fadeImg.GetComponent<Image>().DOFade(1,0.4f).OnComplete(()=> 
            {
                upPanel[0].SetActive(false);
                upPanel1Close.Append(fadeImg.GetComponent<Image>().DOFade(0,0.4f).OnComplete(()=>
                {
                    upPanel1Close.Append(fadeImg.transform.DOScale(0,0.1f));
                    IntitleAction(0);
                }));
                sceneIndex = 2;
            }));
            upPanel1Close.SetAutoKill(false);

        }
        //up 타워 패널 들어가기
        if(index == 4)
        {
            upPanel2Show.Append(fadeImg.transform.DOScale(1,0.1f));
            upPanel2Show.Append(fadeImg.GetComponent<Image>().DOFade(1,0.4f).OnComplete(()=>{
                IntitleAction(9);
                upPanel[1].SetActive(true);
                upPanel2Show.Append(fadeImg.GetComponent<Image>().DOFade(0,0.4f).OnComplete(()=>{
                    upPanel2Show.Append(fadeImg.transform.DOScale(0,0.1f).OnComplete(()=>
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
        if(index == 5)
        {
            upPanel2Close.Append(fadeImg.transform.DOScale(1,0.1f));
            upPanel2Close.Append(fadeImg.GetComponent<Image>().DOFade(1,0.4f).OnComplete(()=> 
            {
                upPanel[1].SetActive(false);
                Debug.Log("패널 1 닫힘");

                upPanel2Close.Append(fadeImg.GetComponent<Image>().DOFade(0,0.4f).OnComplete(()=>
                {
                    upPanel2Close.Append(fadeImg.transform.DOScale(0,0.1f));
                    IntitleAction(0);
                }));
                sceneIndex = 2;
            }));
            upPanel2Close.SetAutoKill(false);

        }

        //up 아이템 뽑기
        if(index == 6)
        {
            upPanel3Show.Append(fadeImg.transform.DOScale(1,0.1f));
            upPanel3Show.Append(fadeImg.GetComponent<Image>().DOFade(1,0.4f).OnComplete(()=>{
                IntitleAction(9);
                upPanel[2].SetActive(true);
                upPanel3Show.Append(fadeImg.GetComponent<Image>().DOFade(0,0.4f).OnComplete(()=>{
                    upPanel3Show.Append(fadeImg.transform.DOScale(0,0.1f).OnComplete(()=>
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
        if(index == 7)
        {
            upPanel3Close.Append(fadeImg.transform.DOScale(1,0.1f));
            upPanel3Close.Append(fadeImg.GetComponent<Image>().DOFade(1,0.4f).OnComplete(()=> 
            {
                upPanel[2].SetActive(false);
                Debug.Log("패널 2 닫힘");
                upPanel3Close.Append(fadeImg.GetComponent<Image>().DOFade(0,0.4f).OnComplete(()=>
                {
                    upPanel3Close.Append(fadeImg.transform.DOScale(0,0.1f));
                    IntitleAction(0);
                }));
                sceneIndex = 2;
            }));
            upPanel3Close.SetAutoKill(false);

        }


        //캔버스 살아있는 업버튼 트위닝 => 세부 패널 들어갔다가 나올때만 호출
        if(index == 9)
        {
            upBtnClose.Append(inTitleBtn[1].GetComponent<Text>().DOFade(0,0.2f));
            upBtnClose.Join(inTitleBtn[2].GetComponent<Text>().DOFade(0,0.2f));
            upBtnClose.Join(inTitleBtn[3].GetComponent<Text>().DOFade(0,0.2f).OnComplete(()=> 
            {
            }));
            upBtnClose.SetAutoKill(false);
        }
    }

    public void EscapeBtn()
    {
        //출발 나오기
        if(sceneIndex == 1 && canEscape)
        {
            btnGroupAction(0);
            canvasGroup[0].SetActive(false);
            canEscape = false;
            backBtn.gameObject.SetActive(false);         
        }
        //업그레이드 창 나가기
        if(sceneIndex == 2 && canEscape)
        {
            btnGroupAction(0);
            IntitleAction(1);
            canEscape = false;
            backBtn.gameObject.SetActive(false);
            isNowTitel = false;      
        }
        //업그레이드 세부 창1 나가기 
        if(sceneIndex == 3 && canSubEscape)
        {   
            canSubEscape = false;
            IntitleAction(3);

        }
        //업그레이드 세부 창2 나가기 

        if(sceneIndex == 4 && canSubEscape)
        {   
            canSubEscape = false;
            IntitleAction(5);

        }
        //업그레이드 세부 창2 나가기 

        if(sceneIndex == 5 && canSubEscape)
        {   
            canSubEscape = false;
            IntitleAction(7);

        }
    }
}
