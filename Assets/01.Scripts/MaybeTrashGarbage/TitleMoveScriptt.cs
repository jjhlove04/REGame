using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Playables;

public class TitleMoveScriptt : MonoBehaviour
{ // 0: 스타트 버튼 . 1: 업그레이드 버튼, 2: 통계 버튼 , 3: 도감 버튼, 4: 설정 버튼, 5: 종료 버튼, 6: 이전화면
    public Button[] titleActionBtn;
    //0: start버튼 눌렀을 때 실행되는 타임라인, 1: 뒤로가기 눌렀을떄 실행되는 타임라인, 2: 업그레이드 눌렀을때 실행되는 타임라인 , 3:업그레이드에서 뒤로가기, 4: 블러 타임라인, 5:블러종료
    public PlayableDirector[] timelines;
    public GameObject blurPanel;

    [Header("버튼 그룹")]
    [SerializeField] private GameObject btnGroup;
    [SerializeField] private RectTransform btnGroupRect;
    [SerializeField] private GameObject backBtn;
    [SerializeField] private Button repairBtn;
    [SerializeField] private GameObject resultPanel;
    private RectTransform resultPanelRect;

    public static int indexNum = 0;
    private bool isDoneTween = false;

    private void Awake()
    {
        repairBtn = repairBtn.GetComponent<Button>();
        btnGroupRect = btnGroup.GetComponent<RectTransform>();
        resultPanelRect = resultPanel.GetComponent<RectTransform>();

        //시작버튼
        titleActionBtn[0].onClick.AddListener(() => {
            timelines[0].Play();
            indexNum = 1;
            BtnSlide(1);
            TitleUII.UI.ReadySetUpPanel(1);
        });

        //업그레이드 버튼
        titleActionBtn[1].onClick.AddListener(() => {
            timelines[2].Play();
            indexNum = 2;
            BtnSlide(1);
        });
        //통계 버튼
        titleActionBtn[2].onClick.AddListener(() => {
            blurPanel.SetActive(true);
            timelines[4].Play();
            indexNum = 3;
            BtnSlide(1);
            backBtn.SetActive(true);
        });
        //컬렉션 버튼
        titleActionBtn[3].onClick.AddListener(() => {
            blurPanel.SetActive(true);
            timelines[4].Play();
            indexNum = 3;
            BtnSlide(1);
        });
        //설정 버튼
        titleActionBtn[4].onClick.AddListener(() => {
            blurPanel.SetActive(true);
            timelines[4].Play();
            indexNum = 3;
            BtnSlide(1);
        });
        //종료 버튼
        titleActionBtn[5].onClick.AddListener(() => {
            indexNum = 3;
            BtnSlide(1);
            Application.Quit();
        });
        //이전화면 버튼
        titleActionBtn[6].onClick.AddListener(() => {
            if (indexNum == 1)
            {
                TitleUII.UI.ReadySetUpPanel(2);
                timelines[1].Play();
                BtnSlide(2);

            }
            if (indexNum == 2)
            {
                for (int i = 0; i < 3; i++)
                {
                    TitleUII.UI.upGradeBtns[i].gameObject.SetActive(false);
                }

                timelines[3].Play();
                BtnSlide(2);
            }
            if (indexNum == 3)
            {
                timelines[5].Play();
                BtnSlide(3);
            }
            //업그레이드 화면에서 나가기
            if (indexNum == 4)
            {
                for (int i = 0; i < 3; i++)
                {
                    TitleUII.UI.upGradePanels[i].SetActive(false);
                    TitleUII.UI.upGradeBtns[i].gameObject.SetActive(true);
                }
                indexNum = 2;

            }



        });

        //수리버튼 눌렀을때
        repairBtn.onClick.AddListener(() =>
        {
            btnGroup.SetActive(true);
            resultPanelRect.DOAnchorPosX(1742, 0.8f);
            btnGroupRect.DOAnchorPosX(70, 0.5f);


            TestTurretDataBase.Instance.resultEXP = 0;
            TestTurretDataBase.Instance.resultDamage = 0;
            TestTurretDataBase.Instance.killEnemy = 0;
            TestTurretDataBase.Instance.resultDamage = 0;
            TestTurretDataBase.Instance.createPrice = 0;
        });

        if (InGameUI.sceneIndex == 1)
        {
            btnGroup.SetActive(false);
            btnGroupRect.DOAnchorPosX(-600, 0.1f);
            resultPanel.SetActive(true);
        }
        if (InGameUI.sceneIndex == 0)
        {
            btnGroup.SetActive(true);
            resultPanel.SetActive(false);
        }


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (backBtn.activeSelf == true && isDoneTween)
            {
                if (indexNum == 1)
                {
                    timelines[1].Play();
                    BtnSlide(2);
                    isDoneTween = false;
                }
                if (indexNum == 2)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        TitleUII.UI.upGradeBtns[i].gameObject.SetActive(false);
                    }

                    timelines[3].Play();
                    BtnSlide(2);
                    isDoneTween = false;
                }
                if (indexNum == 3)
                {
                    timelines[5].Play();
                    BtnSlide(3);
                    isDoneTween = false;
                }
                //업그레이드 화면에서 나가기
                if (indexNum == 4)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        TitleUII.UI.upGradePanels[i].SetActive(false);
                        TitleUII.UI.upGradeBtns[i].gameObject.SetActive(true);
                    }
                    indexNum = 2;
                    isDoneTween = false;
                }
            }
        }

    }

    /// <summary>모든 버튼 상호작용 관리</summary>
    public void BtnSlide(int index)
    {
        if (index == 1)
        {
            BtnGroupMove(0);
            backBtn.SetActive(true);

        }
        if (index == 2)
        {
            BtnGroupMove(1);
            backBtn.SetActive(false);
        }
        if (index == 3)
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
            btnGroupRect.DOAnchorPosX(-578, 0.5f).OnComplete(()=> {isDoneTween = true;});
        }
        //출발 업그레이드 백
        if (index == 1)
        {
            dexSpeed = 0.5f;
            btnGroupRect.DOAnchorPosX(70, dexSpeed).SetDelay(0.5f).OnComplete(()=>{isDoneTween = true;});
        }
        //나머지 버튼 3개 백
        if (index == 2)
        {
            dexSpeed = 0.3f;
            btnGroupRect.DOAnchorPosX(70, dexSpeed).SetDelay(0.4f).OnComplete(()=>{isDoneTween = true;});

        }
    }
}
