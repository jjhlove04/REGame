using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TitleMoveScript : MonoBehaviour
{
    // 0: 스타트 버튼 . 1: 업그레이드 버튼, 2: 통계 버튼 , 3: 도감 버튼, 4: 설정 버튼, 5: 종료 버튼, 6: 이전화면
    public Button[] titleActionBtn;
    //0: start버튼 눌렀을 때 실행되는 타임라인, 1: 뒤로가기 눌렀을떄 실행되는 타임라인, 2: 업그레이드 눌렀을때 실행되는 타임라인 , 3:업그레이드에서 뒤로가기, 4: 블러 타임라인, 5:블러종료
    public PlayableDirector[] timelines;
    public GameObject blurPanel;
    
    [Header("버튼 그룹")]
    [SerializeField] private GameObject btnGroup;
    [SerializeField] private GameObject backBtn;

    int indexNum = 0;
    
    private void Awake() 
    {
        //시작버튼
        titleActionBtn[0].onClick.AddListener(() => {
            timelines[0].Play();
            indexNum = 1;
            BtnSlide(1);
        });   
       
         //업그레이드 버튼
        titleActionBtn[1].onClick.AddListener(()=> {
            timelines[2].Play();
            indexNum = 2;
            BtnSlide(1);
        });
         //통계 버튼
        titleActionBtn[2].onClick.AddListener(()=> {
            blurPanel.SetActive(true);
            timelines[4].Play();
            indexNum = 3;
            BtnSlide(1);
        });
         //컬렉션 버튼
        titleActionBtn[3].onClick.AddListener(()=> {
            blurPanel.SetActive(true);
            timelines[4].Play();
            indexNum = 3;
            BtnSlide(1);
        });
         //설정 버튼
        titleActionBtn[4].onClick.AddListener(()=> {
            blurPanel.SetActive(true);
            timelines[4].Play();
            indexNum = 3;
            BtnSlide(1);
        }); 
          //종료 버튼
        titleActionBtn[5].onClick.AddListener(()=> {
            indexNum = 3;
            BtnSlide(1);
        }); 
        //이전화면 버튼
        titleActionBtn[6].onClick.AddListener(() =>{
            if(indexNum == 1)
            {
                timelines[1].Play();
            }
            if(indexNum == 2)
            {
                timelines[3].Play();
            }
            if(indexNum == 3)
            {
                timelines[5].Play();
               BtnSlide(3);
            }
            BtnSlide(2);
        }); 
        
    }

/// <summary>모든 버튼 상호작용 관리</summary>
    public void BtnSlide(int index)
    {
        if(index == 1)
        {
            BtnGroupMove(0);
            backBtn.SetActive(true);
        }
        if(index == 2 || Input.GetKeyDown(KeyCode.Escape))
        {
            BtnGroupMove(1);
            backBtn.SetActive(false);
        }
        // if(index == 3 || Input.GetKeyDown(KeyCode.Escape))
        // {
        //     BtnGroupMove(2);
        //     backBtn.SetActive(false);
        // }
    }

    /// <summary>
    ///버튼 그룹 이동시키는 기본 함수 0: 밖으로 보내기, 1: 안으로 들어오기
    ///</summary>
    public void BtnGroupMove(int index)
    {
        float dexSpeed = 0;
        if(index == 0)
        {
            btnGroup.transform.DOMoveX(-50, 0.5f);
        }
        //출발 업그레이드 백
        if(index == 1)
        {
            dexSpeed = 0.5f;
            btnGroup.transform.DOMoveX(350 ,dexSpeed);
        } 
        //나머지 버튼 3개 백
        if(index == 2)
        {
            dexSpeed = 0.3f;
            btnGroup.transform.DOMoveX(350 ,dexSpeed);
        }
        
    }

}
