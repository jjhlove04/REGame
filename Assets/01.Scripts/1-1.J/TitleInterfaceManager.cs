using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TitleInterfaceManager : MonoBehaviour
{
    public static TitleInterfaceManager Instance = new TitleInterfaceManager();
    
    [Header("타이틀 - 버튼")]
    public RectTransform btnGroup;
    // 0 : 시작, 1 : 업그레이드, 2 : 상태, 3 : 도감, 4 : 설정, 5 : 종료
    public Button[] titleBtn;

    // 0 : 시작 - 출발, 1 : 업그레이드 - 기차, 2 : 업그레이드 - 타워, 3 : 업그레이드 - 아이템
    public Button[] inTitleBtn;


    [Header("타이틀 - 플레이어")]
    public GameObject playerCard;

    [Header("타이틀 - 캔버스")]
    //0 : 출발 캔버스, 1 : 업그레이드 캔버스, 2 : 도감 캔버스
    public GameObject[] canvasGroup;
    private void Awake() {
        //최초 한번만 실행
        btnGroupAction(0);

        //버튼 애드리스너 시작
        titleBtn[0].onClick.AddListener(()=> {
            //출발 캔버스 액티브 온
            //출발 버튼 액티브 온

        });
        titleBtn[1].onClick.AddListener(()=> {
            //출발 캔버스 액티브 온
            //출발 버튼 액티브 온

        });
        titleBtn[2].onClick.AddListener(()=> {
            //
            //출발 버튼 액티브 온

        });
        
        //버튼 애드리스너 끝
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void btnGroupAction(int index)
    {
        //가장 처음 실행되는 명령문
        if(index == 0) 
        {
            btnGroup.DOAnchorPosX(70,1);
            playerCard.GetComponent<RectTransform>().DOAnchorPosX(-70,0.6f);
        }
    }
}
