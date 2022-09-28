using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.Timeline;


public class TitleMoveScript : MonoBehaviour
{
    public ItemContainer itemContainer;

    // 0: 스타트 버튼 . 1: 업그레이드 버튼, 2: 통계 버튼 , 3: 도감 버튼, 4: 설정 버튼, 5: 종료 버튼, 6: 이전화면
    public Button[] titleActionBtn;
    //0: start버튼 눌렀을 때 실행되는 타임라인, 1: 뒤로가기 눌렀을떄 실행되는 타임라인, 2: 업그레이드 눌렀을때 실행되는 타임라인 , 3:업그레이드에서 뒤로가기, 4: 블러 타임라인, 5:블러종료
    public PlayableDirector[] timelines;
    
    [Header("버튼 그룹")]
    [SerializeField] private GameObject btnGroup;
    [SerializeField] private RectTransform btnGroupRect;
    [SerializeField] private GameObject backBtn;
    [SerializeField] private GameObject collectionPanel;
    [SerializeField] private Button repairBtn;
    [SerializeField] private GameObject resultPanel;
    private RectTransform resultPanelRect;

    public static int indexNum = 0;

    public GameObject startC;
    public GameObject turretC;
    public GameObject turretP;
    public GameObject towerP;
    public bool isback = false;
    float backTime = 0;


    private void Awake() 
    {
        repairBtn = repairBtn.GetComponent<Button>();
        btnGroupRect = btnGroup.GetComponent<RectTransform>();
        resultPanelRect = resultPanel.GetComponent<RectTransform>();

        isback = true;

        //시작버튼
        titleActionBtn[0].onClick.AddListener(() => {
            //timelines[0].Play();
            indexNum = 1;
            TitleUI.UI.titleBack = true;
            BtnSlide(1);
            TitleUI.UI.ReadySetUpPanel(1);
            if(TutorialManager._instance.isFirstTutorial)
            {
                if(TutorialManager._instance!=null)
                    TutorialManager._instance.ProcessTutorial(TutorialManager._instance.processIndex);
                TutorialManager._instance.processIndex++;
            }

            isback = false;
        });

        if (TestTurretDataBase.Instance.isfirst)
        {
            titleActionBtn[1].interactable = false;
        }
        //업그레이드 버튼
        titleActionBtn[1].onClick.AddListener(()=> {
            TitleUI.UI.UpGradePanelOpen(1);
            indexNum = 2;
            TitleUI.UI.titleBack = true;
            BtnSlide(1);

            isback = false;
        });
         //통계 버튼
        titleActionBtn[2].onClick.AddListener(()=> {
            
            timelines[4].Play();
            indexNum = 3;
            BtnSlide(1);
            backBtn.SetActive(true);

            isback = false;
        });
         //컬렉션 버튼
        titleActionBtn[3].onClick.AddListener(()=> {
        
            timelines[4].Play();
            indexNum = 3;
            BtnSlide(1);
            backBtn.SetActive(true);

            isback = false;
        });
         //설정 버튼
        titleActionBtn[4].onClick.AddListener(()=> {
            
            timelines[4].Play();
            indexNum = 3;
            BtnSlide(1);
            TitleUI.UI.titleBack = true;
            
            isback = false;
        }); 
          //종료 버튼
        titleActionBtn[5].onClick.AddListener(()=> {
            indexNum = 3;
            BtnSlide(1);
            Application.Quit();
        }); 
        //이전화면 버튼
        titleActionBtn[6].onClick.AddListener(() =>{
            InputBackBtn();
        });

        //수리버튼 눌렀을때
        repairBtn.onClick.AddListener(() =>
        {
            btnGroup.SetActive(true);
            resultPanelRect.DOAnchorPosX(1742, 0.8f);
            btnGroupRect.DOAnchorPosX(70, 0.5f);
            TitleUI.UI.playerCard.DOAnchorPosX(-18, 0.5f);

            TestTurretDataBase.Instance.resultEXP = 0;
            TestTurretDataBase.Instance.killEnemy = 0;
            TestTurretDataBase.Instance.resultDamage = 0;
            TestTurretDataBase.Instance.resultGold -= (int)((TestTurretDataBase.Instance.round - 1) * 1.5f);
            /*
            (TestTurretDataBase.Instance.trainCount * (TestTurretDataBase.Instance.round - 1)) +
            ((TestTurretDataBase.Instance.round - 1) * (TestTurretDataBase.Instance.round - 1));
            */

            for (int i = 0; i < TestTurretDataBase.Instance.postItemObj.Count; i++)
            {
                TestTurretDataBase.Instance.postItemObj[i].curCarry = 0;

            }

            TestTurretDataBase.Instance.isfirst = false;
            if (!TestTurretDataBase.Instance.isfirst)
            {
                titleActionBtn[1].interactable = true;
            }
        });

        if (InGameUII.sceneIndex == 1)
        {
            btnGroup.SetActive(false);
            btnGroupRect.DOAnchorPosX(-600, 0.1f);
            resultPanel.SetActive(true);
        }
        if (InGameUII.sceneIndex == 0)
        {
            btnGroup.SetActive(true);
            resultPanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (!isback)
        {
            titleActionBtn[0].interactable = false;
            titleActionBtn[1].interactable = false;
            titleActionBtn[3].interactable = false;
            titleActionBtn[5].interactable = false;
            backTime += Time.deltaTime;
            if (backTime >= 1.1f)
            {
                if(!TestTurretDataBase.Instance.isfirst)
                {
                    titleActionBtn[1].interactable = true;
                }
                titleActionBtn[0].interactable = true;
                titleActionBtn[3].interactable = true;
                titleActionBtn[5].interactable = true;
                isback = true;
                backTime = 0f;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && isback)
        {
            if (!itemContainer.isOnPop)
            {
                EscapeFunc();
            }
            else if(itemContainer.isOnPop)
            {
                itemContainer.CloseFunc();
            }
        }

        if(Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log(indexNum);
        }
    }

    void EscapeFunc()
    {
        if (indexNum == 1)
        {
            //timelines[1].Play();
            TitleUI.UI.ReadySetUpPanel(2);
            BtnSlide(2);
            backBtn.SetActive(false);
            TitleUI.UI.titleBack = false;
            indexNum = 0;

            isback = false;
        }
        if (indexNum == 2)
        {
            for (int i = 0; i < 4; i++)
            {
                TitleUI.UI.upGradeBtns[i].gameObject.SetActive(false);
            }

            //timelines[3].Play();
            BtnSlide(2);
            TitleUI.UI.titleBack = false;
            indexNum = 0;

            isback = false;

        }
        if (indexNum == 3)
        {
            timelines[5].Play();
            BtnSlide(3);
            collectionPanel.SetActive(false);
            indexNum = 0;

            isback = false;
        }

        //업그레이드 화면에서 나가기
        if (indexNum == 4)
        {
            for (int i = 0; i < 4; i++)
            {
                TitleUI.UI.upGradePanels[i].SetActive(false);
                TitleUI.UI.upGradeBtns[i].gameObject.SetActive(true);
            }
            indexNum = 2;

            isback = false;
        }
    }

    public void InputBackBtn()
    {
        if (TitleUI.UI.titleBack == true)
            {
                if (indexNum == 1)
                {
                    //timelines[1].Play();
                    TitleUI.UI.ReadySetUpPanel(2);
                    BtnSlide(2);
                    backBtn.SetActive(false);
                    TitleUI.UI.titleBack = false;
                }
                if (indexNum == 2)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        TitleUI.UI.upGradeBtns[i].gameObject.SetActive(false);
                    }

                    //timelines[3].Play();
                    BtnSlide(2);
                    TitleUI.UI.titleBack = false;

                }
                if (indexNum == 3)
                {
                    timelines[5].Play();
                    BtnSlide(3);
                    collectionPanel.SetActive(false);
                }
                //업그레이드 화면에서 나가기
                if (indexNum == 4)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        TitleUI.UI.upGradePanels[i].SetActive(false);
                        TitleUI.UI.upGradeBtns[i].gameObject.SetActive(true);
                    }
                    indexNum = 2;
                }
            }
            // if (startC.activeSelf == true || turretC.activeSelf == true || turretP.activeSelf == true || towerP.activeSelf == true)
            // {
            //     backBtn.SetActive(true);
            // }
            // else
            // {
            //     backBtn.SetActive(false);
            // }
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
        if(index == 3 || Input.GetKeyDown(KeyCode.Escape))
        {
            BtnGroupMove(2);
            backBtn.SetActive(false);
        }
    }

    /// <summary>
    ///버튼 그룹 이동시키는 기본 함수 0: 밖으로 보내기, 1: 안으로 들어오기
    ///</summary>
    public void BtnGroupMove(int index)
    {
        float dexSpeed = 0;
        if (index == 0)
        {
            btnGroupRect.DOAnchorPosX(-578, 0.5f);
            TitleUI.UI.playerCard.DOAnchorPosX(547, 0.5f);
        }
        //출발 업그레이드 백
        if(index == 1)
        {
            dexSpeed = 0.5f;
            btnGroupRect.DOAnchorPosX(70,dexSpeed).SetDelay(0.5f);
            TitleUI.UI.playerCard.DOAnchorPosX(-18, 0.5f).SetDelay(0.5f);
        } 
        //나머지 버튼 3개 백
        if(index == 2)
        {
            dexSpeed = 0.3f;
            btnGroupRect.DOAnchorPosX(70,dexSpeed).SetDelay(0.4f);
            TitleUI.UI.playerCard.DOAnchorPosX(-18, 0.3f).SetDelay(0.5f);
        }
        
    }
    public void BackMove()
    {
        if (indexNum == 1)
        {
            TitleUI.UI.ReadySetUpPanel(2);
            //timelines[1].Play();
            BtnSlide(2);

        }
        if (indexNum == 2)
        {
            TitleUI.UI.UpGradePanelOpen(2);
            for (int i = 0; i < 4; i++)
            {
                TitleUI.UI.upGradeBtns[i].gameObject.SetActive(false);
            }
            //timelines[3].Play();
            BtnSlide(2);
        }
        if (indexNum == 3)
        {
            //timelines[5].Play();
            BtnSlide(3);
        }
        //업그레이드 화면에서 나가기
        if (indexNum == 4)
        {
            for (int i = 0; i < 3; i++)
            {
                TitleUI.UI.upGradePanels[i].SetActive(false);
                TitleUI.UI.upGradeBtns[i].gameObject.SetActive(true);
            }
            indexNum = 2;

        }
    }
}
